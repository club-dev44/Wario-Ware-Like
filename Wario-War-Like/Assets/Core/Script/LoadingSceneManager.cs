using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        private float startLoadingTimeStamp;
        private void Awake()
        {
            gameManager = GameManager.Instance; 
            StartCoroutine(tempDeChargement());
            PlayerConfigurationManager.Instance.resetCurrentActionMapOfAllPlayersInput();
        }

        private float startLoadingTimeStamp;
        private void Start()
        {
            gameManager = GameManager.Instance;
            StartCoroutine(tempDeChargement());
        }



        IEnumerator tempDeChargement()
        {
            yield return null;

            startLoadingTimeStamp = Time.time;
            try
            {
                loadingSceneOperation = gameManager.chargerProchainJeuxAsync();
            }
            catch (GamesGenerationException e)
            {
                notificationManager.addNotification(e.Message, NotificationType.ERROR);
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
