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
    private static SaveManager CreateTestSaveEnvironment()
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
        level.Quit(true);
        if (saveManager.GetLevelsProgress(levelIndex).Completed)
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
        var currentLevelData = saveManager.GetLevelsProgress(levelLoader.CurrentLevelIndex).Completed;

        level.Quit(false);

        if (saveManager.GetLevelsProgress(levelIndex).Completed == currentLevelData)
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

        if (shop.Buy(itemIndex, saveManager.GetCurrentMoney()))
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
        saveManager.ChangeMoney(shop.Items[itemIndex].Price);
        if (shop.Buy(0, saveManager.GetCurrentMoney()))
        {
            Assert.Pass();
        }
        else
            Assert.Fail();
    }

    [UnityTest]
    public IEnumerator EquipmentNotUnlockedWeaponEquipTest()
    {
        yield return SceneManager.LoadSceneAsync(0);
        var saveManager = CreateTestSaveEnvironment();
        var equipment = GameObject.FindAnyObjectByType<EquipmentManager>();
        var weaponID = 1;

        if (equipment.EquipWeapon(weaponID))
            Assert.Fail();
        else
            Assert.Pass();
        
    }
    [UnityTest]
    public IEnumerator EquipmentUnlockedWeaponEquipTest()
    {
        yield return SceneManager.LoadSceneAsync(0);
        var saveManager = CreateTestSaveEnvironment();
        var combatHandler = GameObject.FindAnyObjectByType<CombatHandler>();
        var equipment = GameObject.FindAnyObjectByType<EquipmentManager>();
        var weaponID = 1;
        saveManager.UnlockWeapon(weaponID);
        equipment.UpdateAvailableEquipment(GameObject.FindAnyObjectByType<Database>().EquippableWeapons);
        if (equipment.EquipWeapon(weaponID))
            Assert.Pass();
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
