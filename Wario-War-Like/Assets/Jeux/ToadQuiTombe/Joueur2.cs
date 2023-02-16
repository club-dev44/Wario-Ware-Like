using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Joueur2: MonoBehaviour
{
    public Rigidbody2D Rb;
    public Transform Tr;
    public Animator An;
    public bool end;
    public int Score;
    public float PositionDépart;
    public bool Bool_Feu;
    public bool Bool_Error;
    public bool Bool_Cligne;
    void Start()
    {
    end = true;
    Rb = gameObject.GetComponent<Rigidbody2D>();
    Tr = gameObject.GetComponent<Transform>();
    An = gameObject.GetComponent<Animator>();
    Score = 0;
    gameObject.AddComponent<BoxCollider2D>();
    PositionDépart = Tr.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey (KeyCode.F)){
        if (end==true){
            Destroy(Rb);
            Score = (int)((PositionDépart - Tr.position.y) / (PositionDépart + 37.33225f) * 1000);
                An.SetBool("Bool_Feu", true) ;
                An.SetBool("Bool_Cligne", false);
            end =false;
    }
    }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (end == true)
        {
            Score = 0;
            An.SetBool("Bool_Error", true);
            An.SetBool("Bool_Cligne", false);
            end = false;
        }
    }
}