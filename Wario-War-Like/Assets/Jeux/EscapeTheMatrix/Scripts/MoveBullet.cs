using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBullet : MonoBehaviour
{

    public float init_speed;

    void Start(){
        Destroy(gameObject, 5.0f);
    }

    void Update()
    {
        Vector2 Move = new Vector2(-1,0)*init_speed;
        gameObject.transform.Translate(Move * Time.fixedDeltaTime);
    }
}
