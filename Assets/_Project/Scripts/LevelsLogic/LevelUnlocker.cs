using Selivura;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUnlocker : MonoBehaviour
{
    public void Unlock(int level)
    {
        var data = new LevelProgressData(false, true);
        SaveManager.instance.SaveLevelData(data, level);
    }
}
