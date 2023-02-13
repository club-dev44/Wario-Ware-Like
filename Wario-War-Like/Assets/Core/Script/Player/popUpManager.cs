using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class popUpManager : MonoBehaviour
{

    [SerializeField] private TMP_Text textUi;

    public void setNotificationText(string text) => textUi.SetText(text);

    [SerializeField] private  float DISPLAYTIME = 3.0f; 
    

    public void Start() {
        Invoke("hide", DISPLAYTIME);
    }

    private void hide() {
        Destroy(gameObject);
    }
}
