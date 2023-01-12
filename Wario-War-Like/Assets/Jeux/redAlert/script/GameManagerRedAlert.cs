using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using Object = UnityEngine.Object;

namespace RedAlert
{
    public class GameManagerRedAlert : MonoBehaviour
    {
        [SerializeField] private Object playerPrefab;
        [SerializeField] private GameObject camera;
        [SerializeField] private LevelsManager levelsManager;
        
        private void Start() {
            GameManager.Instance.subscribeToStartGame(startGame);
        }


        private void startGame() {
            PlayerConfigurationManager playerConfigurationManager = PlayerConfigurationManager.Instance;
            IReadOnlyList<PlayerConfiguration> playerConfigurations = playerConfigurationManager.PlayerConfigurations;
            CameraPathFollower cameraPathFollower = camera.GetComponent<CameraPathFollower>();
            cameraPathFollower.players = new List<Transform>();
            foreach (PlayerConfiguration playerConfiguration in playerConfigurations) {
                GameObject player = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
                player.GetComponent<PlayerController>().playerInput = playerConfiguration.Input;
                player.GetComponent<PlayerController>().levelsManager = levelsManager;
                cameraPathFollower.players.Add(player.transform);
            }
            camera.SetActive(true);
        }
    }
}
