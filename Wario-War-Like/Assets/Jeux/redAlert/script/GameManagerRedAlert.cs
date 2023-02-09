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
        [SerializeField] private TMP_Text textChrono;
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
        private float timestampStartTime;
        [SerializeField] private Object textFollowerPrefab;

        private List<int> remainingPlayers = new List<int>();
        
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
            setChrono();
            PlayerConfigurationManager playerConfigurationManager = PlayerConfigurationManager.Instance;
            IReadOnlyList<PlayerConfiguration> playerConfigurations = playerConfigurationManager.PlayerConfigurations;
            CameraPathFollower cameraPathFollower = camera.GetComponent<CameraPathFollower>();
            cameraPathFollower.players = new List<Transform>();
            int index = 0;
            foreach (PlayerConfiguration playerConfiguration in playerConfigurations) {
                addPlayer(cameraPathFollower, playerConfiguration, index);
                remainingPlayers.Add(index);
                index++;
            }

            nbPlayer = playerConfigurations.Count;
            actualNbPlayer = nbPlayer;
            camera.SetActive(true);
        }

        private void addPlayer(CameraPathFollower cameraPathFollower, PlayerConfiguration playerConfiguration,
            int index) {
            GameObject player = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            PlayerController playerComponent = player.GetComponent<PlayerController>();
            playerComponent.playerInput = playerConfiguration.Input;
            playerComponent.levelsManager = levelsManager;
            playerComponent.playerIndex = index; 
            playerComponent.diedEvent += onPlayerDied;
            cameraPathFollower.players.Add(player.transform);
            players.Add(playerComponent);

            GameObject textFollowerGameObject = (GameObject)Instantiate(textFollowerPrefab, Vector3.zero, Quaternion.identity);
            TMP_Text textComponent = textFollowerGameObject.GetComponentInChildren<TMP_Text>();
            textComponent.SetText("Joueur " + index);
            GameObjectFollower gameObjectFollower = textFollowerGameObject.GetComponentInChildren<GameObjectFollower>();
            gameObjectFollower.setObjectToFollow(player);
        }

        private void Update() {
            if (gameRunning) {
                updateChrono();
            }
        }

        private void setChrono() {
            timestampStartTime = Time.time;
            textChrono.gameObject.SetActive(true);
            updateChrono();
        }

        private void updateChrono() {
            float elapsedTime = Time.time - timestampStartTime;
            textChrono.SetText((Mathf.Floor(elapsedTime / 60)).ToString("0") + ":" + (elapsedTime % 60).ToString("0.00"));
        }

        private void onPlayerDied(int index) {
            if(jeuFini) return;
            actualNbPlayer--;
            remainingPlayers.Remove(index);
            if (actualNbPlayer <= 1) {
                jeuFini = true;
                if (remainingPlayers.Count == 0) {
                    winner = index;
                } else {
                    winner = remainingPlayers[0];
                }
                gameRunning = false;
                if (players.Count > 1) {
                    winnerText.SetText("Bravo au joueur " + winner );
                } else {
                    float elapsedTime = Time.time - timestampStartTime;
                    string time = (Mathf.Floor(elapsedTime / 60)).ToString("0") + "m" + (elapsedTime % 60).ToString("0.00") + "s";
                    winnerText.SetText("Score : " + time);
                }
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
