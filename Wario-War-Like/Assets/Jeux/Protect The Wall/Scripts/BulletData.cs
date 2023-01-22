using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProtectTheWall
{
    public class BulletData : MonoBehaviour
    {
        public int DamageAmount;

        [SerializeField]
        private Rigidbody2D rigidBody;
        [SerializeField]
        private float power;

        private void Start()
        {
            rigidBody.AddRelativeForce(power * Vector3.up);
        }

        public void EnemyCollided()
        {
            Destroy(gameObject);
        }
    }

}