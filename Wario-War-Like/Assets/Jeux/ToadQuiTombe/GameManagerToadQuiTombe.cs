using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;


public class GameManagerToadQuiTombe : MonoBehaviour
{
    [SerializeField] private Object playerPrefab;
    private int nbPlayer = 0;
    private int actualNbPlayer;
    private List<Joueur> players = new List<Joueur>();
    private int premier;
    private int deuxième;
    private int troisième;
    private int quatrième;
    private bool jeuFini = false;
    private bool canStartGame = false;
    private bool gameRunning = false;


    private List<int> remainingPlayers = new List<int>();

    private void Start()
    {
        GameManager.Instance.subscribeToStartGame(onReadyToStartGame);
    }
    private void onReadyToStartGame()
    {
        canStartGame = true;
        startGame();
    }

    private void startGame()
    {
        if (gameRunning) return;
        gameRunning = true;
        PlayerConfigurationManager playerConfigurationManager = PlayerConfigurationManager.Instance;
        IReadOnlyList<PlayerConfiguration> playerConfigurations = playerConfigurationManager.PlayerConfigurations;
        int index = 0;
        foreach (PlayerConfiguration playerConfiguration in playerConfigurations)
        {
            addPlayer(playerConfiguration, index);
            remainingPlayers.Add(index);
            index++;
        }

        nbPlayer = playerConfigurations.Count;
        actualNbPlayer = nbPlayer;
    }

    private void addPlayer(PlayerConfiguration playerConfiguration, int index)
    {
        GameObject player = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        Joueur playerComponent = player.GetComponent<Joueur>();
        playerComponent.playerInput = playerConfiguration.Input;
        playerComponent.playerIndex = index;
        players.Add(playerComponent);
    }

    private void fin()
    {
        int[] resultat = new int[nbPlayer];
        if (actualNbPlayer == 1)
        {
            resultat[premier] = 100;
        }
        if (actualNbPlayer == 2)
        {
            resultat[premier] = 75;
            resultat[deuxième] = 25;
        }
        if (actualNbPlayer == 3) 
        {
            resultat[premier] = 60;
            resultat[deuxième] = 25;
            resultat[troisième] = 15;
        }
        if (actualNbPlayer == 4)
        {
            resultat[premier] = 55;
            resultat[deuxième] = 25;
            resultat[troisième] = 15;
            resultat[quatrième] = 5;
        }

        GameManager.Instance.jeuSuivant(resultat);
    }
}
