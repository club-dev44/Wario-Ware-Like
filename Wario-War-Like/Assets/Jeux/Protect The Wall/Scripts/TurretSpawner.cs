using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProtectTheWall
{
    public class TurretSpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject playerPrefab;
        [SerializeField]
        private Transform LeftPosition, RightPosition;
        [SerializeField]
        private float yPosition;

        private float distanceBetweenBorder
        {
            get => Mathf.Abs(LeftPosition.position.x - RightPosition.position.x);
        }

        private void Start()
        {
            GameManager.Instance.subscribeToStartGame(StartGame);
        }

        public void StartGame()
        {
            foreach (PlayerConfiguration player in PlayerConfigurationManager.Instance.PlayerConfigurations)
            {
                AddPlayer(player);
            }
        }

        private void AddPlayer(PlayerConfiguration playerConf)
        {
            GameObject _player = Instantiate(playerPrefab, transform);
            float _playerPosition = LeftPosition.position.x;
            float _step = distanceBetweenBorder / (transform.childCount + 1);
            foreach (Transform child in transform)
            {
                _playerPosition += _step;
                child.position = new Vector3(_playerPosition, yPosition);
            }
            _player.GetComponent<TurretController>().playerConfiguration = playerConf;
            playerConf.Input.SwitchCurrentActionMap("ProtectTheWallActionMap");
        }
    }
}