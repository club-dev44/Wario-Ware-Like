using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHandler : MonoBehaviour
{
    PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    public void IsReady()
    {
        PlayerConfigurationManager.Instance.PlayerReady(playerInput.playerIndex);
    }
}
