using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core
{


    public class scoreUiManager : MonoBehaviour
    {
        private GameManager gameManager;
        [SerializeField] private Object cellScorePrefab;
        [SerializeField] private NotificationManager notificationManager;

        // Start is called before the first frame update
        void Start()
        {
            gameManager = GameManager.Instance;
            int playerIndex = 0;
            Array.Sort(gameManager.scoresJoueurs);
            Array.Reverse(gameManager.scoresJoueurs);
            foreach (int score in gameManager.scoresJoueurs)
            {
                GameObject cell = (GameObject)Instantiate(cellScorePrefab, this.transform);
                cell.GetComponent<CellManager>().setScoreAndPlayer(playerIndex, score);
                playerIndex++;
            }
        }

    }

}