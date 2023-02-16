using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreJ3 : MonoBehaviour
{
    public Text Text;
    public float Scr ;
    public Joueur3 J3;
    void Start()
    {
        Scr = J3.Score;
    }

    // Update is called once per frame
    void Update()
    {
        Scr = J3.Score;
        Text.text = $"Score J3 : {Scr.ToString()}";
    }
}
