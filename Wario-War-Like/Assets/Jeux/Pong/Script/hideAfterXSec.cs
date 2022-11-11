using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Pong
{
    
public class hideAfterXSec : MonoBehaviour
{
    [SerializeField]
    private float time = 0.9f;

    private void OnEnable()
    {
        Invoke("hide", time);
    }

    private void hide()
    {
        this.gameObject.SetActive(false);
    }

}
}
