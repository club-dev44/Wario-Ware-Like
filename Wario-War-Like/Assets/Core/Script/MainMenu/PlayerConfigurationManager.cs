using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Core
{
    
public class PlayerConfigurationManager : MonoBehaviour
{
    public PlayerInputManager inputManager { get; private set; }

    private List<PlayerConfiguration> playerConfigs;
    public IReadOnlyList<PlayerConfiguration> PlayerConfigurations { get => playerConfigs; }

    public static PlayerConfigurationManager Instance { get; private set; }

    
    public event Action<int> playerReady;

    private bool allPlayersReady;
    public bool AllPlayersReady
    {
        get => allPlayersReady;
        private set
        {
            if (value) allPlayersAreReady?.Invoke();
            allPlayersReady = value;
        }
    }

    public event Action allPlayersAreReady;

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
        inputManager.onPlayerJoined += OnPlayerJoin;
        inputManager.DisableJoining();
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
            AllPlayersReady = true;
        }
    }
    

    public void RemoveAllPlayersFromGame()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void reset() {
        RemoveAllPlayersFromGame();
        allPlayersReady = false;
        this.playerConfigs.Clear();
        inputManager.DisableJoining();
    }


    public void enableJoining() {
        inputManager.EnableJoining();
    }

    public void disableJoining() {
        inputManager.DisableJoining();
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
}
