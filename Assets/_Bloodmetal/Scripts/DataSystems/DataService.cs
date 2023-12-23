using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;

public class DataService : IDataService
{
    public T LoadData<T>(string relativePath)
    {
        string path = Application.persistentDataPath + "/" + relativePath;
        if (!File.Exists(path))
            throw new FileNotFoundException($"{path} does nor exists.");
        try
        {
            Debug.Log($"Loading from {relativePath}");
            T data = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
            return data;
        }
        catch (Exception exception)
        {
            Debug.LogError($"Bruh while loading data due to:{exception.Message} {exception.StackTrace}");
            throw exception;
        }
    }

    public bool SaveData<T>(string relativePath, T data)
    {
        string path = Application.persistentDataPath + "/" + relativePath;
        try
        {
            Debug.Log($"Saving to {relativePath}");
            if (File.Exists(path))
                File.Delete(path);
            using FileStream stream = File.Create(path);
            stream.Close();
            File.WriteAllText(path, JsonConvert.SerializeObject(data));
            return true;
        }
        catch (Exception exception)
        {
            Debug.LogError($"Bruh while saving data due to:{exception.Message} {exception.StackTrace}");
            return false;
        }
    }
}

public interface IDataService
{
    bool SaveData<T>(string relativePath, T data);
    T LoadData<T>(string relativePath);
}
