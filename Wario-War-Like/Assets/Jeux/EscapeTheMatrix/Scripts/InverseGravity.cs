using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverseGravity : MonoBehaviour
{
    public Rigidbody2D RB2D; 
    public SpriteRenderer SRender; 
    public GameManager GManager; 
    private bool check_ground;

    void Start()
    {   
        GManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        RB2D.gravityScale = GManager.gravity;
        check_ground = true; 
    }

    void Update()
    {
        if(Input.GetButton("Jump" + gameObject.name) && check_ground){
            RB2D.gravityScale = -RB2D.gravityScale;
            SRender.flipY = !SRender.flipY; 
            check_ground = false;
        }
    }

    void OnTriggerEnter2D(Collider2D ObjectHit){
        if(ObjectHit.gameObject.tag == "TriggerGravity" ){
            check_ground = true;
        }
    }
}
