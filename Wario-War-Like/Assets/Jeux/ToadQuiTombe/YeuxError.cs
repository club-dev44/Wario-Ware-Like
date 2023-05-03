using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YeuxError : MonoBehaviour
{
    public Animator An;
    public Joueur J;
    public bool erreur;

    // Start is called before the first frame update
    void Start()
    {
        erreur = J.end;
    }

    // Update is called once per frame
    void Update()
    {
        erreur = J.end;
        if (erreur == true) { An.SetBool("Bool_Error", true); }
    }
}
