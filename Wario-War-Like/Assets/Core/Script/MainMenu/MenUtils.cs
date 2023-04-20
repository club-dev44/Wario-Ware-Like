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
            if (GameManager.Instance != null) {
                Destroy(GameManager.Instance.gameObject);
            }
            if(PlayerConfigurationManager.Instance != null) {
                Destroy(PlayerConfigurationManager.Instance.gameObject);
            }
            SceneManager.LoadScene("Menus");
        }

        public void play()
        {
            if (PlayerConfigurationManager.Instance.AllPlayersReady)
            {
                SceneManager.LoadScene(LOADINGSCENENAME);
            }

        }

        public void enableJoiningForPlayerManager()
        {
            PlayerConfigurationManager.Instance.EnableJoining();
        }

        public void clearPlayers()
        {
            PlayerConfigurationManager.Instance.ResetConfiguration();
        }
    }
}
