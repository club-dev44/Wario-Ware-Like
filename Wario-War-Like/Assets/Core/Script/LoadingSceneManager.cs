using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Core
{

    public class LoadingSceneManager : MonoBehaviour
    {

        private GameManager gameManager;
        private AsyncOperation loadingSceneOperation;
        [SerializeField]
        private Slider loaderFill;

        [SerializeField] private NotificationManager notificationManager;
        [SerializeField] private TMP_Text textConsigne;

        private float startLoadingTimeStamp;
        private void Start()
        {
            gameManager = GameManager.Instance;
            StartCoroutine(LoadingTime());
            PlayerConfigurationManager.Instance.resetCurrentActionMapOfAllPlayersInput();
        }


        /// <summary>
        /// Load the next game scene and wait for 3 seconds before activating it.
        /// The time is to let the player see the description of the game.
        /// </summary>
        IEnumerator LoadingTime()
        {
            yield return null;

            startLoadingTimeStamp = Time.time;
            try
            {
                loadingSceneOperation = gameManager.LoadNextGameAsync();
                textConsigne.text = gameManager.jeuxChoisi[gameManager.CurrentGameIndex].consigne;
            }
            catch (GamesGenerationException e)
            {
                notificationManager.AddNotification(e.Message, NotificationType.ERROR);
                SceneManager.LoadScene(0);
                yield break;
            }
            while (!loadingSceneOperation.isDone)
            {
                loaderFill.value = Mathf.Lerp(loaderFill.value, loadingSceneOperation.progress, Time.deltaTime / 5);
                if (loadingSceneOperation.progress >= 0.9f)
                    break;
                yield return null;
            }
            loaderFill.value = 0.99f;
            yield return new WaitForSeconds(3.0f - (Time.time - startLoadingTimeStamp));
            loadingSceneOperation.allowSceneActivation = true;
            
        }

    }
}
