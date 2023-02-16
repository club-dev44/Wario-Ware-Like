using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreJ4 : MonoBehaviour
{
    public Text Text;
    public float Scr ;
    public Joueur4 J4;
    void Start()
    {
        Scr = J4.Score;
    }

    // Update is called once per frame
    void Update()
    {
        Scr = J4.Score;
        Text.text = $"Score J4 : {Scr.ToString()}";
    }
}
