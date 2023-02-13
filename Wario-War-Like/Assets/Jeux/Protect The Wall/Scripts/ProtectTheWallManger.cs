using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProtectTheWall
{
    public class ProtectTheWallManger : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> possibleBullets = new();
        [SerializeField]
        private List<GameObject> enemies = new();

        private List<TurretController> players = new();

        public void AddPlayer(TurretController player)
        {
            players.Add(player);
        }

        public GameObject GetCurrentBullet()
        {
            return possibleBullets[Random.Range(0, possibleBullets.Count)]; // changer pour une fonction genre du temps de jeu et du niveau de difficulté
        }

        public GameObject GetAMobToSpawn()
        {
            return enemies[Random.Range(0, enemies.Count)]; // changer pour une fonction genre du temps de jeu et du niveau de difficulté
        }

        internal void FinishGame()
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
            scores[bestPlayerIndex] += totalScoreSum - secondarySum;
            GameManager.Instance.jeuSuivant(scores);
        }
    }
}