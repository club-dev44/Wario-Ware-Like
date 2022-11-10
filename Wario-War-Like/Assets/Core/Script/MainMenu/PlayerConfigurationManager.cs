using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerConfigurationManager : MonoBehaviour
{
    public PlayerInputManager inputManager { get; private set; }

    private List<PlayerConfiguration> playerConfigs;

    public static PlayerConfigurationManager Instance { get; private set; }

    public event Action<int> playerReady;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
            playerConfigs = new List<PlayerConfiguration>();
        }

        inputManager = GetComponent<PlayerInputManager>();
    }

    private void Start()
    {
        inputManager.onPlayerJoined += OnPlayerJoin;
    }

    public void OnPlayerJoin(PlayerInput playerInput)
    {
        Debug.Log("Player joined");
        playerInput.transform.parent = transform;
        if (!playerConfigs.Any(pi => pi.PlayerIndex == playerInput.playerIndex))
            playerConfigs.Add(new PlayerConfiguration(playerInput));
    }

    public void PlayerReady(int index)
    {
        playerReady?.Invoke(index);
        playerConfigs[index].IsReady = !playerConfigs[index].IsReady;
        if (playerConfigs.TrueForAll(pi => pi.IsReady))
        {
            inputManager.DisableJoining();
            Debug.Log("Everyone is ready! Let's go!!");
            SceneManager.LoadScene("LoadingScene");
        }
    }

    public void RemoveAllPlayersFromGame()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}

public class PlayerConfiguration
{
    public PlayerConfiguration(PlayerInput playerInput)
    {
        Input = playerInput;
        PlayerIndex = playerInput.playerIndex;
        IsReady = false;
    }
    public PlayerInput Input;
    public int PlayerIndex;
    public bool IsReady;
    public Material PlayerMaterial;
}
