using UnityEngine;

namespace Pong
{

    public class ScoreManagerPong : MonoBehaviour
    {
        private int scorePlayer1;
        private int scorePlayer2;

        [SerializeField]
        private TMPro.TextMeshProUGUI scoreText;

        [SerializeField]
        private GameManagerPong gameManager;

        // Start is called before the first frame update
        void Start()
        {
            resetGame();
        }

        private void resetGame()
        {
            scorePlayer1 = 0;
            scorePlayer2 = 0;
            updateScoreText();
        }

        private void updateScoreText()
        {
            scoreText.text = scorePlayer1 + " : " + scorePlayer2;

        }

        public void setScorePlayer1(int newScore)
        {
            scorePlayer1 = newScore;
            updateScoreText();
            if (scorePlayer1 >= 5)
            {
                gameManager.win("player 1 ! ");
            }

        }
        public void setScorePlayer2(int newScore)
        {
            scorePlayer2 = newScore;
            updateScoreText();
            if (scorePlayer2 >= 5)
            {
                gameManager.win("player 2 ! ");
            }
        }

        public int getScorePlayer1()
        {
            return scorePlayer1;
        }

        public int getScorePlayer2()
        {
            return scorePlayer2;
        }

    }
}
