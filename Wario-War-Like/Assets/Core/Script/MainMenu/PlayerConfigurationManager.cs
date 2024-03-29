using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core
{

    public class PlayerConfigurationManager : MonoBehaviour
    {
        public PlayerInputManager inputManager { get; private set; }

        private List<PlayerConfiguration> playerConfigs;
        public IReadOnlyList<PlayerConfiguration> PlayerConfigurations { get => playerConfigs; }

        public static PlayerConfigurationManager Instance { get; private set; }

        [SerializeField] private bool enableJoiningByDefault;

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
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(Instance);
            playerConfigs = new List<PlayerConfiguration>();
            inputManager = GetComponent<PlayerInputManager>();
            inputManager.enabled = true;
            inputManager.onPlayerJoined += OnPlayerJoin;
            if (enableJoiningByDefault)
            {
                enableJoining();
            }
            else
            {
                disableJoining();
            }
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
                NotificationManager.LastInstanceCreated.addNotification("Everyone is ready! Let's go!!");
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

        public void reset()
        {
            RemoveAllPlayersFromGame();
            allPlayersReady = false;
            this.playerConfigs.Clear();
            inputManager.DisableJoining();
        }


        public void enableJoining()
        {
            inputManager.EnableJoining();
        }

        public void disableJoining()
        {
            inputManager.DisableJoining();
        }

        public void resetCurrentActionMapOfAllPlayersInput() {
            foreach (PlayerConfiguration playerConfiguration in PlayerConfigurations) {
                playerConfiguration.Input.currentActionMap = playerConfiguration.Input.actions.FindActionMap(playerConfiguration.Input.defaultActionMap);
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
}
