using System;
using System.Collections;
using UnityEngine;

namespace ProtectTheWall
{
    public class EnemyBrain : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D rigidBody;
        [SerializeField]
        private float moveSpeed;
        [SerializeField]
        private int maxHealth;
        [SerializeField, Range(0, 1)]
        private float spawnProbability;
        public float SpawnProbability { get => spawnProbability; }

        private int currentHealth;
        private int Health
        {
            get => currentHealth;
            set
            {
                currentHealth = value;
                if (currentHealth <= 0)
                {
                    if (Dead == null) Destroy(gameObject);
                    else
                    {
                        StopCoroutine(nameof(Walk));
                        rigidBody.simulated = false;
                        Dead.Invoke();
                    }
                }
            }
        }

        public event Action Dead;

        // Start is called before the first frame update
        void Start()
        {
            currentHealth = maxHealth;
            StartCoroutine(nameof(Walk));
        }

        private IEnumerator Walk()
        {
            while (true)
            {
                rigidBody.MovePosition(moveSpeed * Time.fixedDeltaTime * Vector3.down + transform.position);
                yield return new WaitForFixedUpdate();
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
            {
                BulletBrain bullet = collision.gameObject.GetComponent<BulletBrain>();
                Health -= bullet.DamageAmount;
                int pointsToGive = Health <= 0 ? maxHealth : 0;
                bullet.EnemyCollided(pointsToGive);
            }
            if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
                Destroy(gameObject);
        }
    }

}