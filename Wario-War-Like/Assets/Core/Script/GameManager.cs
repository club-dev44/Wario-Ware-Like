using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
     
    // Static singleton instance
    public static GameManager Instance { get; private set; }
    private int score = 10;
    
    [Header("Scenes Settings")]
    [Tooltip("la scene doit être dans les build settings")]
    public List<GameData> scenesDeJeu = new List<GameData>();
    public string nomSceneMenu = "Menu";


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
    

    public void jeuSuivant()
    {
        SceneManager.LoadScene(scenesDeJeu[0].sceneName);
    }

    public void sceneMenu()
    {
        SceneManager.LoadScene(nomSceneMenu);
    }

    public void ajouterScore()
    {
        score++;
    }

    
    
}
