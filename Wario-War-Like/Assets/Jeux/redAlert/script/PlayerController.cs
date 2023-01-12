using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace RedAlert{
    public class PlayerController : MonoBehaviour
    {
        public PlayerInput playerInput;
        public LevelsManager levelsManager;

        [SerializeField] private Rigidbody2D rigidbody2D;
        [SerializeField] private float boostUp = 1;
        [SerializeField] private float boostRight = 1;
        [SerializeField] private string tagWall;
        [SerializeField] private string nextLevelTriggerTag;
        [SerializeField] private string inputActionName = "Valider";
        private void FixedUpdate() {
            if (playerInput.actions[inputActionName].IsPressed()) {
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