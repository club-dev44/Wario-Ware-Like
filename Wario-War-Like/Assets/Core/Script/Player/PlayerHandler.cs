using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHandler : MonoBehaviour
{
    [SerializeField]
    private PlayerInput playerInput;

    private InputAction readyPlayerInput;
    private bool ready = false;

    private void Awake() {
        ready = false;
        readyPlayerInput = playerInput.currentActionMap.FindAction("Ready");
        readyPlayerInput.performed += OnReady;
    }

    public void OnReady(InputAction.CallbackContext context)
    {
        PlayerConfigurationManager.Instance.PlayerReady(playerInput.playerIndex);
        ready = true;
        readyPlayerInput.performed -= OnReady;
    }
}
