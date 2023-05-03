using System.Collections;
using Core;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
public class Joueur : MonoBehaviour
{
    public PlayerInput playerInput;
    public int playerIndex;
    private string inputActionName;
    public Rigidbody2D Rb;
    public Transform Tr;
    public Animator An;
    public BoxCollider2D Bc;
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
        Bc = gameObject.GetComponent<BoxCollider2D>();
        PositionDépart = Tr.position.y;
    }


    private void FixedUpdate()
    {
        if (playerInput.actions[inputActionName].IsPressed())
        {
            if (end == true)
            {
                Destroy(Rb);
                Score = (int)((PositionDépart - Tr.position.y) / (PositionDépart + 37.33225f) * 10000);
                An.SetBool("Bool_Feu", true);
                An.SetBool("Bool_Cligne", false);
                end = false;
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