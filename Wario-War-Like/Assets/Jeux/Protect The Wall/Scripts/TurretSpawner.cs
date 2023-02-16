using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProtectTheWall
{
    public class TurretSpawner : MonoBehaviour
    {
        [SerializeField]
        private ProtectTheWallManger gameManager;
        [SerializeField]
        private GameObject playerPrefab;
        [SerializeField]
        private Transform leftPosition, rightPosition, bulletBag;
        [SerializeField]
        private float yPosition;

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
            foreach (PlayerConfiguration player in PlayerConfigurationManager.Instance.PlayerConfigurations)
            {
                AddPlayer(player);
            }
        }

        private void AddPlayer(PlayerConfiguration playerConf)
        {
            GameObject _player = Instantiate(playerPrefab, transform);
            float _playerXPosition = leftPosition.position.x;
            float _step = DistanceBetweenBorder / (transform.childCount + 1);
            foreach (Transform child in transform)
            {
                _playerXPosition += _step;
                child.position = new Vector3(_playerXPosition, yPosition);
            }
            TurretController _controller = _player.GetComponent<TurretController>();
            _controller.playerConfiguration = playerConf;
            _controller.gameManager = gameManager;
            _controller.BulletBag = bulletBag;
            gameManager.AddPlayer(_controller);
            playerConf.Input.SwitchCurrentActionMap("ProtectTheWallActionMap");
        }
    }
}