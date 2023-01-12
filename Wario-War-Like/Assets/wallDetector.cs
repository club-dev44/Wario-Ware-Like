using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class wallDetector : MonoBehaviour
{

    [SerializeField] private GameObject player;

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.Equals(player)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
