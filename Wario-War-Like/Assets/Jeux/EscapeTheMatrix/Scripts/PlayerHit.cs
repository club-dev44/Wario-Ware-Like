using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    public Rigidbody2D RB2D; 
    public InverseGravity GravityScript; 
    public CapsuleCollider2D Collider;
    public SpriteRenderer SRender; 
    public Animator Anim; 
    public Dictionary<string, string> correspondanceLayer= new Dictionary<string, string>();
    private GameManager GManager; 

    private int i; 

    void Start(){
        GManager = GameObject.Find("GameManager").GetComponent<GameManager>(); 
        for(i = 0; i < 4; i++) {
            correspondanceLayer.Add("Player"+(i+1).ToString(), "Dead"+(i+1).ToString());
        }
    }

    void OnCollisionEnter2D(Collision2D ObjectHit){
        if(ObjectHit.gameObject.name == "FloorDOWN"){
            Destroy(RB2D);
        }
        if(ObjectHit.gameObject.tag == "Ennemy"){
            GravityScript.enabled = false;
            SRender.flipY = false; 
            Anim.enabled = false; 
            RB2D.rotation = 90f; 
            RB2D.gravityScale = 5;
            gameObject.layer = LayerMask.NameToLayer(correspondanceLayer[gameObject.name]); 
            GManager.InfoDeath(gameObject.name);
        }
    }

}
