using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProtectTheWall
{
    public class BulletData : MonoBehaviour
    {
        public int DamageAmount;

        public void EnemyCollided()
        {
            Destroy(gameObject);
        }
    }

}