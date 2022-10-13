using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
     
    // Static singleton instance
    public static GameManager Instance { get; private set; }
    private int score = 10;



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
        SceneManager.LoadScene("Jeu1");
    }

    public void sceneMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ajouterScore()
    {
        score++;
    }

    
    
}
