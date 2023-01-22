using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProtectTheWall
{
    public class ProtectTheWallManger : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> possibleBullets = new List<GameObject>();
        [SerializeField]
        private List<GameObject> enemies = new List<GameObject>();

        public GameObject GetCurrentBullet()
        {
            return possibleBullets[Random.Range(0,possibleBullets.Count)]; // changer pour une fonction genre du temps de jeu et du niveau de difficulté
        }

        public GameObject GetAMobToSpawn()
        {
            return enemies[Random.Range(0, enemies.Count)]; // changer pour une fonction genre du temps de jeu et du niveau de difficulté
        }
    }
}