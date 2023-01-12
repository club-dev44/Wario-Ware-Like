using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace RedAlert{


public class PlayerControler : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private float boostUp = 1;
    [SerializeField] private float boostRight = 1;
    [SerializeField] private string tagWall;
    [SerializeField] private string nextLevelTriggerTag;
    [SerializeField] private LevelsManager levelsManager;
    private void FixedUpdate() {
        if (Input.GetKey(KeyCode.Space)) {
            rigidbody2D.velocity += new Vector2(boostRight, boostUp) * Time.deltaTime;
        } 
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.tag.Equals(tagWall)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (col.tag.Equals(nextLevelTriggerTag)) {
            levelsManager.addLevel();
        }
    }
}

}