using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownAnimation : MonoBehaviour
{

    [SerializeField] private GameObject animGameObject;
    [SerializeField] private float animationDuration = 3.0f;
    
    public void playAnimation()
    {
        IEnumerator animation = animationEnumerator();
        StartCoroutine(animation);
    }

    IEnumerator animationEnumerator()
    {
        animGameObject.SetActive(true);
        yield return new WaitForSeconds(animationDuration);
        animGameObject.SetActive(false);
    }


}