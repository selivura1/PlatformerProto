using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using Selivura;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UIElements;
public class PlayerTests
{
    [UnityTest]
    public IEnumerator PlayerDamageTest()
    {
        yield return SceneManager.LoadSceneAsync(0);
        var player = GameObject.FindAnyObjectByType<Player>();
        player.TakeDamage(player.MaxHealth / 2);
        Assert.AreEqual(player.MaxHealth / 2, player.Health);
    }
    [UnityTest]
    public IEnumerator PlayerDeathTest()
    {
        yield return SceneManager.LoadSceneAsync(0);
        var player = GameObject.FindAnyObjectByType<Player>();
        player.TakeDamage(player.MaxHealth);
        Assert.AreEqual(0, player.Health);
        Assert.AreEqual(false, player.gameObject.activeSelf);
    }
    [UnityTest]
    public IEnumerator PlayerCoinPickupTest()
    {
        yield return SceneManager.LoadSceneAsync(0);
        var player = GameObject.FindAnyObjectByType<Player>();
        var coin = GameObject.Instantiate(GameObject.FindAnyObjectByType<Database>().Coin);
        var position = new Vector3(0, 5, 0);
        player.transform.position = position;
        coin.transform.position = position;
        yield return new WaitForFixedUpdate();
        Assert.AreEqual(1, player.Score);
    }
}
