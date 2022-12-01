using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Core
{
    

public class scoreUiManager : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private Object cellScorePrefab;
    
    // Start is called before the first frame update
    void Start() {
        gameManager = GameManager.Instance;
        int playerIndex = 0;
        foreach (int score in gameManager.scoresJoueurs) {
            GameObject cell = (GameObject)Instantiate(cellScorePrefab, this.transform);
            cell.GetComponent<CellManager>().setScoreAndPlayer(playerIndex, score);
            playerIndex++;
        }
    }

}

}