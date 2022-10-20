using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerDisplay : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> players = new List<GameObject>(4);

    private void Start()
    {
        PlayerConfigurationManager.Instance.playerReady += OnPlayerReady;
    }

    public void OnPlayerJoin(PlayerInput playerInput)
    {
        players[playerInput.playerIndex].GetComponent<Image>().color = Color.gray;
    }

    public void OnPlayerReady(int playerIndex)
    {
        players[playerIndex].GetComponent<Image>().color = Color.green;
    }
}
