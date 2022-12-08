using System;
using System.Collections;
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

    [SerializeField] private List<GameData> jeuxChoisi = new List<GameData>();
    public int actualGameIndex { get; private set; }
    [SerializeField] private readonly int nbJeuParManche = 2;  
    
    private PlayerConfigurationManager playerConfiguration;
    [SerializeField] public int[] scoresJoueurs;
    [SerializeField] private readonly string finalSceneName = "FinalScoreScene";
    
    private event Action StartGame;

    public void subscribeToStartGame(Action action) {
        if (playerConfiguration.AllPlayersReady) {
            action.Invoke();
        } else {
            StartGame += action;
        }
    }

    private void Awake()
    {
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
            OnPlayerReady();
        else
            playerConfiguration.allPlayersAreReady += OnPlayerReady;
        
    }

    private void OnPlayerReady()
    {
        StartGame?.Invoke();
        StartGame = null;
        scoresJoueurs = new int[playerConfiguration.PlayerConfigurations.Count];
    }

    public void jeuSuivant(int[] resultatJoueur)
    {
        
        if (playerConfiguration.inputManager.playerCount > 1) {
            int sum = resultatJoueur.Sum();
            if (sum < 100) throw new Exception("The game finish without distributing the 100 points");
        } else {
            if (resultatJoueur[0] < 50) {
                actualGameIndex = jeuxChoisi.Count;
                SceneManager.LoadScene(nomLoadingScene);
            }
        }
        for (int i = 0; i < resultatJoueur.Length; i++) {
            scoresJoueurs[i] += resultatJoueur[i];
        }
        SceneManager.LoadScene(nomLoadingScene);
    }

    public void sceneMenu()
    {
        SceneManager.LoadScene(nomSceneMenu);
    }

    public void choisirJeux(int nbJoueur, int nbJeu)
    {
        List<GameData> jeuxPossible = scenesDeJeu.FindAll(data => data.nbJoueurs.Any(nbJ => nbJ == nbJoueur));
        jeuxChoisi.Clear();
        if (jeuxPossible.Count <= 0) {
            throw new GamesGenerationException("impossible de trouver " + nbJeu + " jeu avec " + nbJoueur + " joueurs");
        }
        for (int i = 0; i <= nbJeu; i++)
        {
            int randomIndex = Random.Range(0, jeuxPossible.Count - 1);
            jeuxChoisi.Add(jeuxPossible[randomIndex]);
        }
    }

    public AsyncOperation chargerProchainJeuxAsync()
    {
        if (jeuxChoisi.Count == 0) choisirJeux(1, nbJeuParManche);
        actualGameIndex++;
        AsyncOperation operation;
        operation = actualGameIndex >= jeuxChoisi.Count ? 
            SceneManager.LoadSceneAsync(finalSceneName) :
            SceneManager.LoadSceneAsync(jeuxChoisi[actualGameIndex].sceneName);
        operation.allowSceneActivation = false;
        return operation;
    }

}
}
