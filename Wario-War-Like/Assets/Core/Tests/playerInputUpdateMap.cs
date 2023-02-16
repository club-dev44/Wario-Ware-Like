using System.Collections;
using System.Collections.Generic;
using Core;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TestTools;

public class playerInputUpdateMap : InputTestFixture
{
    private GameObject prefabPlayerManager = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Core/Prefab/PlayerManager.prefab");
    
    // A Test behaves as an ordinary method
    [Test]
    public void playerInputUpdateMapSimplePasses() {
    }

    [Test]
    public void canPressButtonOnGamePad() {
        var gamepad = InputSystem.AddDevice<Gamepad>();
        Press(gamepad.buttonSouth);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator playerInputUpdateMapWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        GameObject playerMananager = MonoBehaviour.Instantiate(prefabPlayerManager, Vector3.zero, Quaternion.identity);
        UnityEngine.Assertions.Assert.IsNotNull(playerMananager);
        PlayerConfigurationManager playerConfigurationManager = playerMananager.GetComponent<PlayerConfigurationManager>();
        Assert.Zero(playerConfigurationManager.PlayerConfigurations.Count);
        playerConfigurationManager.enableJoining();
        var gamepad = InputSystem.AddDevice<Gamepad>();
        Press(gamepad.startButton);
        yield return null;
        Assert.AreEqual(1, playerConfigurationManager.PlayerConfigurations.Count);
    }
}
