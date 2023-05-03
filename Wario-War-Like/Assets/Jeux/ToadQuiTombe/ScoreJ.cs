using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreJ : MonoBehaviour
{
    public Text Text;
    public float Scr ;
    public Joueur J;
    void Start()
    {
        Scr = J.Score;
    }

    // Update is called once per frame
    void Update()
    {
        Scr = J.Score;
        Text.text = $"Score : {Scr.ToString()}";
    }
}
