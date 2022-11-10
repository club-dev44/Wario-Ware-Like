using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHandler : MonoBehaviour
{
    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        InputAction _ready = playerInput.currentActionMap.FindAction("Ready");
        _ready.performed += IsReady;
    }

    public void IsReady(InputAction.CallbackContext context)
    {
        PlayerConfigurationManager.Instance.PlayerReady(playerInput.playerIndex);
    }
}
