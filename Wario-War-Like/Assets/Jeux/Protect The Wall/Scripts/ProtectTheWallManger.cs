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
            foreach (TurretController player in players)
            {
                totalScoreSum += player.Points;
            }
            int[] scores = new int[players.Count];
            int bestPlayerIndex = 0;
            int checkScore = 0;
            for (int i = 0; i < scores.Length; i++)
            {
                scores[i] = players[i].Points * 100 / totalScoreSum;
                if (players[i].Points >= 100)
                {
                    bestPlayerIndex= i;
                }
                checkScore += scores[i];
            }
            if (checkScore < 100)
                scores[bestPlayerIndex];OSKOUR
            GameManager.Instance.jeuSuivant(scores);
        }
    }
}