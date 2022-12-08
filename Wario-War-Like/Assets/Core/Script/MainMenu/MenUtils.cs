using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    
public class MenUtils : MonoBehaviour
{

    const string LOADINGSCENENAME = "LoadingScene";

    public void Quit()
    {
        Application.Quit();
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Menus");
    }

    public void play() {
        if (PlayerConfigurationManager.Instance.AllPlayersReady) {
            SceneManager.LoadScene(LOADINGSCENENAME);
        }

    }

    public void enableJoiningForPlayerManager() {
        PlayerConfigurationManager.Instance.enableJoining();
    }

    public void clearPlayers() {
        PlayerConfigurationManager.Instance.reset();
    }
}
}
