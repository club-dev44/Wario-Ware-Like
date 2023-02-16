using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProtectTheWall
{
    public class EnemiesSpawner : MonoBehaviour
    {
        [SerializeField]
        private ProtectTheWallManger gameManager;
        [SerializeField]
        private Transform leftPosition, rightPosition;
        [SerializeField]
        private float yPosition;

        [SerializeField]
        private float spawnRatePerPlayer;

        private float DistanceBetweenBorder
        {
            get => Mathf.Abs(leftPosition.position.x - rightPosition.position.x);
        }

        private void Start()
        {
            GameManager.Instance.subscribeToStartGame(StartGame);
        }

        public void StartGame()
        {
            StartCoroutine(nameof(MobSpawner));
        }

        private IEnumerator MobSpawner()
        {
            yield return new WaitForSeconds(3);
            while(true)
            {
                SpawnMob();
                yield return new WaitForSeconds(spawnRatePerPlayer / PlayerConfigurationManager.Instance.inputManager.playerCount);
            }
        }

        private void SpawnMob()
        {
            Instantiate(gameManager.GetAMobToSpawn(), new Vector3(Random.Range(leftPosition.position.x, rightPosition.position.x), yPosition), Quaternion.identity, transform);
        }
    }

}