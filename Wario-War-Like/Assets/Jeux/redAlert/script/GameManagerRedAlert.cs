using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace RedAlert
{
    public class GameManagerRedAlert : MonoBehaviour
    {
        [SerializeField] private Object playerPrefab;
        [SerializeField] private GameObject camera;
        [SerializeField] private LevelsManager levelsManager;
        private int nbPlayer = 0;
        private int actualNbPlayer;
        private List<PlayerController> players = new List<PlayerController>();
        private int winner;
        private bool jeuFini = false;
        [SerializeField] private GameObject animation;

        [SerializeField] private TextMeshProUGUI winnerText;
        private bool canStartGame = false;
        private bool gameRunning = false;
        private bool animTerminer = false;
        private float durationAnimation = 5.1f;
        private void Start() {
            GameManager.Instance.subscribeToStartGame(onReadyToStartGame);
            StartCoroutine(animationWaiting());
        }

        IEnumerator animationWaiting() {
            yield return new WaitForSeconds(durationAnimation);
            if(canStartGame) startGame();
            animTerminer = true;
        }


        private void onReadyToStartGame() {
            canStartGame = true;
            if(animTerminer) startGame();
        }

        

        private void startGame() {
            if(gameRunning) return;
            gameRunning = true;
            animation.SetActive(false);
            PlayerConfigurationManager playerConfigurationManager = PlayerConfigurationManager.Instance;
            IReadOnlyList<PlayerConfiguration> playerConfigurations = playerConfigurationManager.PlayerConfigurations;
            CameraPathFollower cameraPathFollower = camera.GetComponent<CameraPathFollower>();
            cameraPathFollower.players = new List<Transform>();
            int index = 0;
            foreach (PlayerConfiguration playerConfiguration in playerConfigurations) {
                GameObject player = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
                PlayerController playerComponent = player.GetComponent<PlayerController>();
                playerComponent.playerInput = playerConfiguration.Input;
                playerComponent.levelsManager = levelsManager;
                playerComponent.playerIndex = index; 
                playerComponent.diedEvent += onPlayerDied;
                cameraPathFollower.players.Add(player.transform);
                players.Add(playerComponent);
                index++;
            }

            nbPlayer = playerConfigurations.Count;
            actualNbPlayer = nbPlayer;
            camera.SetActive(true);
        }

        private void onPlayerDied(int index) {
            if(jeuFini) return;
            actualNbPlayer--;
            if (actualNbPlayer <= 1) {
                jeuFini = true;
                winner = index;
                winnerText.SetText("Bravo au joueur " + winner);
                Invoke("fin", 5.0f);
            }
        }

        private void fin() {
            int[] resultat = new int[nbPlayer];
            resultat[winner] = 100;
            GameManager.Instance.jeuSuivant(resultat);
        }
    }
}
