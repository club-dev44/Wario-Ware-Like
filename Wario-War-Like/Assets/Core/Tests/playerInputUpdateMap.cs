using System.Collections;
using System.Collections.Generic;
using Core;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class playerInputUpdateMap : InputTestFixture
{
    private GameObject prefabPlayerManager = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Core/Prefab/PlayerManager.prefab");
    
    private string DEFAULT_ACTION_MAP_NAME = "Controls";
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
    public IEnumerator playerIsAbleToJoin()
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
    
    [UnityTest]
    public IEnumerator defaultMapIsSetOnPlayer()
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
        Assert.IsTrue(playerConfigurationManager.PlayerConfigurations[0].Input.actions.name.Equals(DEFAULT_ACTION_MAP_NAME));
    }
    
    [UnityTest]
    public IEnumerator actionMapHaveBeenChangedOnPlayers() {
        EditorSceneManager.LoadSceneInPlayMode("Assets/Core/Tests/sceneTest.unity",
            new LoadSceneParameters(LoadSceneMode.Single));
        yield return null;
        Assert.NotNull(PlayerConfigurationManager.Instance.gameObject);
        PlayerConfigurationManager.Instance.enableJoining();
        var gamepad = InputSystem.AddDevice<Gamepad>();
        Press(gamepad.startButton);
        yield return null;

        GameObject testeur = GameObject.Find("testeur");
        Assert.NotNull(testeur);
        testeur.GetComponent<ChangeActionMapOnPlayers>().changeActionMapOnPlayers();
        Assert.True(PlayerConfigurationManager.Instance.PlayerConfigurations[0].Input.currentActionMap.name.Equals("test"));
    }

    
    [UnityTest]
    public IEnumerator actionMapIsResetAfterSceneIsLoaded() {
        EditorSceneManager.LoadSceneInPlayMode("Assets/Core/Tests/sceneTest.unity",
            new LoadSceneParameters(LoadSceneMode.Single));
        yield return null;
        Assert.NotNull(PlayerConfigurationManager.Instance.gameObject);
        PlayerConfigurationManager.Instance.enableJoining();
        var gamepad = InputSystem.AddDevice<Gamepad>();
        Press(gamepad.startButton);
        yield return null;
        GameObject testeur = GameObject.Find("testeur");
        testeur.GetComponent<ChangeActionMapOnPlayers>().changeActionMapOnPlayers();
        
        //on change de scene loadingScene
        SceneManager.LoadScene("loadingScene");
        yield return null;
        //on verifique que le player a bien le bon actionMap
        PlayerConfigurationManager playerConfigurationManager = PlayerConfigurationManager.Instance;
        Assert.True(playerConfigurationManager.PlayerConfigurations[0].Input.currentActionMap.name.Equals(DEFAULT_ACTION_MAP_NAME));

    }
    
}
