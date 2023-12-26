using NUnit.Framework;
using Selivura;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class LevelLoadingTest
{
    [UnityTest]
    public IEnumerator LevelLoadingTestEnum()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return SceneManager.LoadSceneAsync(0);
        var levelLoader = GameObject.FindAnyObjectByType<LevelLoader>();
        yield return new WaitForSeconds(1);
        yield return levelLoader.LoadLevel(0);
        Assert.AreEqual(0, levelLoader.CurrentLevelIndex);
        Assert.AreEqual(levelLoader.AllLevels[0].SceneID, levelLoader.AllLevels[levelLoader.CurrentLevelIndex].SceneID);
    }
    [UnityTest]
    public IEnumerator LevelRestartTestEnum()
    {
        yield return SceneManager.LoadSceneAsync(0);

        var levelLoader = GameObject.FindAnyObjectByType<LevelLoader>();
        yield return levelLoader.LoadLevel(0);

        var expectedLevel = levelLoader.CurrentLevelIndex;
        yield return levelLoader.RestartCurrentLevel();
        Assert.AreEqual(expectedLevel, levelLoader.CurrentLevelIndex);
    }

}
