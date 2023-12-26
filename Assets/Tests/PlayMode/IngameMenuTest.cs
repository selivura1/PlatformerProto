using NUnit.Framework;
using Selivura;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class IngameMenuTest
{
    [UnityTest]
    public IEnumerator OpenInMainMenuTest()
    {
        yield return SceneManager.LoadSceneAsync(0);
        var menu = GameObject.FindAnyObjectByType<IngameMenuUI>();
        var levelLoader = GameObject.FindAnyObjectByType<LevelLoader>();
        yield return levelLoader.LoadMainMenu();
        if (menu.OpenMenu())
        {
            Assert.Fail();
        }
        else
        {
            Assert.Pass();
        }
        yield return null;
    }
    [UnityTest]
    public IEnumerator ContinueGameTest()
    {
        yield return SceneManager.LoadSceneAsync(0);
        var menu = GameObject.FindAnyObjectByType<IngameMenuUI>();
        var levelLoader = GameObject.FindAnyObjectByType<LevelLoader>();
        yield return levelLoader.LoadLevel(0);
        menu.OpenMenu();
        if (!menu.ContinueGame())
            Assert.Fail();
        else
            Assert.Pass();
        yield return null;
    }
    [UnityTest]
    public IEnumerator RestartLevelTest()
    {
        yield return SceneManager.LoadSceneAsync(0);
        var menu = GameObject.FindAnyObjectByType<IngameMenuUI>();
        var levelLoader = GameObject.FindAnyObjectByType<LevelLoader>();

        yield return levelLoader.LoadLevel(0);
        int currentLevelIndex = levelLoader.CurrentLevelIndex;

        menu.RestartLevel();
        Assert.AreEqual(currentLevelIndex, levelLoader.CurrentLevelIndex);

        yield return null;
    }
}
