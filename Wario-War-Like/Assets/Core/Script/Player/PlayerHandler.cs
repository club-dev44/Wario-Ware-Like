using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core
{
    
public class PlayerHandler : MonoBehaviour
{
    [SerializeField]
    private PlayerInput playerInput;

    private void Awake()
    {
        InputAction _ready = playerInput.currentActionMap.FindAction("Ready");
        _ready.performed += IsReady;
    }

    public void IsReady(InputAction.CallbackContext context)
    {
        PlayerConfigurationManager.Instance.PlayerReady(playerInput.playerIndex);
    }
}
}
