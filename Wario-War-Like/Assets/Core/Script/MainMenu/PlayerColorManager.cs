using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Core
{

    public class PlayerColorManager : MonoBehaviour
    {
        [SerializeField] private int playerindex;
        [SerializeField] private ColorManager colorManager;
        [SerializeField] Image image;

        private PlayerConfiguration playerConfiguration;

        private void Start()
        {
            PlayerConfigurationManager.Instance.inputManager.onPlayerJoined += AddPlayer;
        }

        private void AddPlayer(PlayerInput player)
        {
            if (player.playerIndex != playerindex) { return; }

            playerConfiguration = PlayerConfigurationManager.Instance.PlayerConfigurations[playerindex];
            SubscribeToAction();
        }

        public void NextColor(InputAction.CallbackContext ctxt)
        {
            Color color = colorManager.GetNextColor(image.color);
            image.color = color;
            playerConfiguration.PlayerColor = color;
        }

        public void PreviousColor(InputAction.CallbackContext ctxt)
        {
            Color color = colorManager.GetPreviousColor(image.color);
            image.color = color;
            playerConfiguration.PlayerColor = color;
        }

        private void SubscribeToAction()
        {
            playerConfiguration.Input.actions["NextColor"].performed += NextColor;
            playerConfiguration.Input.actions["PreviousColor"].performed += PreviousColor;
        }
    }
}
