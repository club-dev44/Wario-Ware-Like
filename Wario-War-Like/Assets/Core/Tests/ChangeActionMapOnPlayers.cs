using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core
{
    public class ChangeActionMapOnPlayers : MonoBehaviour
    {
        [SerializeField]
        public InputActionReference actionMap;
        public void changeActionMapOnPlayers() {
            PlayerConfigurationManager playerConfigurationManager = PlayerConfigurationManager.Instance;
            foreach (PlayerConfiguration playerConfiguration in playerConfigurationManager.PlayerConfigurations) {
                playerConfiguration.Input.currentActionMap = actionMap.action.actionMap;
            }
        }
    }
}