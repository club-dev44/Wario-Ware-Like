using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Core
{
    
public class PlayerDisplay : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> players = new List<GameObject>(4);

    [SerializeField]
    private List<Color> playersColor = new List<Color>(4);

    
    
    private void Start() {
        PlayerConfigurationManager.Instance.inputManager.onPlayerJoined += OnPlayerJoin;
        PlayerConfigurationManager.Instance.playerReady += OnPlayerReady;
        foreach (PlayerConfiguration playerConfiguration in PlayerConfigurationManager.Instance.PlayerConfigurations) {
            PlayerInput playerInput = playerConfiguration.Input;
            players[playerInput.playerIndex].GetComponent<Image>().color = playersColor[playerInput.playerIndex];
        }
    }

    public void OnPlayerJoin(PlayerInput playerInput)
    {
        players[playerInput.playerIndex].GetComponent<Image>().color = Color.gray;
    }

    public void OnPlayerReady(int playerIndex)
    {
        players[playerIndex].GetComponent<Image>().color = playersColor[playerIndex];
    }

    private void OnDestroy() {
        PlayerConfigurationManager.Instance.inputManager.onPlayerJoined -= OnPlayerJoin;
        PlayerConfigurationManager.Instance.playerReady -= OnPlayerReady;
    }

    public void reset() {
        foreach (var player in players) {
            player.GetComponent<Image>().color =Color.white;
        }
    }
}
}
