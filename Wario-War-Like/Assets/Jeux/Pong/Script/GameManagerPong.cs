using Core;
using System;
using System.Collections;
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
        private GameObject animationNewMatch;

        [SerializeField]
        private GameObject explosion;

        private bool end = false;

        [SerializeField]
        private TMPro.TextMeshProUGUI winText;


        public bool gameStarted = false;
        public event Action gameStart;
        public event Action matchStart;



        private void Start()
        {
            GameManager.Instance.SubscribeToStartGame(newMatch);
        }


        public void newMatch()
        {
            if (end) return;
            player1.transform.position = player1StartPosition;
            player2.transform.position = player2StartPosition;
            ball.transform.position = Vector3.zero;
            ball.GetComponent<BallConttroll>().play = false;

            IEnumerator coroutine = compteARebourNewMatch();
            animationNewMatch.SetActive(true);

            StartCoroutine(coroutine);
        }

        internal void win(string v)
        {
            ball.GetComponent<BallConttroll>().play = false;
            winText.text = "bravo " + v;
            end = true;
            StartCoroutine(Quit(new[]
            {
        scoreManager.getScorePlayer1(),
        scoreManager.getScorePlayer2()
    }));
        }


        IEnumerator Quit(int[] resultat)
        {
            yield return new WaitForSeconds(5.0f);
            if (resultat[0] == 5)
            {
                GameManager.Instance.EndMyGame(new[] { 100, 0 });
            }
            else
            {
                GameManager.Instance.EndMyGame(new[] { 0, 100 });
            }
        }

        IEnumerator compteARebourNewMatch()
        {

            yield return new WaitForSeconds(3f);
            explosion.SetActive(true);
            animationNewMatch.SetActive(false);
            matchStart?.Invoke();
            if (!gameStarted)
            {
                gameStarted = true;
                gameStart?.Invoke();
            }
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