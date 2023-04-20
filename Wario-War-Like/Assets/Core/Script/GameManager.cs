using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Core
{


    public class GameManager : MonoBehaviour
    {


        // Static singleton instance
        public static GameManager Instance { get; private set; }

        [Header("Scenes Settings")]
        [Tooltip("la scene doit être dans les build settings")]
        public List<GameData> scenesDeJeu = new List<GameData>();
        private string nomSceneMenu = "Menu";
        private string nomLoadingScene = "loadingScene";

        [SerializeField] public List<GameData> jeuxChoisi = new();
        public int CurrentGameIndex { get; private set; }
        [SerializeField] private readonly int nbJeuParManche = 2;

        private PlayerConfigurationManager playerConfiguration;
        [SerializeField] public int[] scoresJoueurs;
        [SerializeField] private readonly string finalSceneName = "FinalScoreScene";

        private event Action StartGame;

        /// <summary>
        /// Allows to subscribe a method to the event triggered when the game should start
        /// The methods provided are used to start the whole game
        /// </summary>
        /// <param name="action">A methode to subscribe the starting game event</param>
        public void SubscribeToStartGame(Action action)
        {
            playerConfiguration = PlayerConfigurationManager.Instance;
            if (playerConfiguration.AllPlayersReady)
            {
                action.Invoke();
            }
            else
            {
                StartGame += action;
            }
        }

        private void Awake()
        {
            // Unity singleton pattern
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            playerConfiguration = PlayerConfigurationManager.Instance;
            if (playerConfiguration.AllPlayersReady)
                scoresJoueurs = new int[playerConfiguration.PlayerConfigurations.Count];
            playerConfiguration.allPlayersAreReady += PlayerConfigurationOnAllPlayersAreReady;
        }

        /// <summary>
        /// The behaviour when all players are set to ready from the player configuration
        /// </summary>
        private void PlayerConfigurationOnAllPlayersAreReady()
        {
            StartGame?.Invoke();
            StartGame = null;
            scoresJoueurs = new int[playerConfiguration.PlayerConfigurations.Count];
        }

        /// <summary>
        /// The method to end the current game
        /// Should provide points to each player for a total of 100 points
        /// </summary>
        /// <param name="resultatJoueur">An array of the points distributed to each player, should totalize 100 points</param>
        /// <exception cref="Exception">All the points where not distibuted correctly</exception>
        public void EndMyGame(int[] resultatJoueur)
        {
            if (playerConfiguration.inputManager.playerCount > 1)
            {
                int sum = resultatJoueur.Sum();
                if (sum != 100) throw new Exception("The game finish without distributing the 100 points");
            }
            else
            {
                if (resultatJoueur[0] < 50)
                {
                    CurrentGameIndex = jeuxChoisi.Count;
                    SceneManager.LoadScene(nomLoadingScene);
                }
            }
            for (int i = 0; i < resultatJoueur.Length; i++)
            {
                scoresJoueurs[i] += resultatJoueur[i];
            }
            SceneManager.LoadScene(nomLoadingScene);
        }

        /// <summary>
        /// Load the scene menu
        /// </summary>
        public void SceneMenu()
        {
            SceneManager.LoadScene(nomSceneMenu);
        }

        /// <summary>
        /// The logic to choose a game amoung the possibe ones
        /// </summary>
        /// <param name="nbJoueur">The number of player that will play</param>
        /// <param name="nbJeu">The total number of games</param>
        /// <exception cref="GamesGenerationException">It was not possible to choose a game which fits the requirements</exception>
        private void ChooseGame(int nbJoueur, int nbJeu)
        {
            List<GameData> jeuxPossible = scenesDeJeu.FindAll(data => data.nbJoueurs.Any(nbJ => nbJ == nbJoueur));
            jeuxChoisi.Clear();
            if (jeuxPossible.Count <= 0)
            {
                throw new GamesGenerationException("impossible de trouver " + nbJeu + " jeu avec " + nbJoueur + " joueurs");
            }
            for (int i = 0; i <= nbJeu; i++)
            {
                int randomIndex = Random.Range(0, jeuxPossible.Count);
                jeuxChoisi.Add(jeuxPossible[randomIndex]);
            }
        }

        /// <summary>
        /// Load the next game while in the loading screen
        /// </summary>
        public AsyncOperation LoadNextGameAsync()
        {
            if (jeuxChoisi.Count == 0) ChooseGame(playerConfiguration.inputManager.playerCount, nbJeuParManche);
            CurrentGameIndex++;
            AsyncOperation operation;
            operation = CurrentGameIndex >= jeuxChoisi.Count ?
                SceneManager.LoadSceneAsync(finalSceneName) :
                SceneManager.LoadSceneAsync(jeuxChoisi[CurrentGameIndex].sceneName);
            operation.allowSceneActivation = false;
            return operation;
        }
    }
}
