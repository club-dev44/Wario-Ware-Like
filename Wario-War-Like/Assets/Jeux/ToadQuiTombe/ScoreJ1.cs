using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreJ1 : MonoBehaviour
{
    public Text Text;
    public float Scr ;
    public Joueur1 J1;
    void Start()
    {
        Scr = J1.Score;
    }

    // Update is called once per frame
    void Update()
    {
        Scr = J1.Score;
        Text.text = $"Score J1 : {Scr.ToString()}";
    }
}
