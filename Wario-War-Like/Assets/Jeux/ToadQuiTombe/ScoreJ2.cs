using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreJ2 : MonoBehaviour
{
    public Text Text;
    public float Scr ;
    public Joueur2 J2;
    void Start()
    {
        Scr = J2.Score;
    }

    // Update is called once per frame
    void Update()
    {
        Scr = J2.Score;
        Text.text = $"Score J2 : {Scr.ToString()}";
    }
}
