using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProtectTheWall
{
    public class EnemyAnimator : MonoBehaviour
    {
        [SerializeField]
        private EnemyBrain brain;
        [SerializeField]
        private Animator animator;

        private void Start()
        {
            brain.Dead += () => animator.SetTrigger("Died");
        }

        public void Died()
        {
            Destroy(gameObject);
        }
    }
}
