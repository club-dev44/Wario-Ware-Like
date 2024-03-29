using System.Collections;
using UnityEngine;

namespace ProtectTheWall
{
    public class BulletBrain : MonoBehaviour
    {
        public int DamageAmount;

        [SerializeField]
        private Rigidbody2D rigidBody;
        [SerializeField]
        private float power;
        [SerializeField]
        private float lifeTimeAfterBounce, minimumVelocityThreshold;

        private TurretController playerOwner;

        public TurretController PlayerOwner
        {
            private get => playerOwner;
            set
            {
                if (playerOwner == null)
                    playerOwner = value;
                else
                    Debug.Log("Bullet already owned");
            }
        }

        private void Start()
        {
            rigidBody.AddRelativeForce(power * Vector3.up);
            StartCoroutine(IsItStoped());
        }

        private IEnumerator IsItStoped()
        {
            yield return new WaitForSeconds(1);
            while (true)
            {
                if (rigidBody.velocity.sqrMagnitude < minimumVelocityThreshold)
                    Destroy(gameObject);
                yield return new WaitForSeconds(.5f);
            }
        }

        public void EnemyCollided(int points)
        {
            PlayerOwner.Points += points;
            Destroy(gameObject);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                Destroy(gameObject, lifeTimeAfterBounce);
            }
        }
    }

}