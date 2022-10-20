using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

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
    [SerializeField] private int actualGameIndex;
    
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
    

    public void jeuSuivant(int[] resultatJoueur)
    {
        //Todo passé au score manager le resultat des joueurs
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
        for (int i = 0; i < nbJeu; i++)
        {
            int randomIndex = Random.Range(0, jeuxPossible.Count - 1);
            jeuxChoisi.Add(jeuxPossible[randomIndex]);
        }
    }

    public AsyncOperation chargerProchainJeuxAsync()
    { 
        if(jeuxChoisi.Count == 0) choisirJeux(1, 10);
        actualGameIndex++;
        if (actualGameIndex >= jeuxChoisi.Count)
        {
            SceneManager.LoadScene(0);
            return null;
        }
        AsyncOperation operation = SceneManager.LoadSceneAsync(jeuxChoisi[actualGameIndex].sceneName);
        operation.allowSceneActivation = false;
        return operation;
    }
}
