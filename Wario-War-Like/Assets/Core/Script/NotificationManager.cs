using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core
{
    public class NotificationManager : MonoBehaviour
    {

        private struct Notification
        {
            public string message { get; set; }
            public NotificationType notificationType { get; set; }

            public Notification(string message, NotificationType notificationType)
            {
                this.message = message;
                this.notificationType = notificationType;
            }
        }

        private Queue<Notification> notificationsQueue = new Queue<Notification>();
        private object queueLock = new object();
        public bool displayingNotification = false;
        public static NotificationManager LastInstanceCreated { get; private set; }

        private void Awake()
        {
            LastInstanceCreated = this;
        }

        private void OnEnable()
        {
            Application.logMessageReceived += ApplicationOnlogMessageReceived;
        }

        private void ApplicationOnlogMessageReceived(string condition, string stacktrace, LogType type)
        {
            switch (type)
            {
                case LogType.Error:
                    addNotification(condition, NotificationType.ERROR);
                    break;
                case LogType.Exception:
                    addNotification(condition, NotificationType.ERROR);
                    break;
                case LogType.Warning:
                    addNotification(condition, NotificationType.WARNING);
                    break;
                default:
                    addNotification(condition, NotificationType.INFO);
                    break;
            }
        }

        private void OnDisable()
        {
            Application.logMessageReceived -= ApplicationOnlogMessageReceived;
        }

        [SerializeField] private Object infoPopUpPrefab;
        [SerializeField] private Object warningPopUpPrefab;
        [SerializeField] private Object errorPopUpPrefab;


        public void addNotification(string message, NotificationType notificationType = NotificationType.INFO)
        {
            lock (queueLock)
            {
                notificationsQueue.Enqueue(new Notification(message, notificationType));
                if (!displayingNotification)
                {
                    displayingNotification = true;
                    StartCoroutine(coroutineNotification());
                }
            }
        }


        IEnumerator coroutineNotification()
        {
            while (notificationsQueue.Count > 0)
            {
                Notification notification = notificationsQueue.Dequeue();
                GameObject popUp;
                switch (notification.notificationType)
                {
                    case NotificationType.INFO:
                        popUp = (GameObject)Instantiate(infoPopUpPrefab);
                        break;
                    case NotificationType.WARNING:
                        popUp = (GameObject)Instantiate(warningPopUpPrefab);
                        break;
                    case NotificationType.ERROR:
                        popUp = (GameObject)Instantiate(errorPopUpPrefab);
                        break;
                    default:
                        popUp = (GameObject)Instantiate(infoPopUpPrefab);
                        break;
                }
                DontDestroyOnLoad(popUp);
                popUp.GetComponent<popUpManager>().setNotificationText(notification.message);
                yield return new WaitForSeconds(3);
            }
            displayingNotification = false;
        }

    }
}