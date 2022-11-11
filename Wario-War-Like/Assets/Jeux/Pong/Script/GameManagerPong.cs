using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    
public class GameManagerPong : MonoBehaviour
{
    [SerializeField]
    private ScoreManagerPong scoreManager;


    [SerializeField]
    private GameObject player1;



    [SerializeField]
    private GameObject player2;

    [SerializeField]
    private GameObject ball;


    [SerializeField]
    private Vector2 player1StartPosition;


    [SerializeField]
    private Vector2 player2StartPosition;


    [SerializeField]
    private GameObject animation;

    [SerializeField]
    private GameObject explosion;

    private bool end = false;

    [SerializeField]
    private TMPro.TextMeshProUGUI winText;

    private void Start()
    {
        newMatch();
    }


    public void newMatch()
    {
        if (end) return;
        player1.transform.position = player1StartPosition;
        player2.transform.position = player2StartPosition;
        ball.transform.position = Vector3.zero;
        ball.GetComponent<BallConttroll>().play = false;
        ball.GetComponent<BallConttroll>().setSpeed(10.0f);

        IEnumerator coroutine = compteARebourNewMatch();
        animation.SetActive(true);

        StartCoroutine(coroutine);
    }

    internal void win(string v)
    {
        ball.GetComponent<BallConttroll>().play = false;
        winText.text = "bravo " + v;
        end = true;
        StartCoroutine(Quit(new []
        {
            scoreManager.getScorePlayer1(),
            scoreManager.getScorePlayer2()
        }));
    }


    IEnumerator Quit(int[] resultat)
    {
        yield return new WaitForSeconds(5.0f);
        GameManager.Instance.jeuSuivant(resultat);
    }

    IEnumerator compteARebourNewMatch()
    {
        
        yield return new WaitForSeconds(3f);
        explosion.SetActive(true);


        ball.GetComponent<BallConttroll>().play = true;
         animation.SetActive(false);
    }


    public void player1MissBall()
    {
        scoreManager.setScorePlayer2(scoreManager.getScorePlayer2() + 1);
        newMatch();
    }

    public void player2MissBall()
    {
        scoreManager.setScorePlayer1(scoreManager.getScorePlayer1() + 1);
        newMatch();
    }

}

}