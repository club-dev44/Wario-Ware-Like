using UnityEngine;
using UnityEngine.InputSystem;

namespace Core
{

    public class PlayerHandler : MonoBehaviour
    {
        [SerializeField]
        private PlayerInput playerInput;

        private InputAction readyPlayerInput;

        private void Awake()
        {
            readyPlayerInput = playerInput.currentActionMap.FindAction("Ready");
            readyPlayerInput.performed += OnReady;
        }

        public void OnReady(InputAction.CallbackContext context)
        {
            PlayerConfigurationManager.Instance.PlayerReady(playerInput.playerIndex);
            readyPlayerInput.performed -= OnReady;
        }
    }
}
