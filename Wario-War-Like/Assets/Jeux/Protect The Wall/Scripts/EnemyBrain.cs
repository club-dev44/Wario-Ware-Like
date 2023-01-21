using System.Collections;
using System.Collections.Generic;
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
        private int health;

        private int Health
        {
            get => health;
            set
            {
                health = value;
                if (health <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }

        // Start is called before the first frame update
        void Start()
        {
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
                BulletData bullet = collision.gameObject.GetComponent<BulletData>();
                Health -= bullet.DamageAmount;
                bullet.EnemyCollided();
            }
        }
    }

}