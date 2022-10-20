using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemareJeu : MonoBehaviour
{
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
    }


    public void DemarerJeu()
    {
        gameManager.jeuSuivant();
    }
}
