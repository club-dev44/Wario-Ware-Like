using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RedAlert
{
    public class WallDetector : MonoBehaviour
    {

        [SerializeField] private GameObject player;

        private void OnTriggerEnter2D(Collider2D col) {
            if (col.gameObject.Equals(player)) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
