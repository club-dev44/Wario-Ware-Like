using Core;
using System.Collections.Generic;
using UnityEngine;

namespace ProtectTheWall
{
    public class ProtectTheWallManger : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> enemies = new();
        [SerializeField]
        private Animator boomAnimator;
        [SerializeField]
        private GameObject boomSprite, bulletBag, enemySpawner;

        private List<TurretController> players = new();

        private void Start()
        {
            boomSprite.SetActive(false);
        }

        public void AddPlayer(TurretController player)
        {
            players.Add(player);
        }

        public GameObject GetAMobToSpawn()
        {
            GameObject chosenEnemy = null;
            while (chosenEnemy == null)
            {
                GameObject possibleEnemy = enemies[Random.Range(0, enemies.Count)];
                if (Random.value < possibleEnemy.GetComponent<EnemyBrain>().SpawnProbability)
                {
                    chosenEnemy = possibleEnemy;
                }
            }
            return chosenEnemy;
        }

        public void FinishGame()
        {
            Destroy(bulletBag);
            Destroy(enemySpawner);
            boomSprite.SetActive(true);
            boomAnimator.Play("BigBOOM");
        }

        private void WaitForTheExplosion()
        {
            int totalScoreSum = 0;
            int bestPlayerIndex = 0;
            for (int index = 0; index < players.Count; index++)
            {
                if (players[index].Points > players[bestPlayerIndex].Points)
                    bestPlayerIndex = index;
                totalScoreSum += players[index].Points;
            }
            totalScoreSum += players[bestPlayerIndex].Points; // to double the winner points

            int[] scores = new int[players.Count];
            int secondarySum = 0; // to be able to have exactly 100 points distributed
            for (int index = 0; index < players.Count; index++)
            {
                int playerPoints = players[index].Points;
                if (index == bestPlayerIndex)
                    playerPoints *= 2;
                scores[index] = Mathf.FloorToInt(playerPoints * 100f / totalScoreSum);
                secondarySum += scores[index];
            }

            scores[bestPlayerIndex] += 100 - secondarySum;
            GameManager.Instance.jeuSuivant(scores);
        }
    }
}