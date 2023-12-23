using NUnit.Framework;
using Selivura;
using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class SavingTest
{
    private SaveManager CreateTestSaveEnvironment()
    {
        var saveManager = GameObject.FindAnyObjectByType<SaveManager>();
        saveManager.EnableSaveWriting = false;
        var testSave = saveManager.BlankSave;
        saveManager.LoadSave(testSave);
        return saveManager;
    }
    [UnityTest]
    public IEnumerator LevelCompleteProgressSaveTest()
    {
        yield return SceneManager.LoadSceneAsync(0);
        var saveManager = CreateTestSaveEnvironment();
        var levelLoader = GameObject.FindAnyObjectByType<LevelLoader>();
        yield return levelLoader.LoadLevel(0);
        var level = levelLoader.CurrentLevel;
        var levelIndex = levelLoader.CurrentLevelIndex;
        level.CompleteLevel();
        if (saveManager.GetLevelsProgress()[levelIndex].Completed)
            Assert.Pass();
        else
            Assert.Fail();
    } 
    [UnityTest]
    public IEnumerator LevelCancelProgressSaveTest()
    {
        yield return SceneManager.LoadSceneAsync(0);

        var saveManager = CreateTestSaveEnvironment();
        var levelLoader = GameObject.FindAnyObjectByType<LevelLoader>();

        yield return levelLoader.LoadLevel(0);

        var level = levelLoader.CurrentLevel;
        var levelIndex = levelLoader.CurrentLevelIndex;
        var currentLevelData = saveManager.GetLevelsProgress()[levelLoader.CurrentLevelIndex].Completed;

        level.CancelLevel();

        if (saveManager.GetLevelsProgress()[levelIndex].Completed == currentLevelData)
            Assert.Pass();
        else
            Assert.Fail();
    }
    [UnityTest]
    public IEnumerator MoneyIncreaseTest()
    {
        yield return SceneManager.LoadSceneAsync(0);
        var saveManager = CreateTestSaveEnvironment();
        var shop = GameObject.FindAnyObjectByType<Shop>();
        saveManager.ChangeMoney(10);
        Assert.AreEqual(10, saveManager.GetCurrentMoney());
    }
    [UnityTest]
    public IEnumerator ShopNotEnoughMoneyTest()
    {
        yield return SceneManager.LoadSceneAsync(0);
        var saveManager = CreateTestSaveEnvironment();
        var shop = GameObject.FindAnyObjectByType<Shop>();
        int itemIndex = 0;

        if (shop.BuyUpgrade(itemIndex))
            Assert.Fail();
        else
            Assert.Pass();
    }
    [UnityTest]
    public IEnumerator ShopBuyTest()
    {
        yield return SceneManager.LoadSceneAsync(0);
        var saveManager = CreateTestSaveEnvironment();
        var shop = GameObject.FindAnyObjectByType<Shop>();
        int itemIndex = 0;
        saveManager.ChangeMoney(shop.Upgrades[itemIndex].Price);
        if (shop.BuyUpgrade(0) && saveManager.GetCurrentMoney() == 0)
        {
            Assert.Pass();
        }
        else
            Assert.Fail();
    }
    [UnityTest]
    public IEnumerator DashUnlockTest() 
    {
        yield return SceneManager.LoadSceneAsync(0);
        var saveManager = CreateTestSaveEnvironment();
        saveManager.UnlockDash();
        Assert.AreEqual(true, saveManager.GetDashUnlocked());
    }
    [UnityTest]
    public IEnumerator WallJumpUnlockTest()
    {
        yield return SceneManager.LoadSceneAsync(0);
        var saveManager = CreateTestSaveEnvironment();
        saveManager.UnlockWallJump();
        Assert.AreEqual(true, saveManager.GetWallJumpUnlocked());
    }
}
