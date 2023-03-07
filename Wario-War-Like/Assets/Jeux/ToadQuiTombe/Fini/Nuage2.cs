using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nuage2 : MonoBehaviour


{
    public Transform Tr;
    public float X;
    public bool GD;

    // Start is called before the first frame update
    void Start()
    {
        X = Random.Range(-18f, 18f);
        Tr = gameObject.GetComponent<Transform>();
        Tr.position = new Vector3(X, Random.Range(-75.1f, -140.1f), 0);
        GD = false;

    }

    // Update is called once per frame
    void Update()
    {

        if (GD == false)
        {
            Tr.Translate(new Vector3(Random.Range(0.001f, 0.02f), 0, 0));
        }
        if ((Tr.position.x > 18) && (GD == false))
        {
            GD = true;
        }

        if (GD == true)
        {
            Tr.Translate(new Vector3(Random.Range(-0.02f, -0.001f), 0, 0));
        }

        if ((Tr.position.x < -18) && (GD == true))
        {
            GD = false;
        }
    }
}
