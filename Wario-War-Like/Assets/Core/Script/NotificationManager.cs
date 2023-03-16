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

        /// <summary>
        /// callback for log messages from unity
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="stacktrace"></param>
        /// <param name="type"></param>
        private void ApplicationOnlogMessageReceived(string condition, string stacktrace, LogType type)
        {
            switch (type)
            {
                case LogType.Error:
                    AddNotification(condition, NotificationType.ERROR);
                    break;
                case LogType.Exception:
                    AddNotification(condition, NotificationType.ERROR);
                    break;
                case LogType.Warning:
                    AddNotification(condition, NotificationType.WARNING);
                    break;
                default:
                    AddNotification(condition, NotificationType.INFO);
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

        /// <summary>
        /// add notification manually to the queue and display it if no other notification is displayed
        /// </summary>
        /// <param name="message"></param>
        /// <param name="notificationType"></param>
        public void AddNotification(string message, NotificationType notificationType = NotificationType.INFO)
        {
            lock (queueLock)
            {
                notificationsQueue.Enqueue(new Notification(message, notificationType));
                if (!displayingNotification)
                {
                    displayingNotification = true;
                    StartCoroutine(CoroutineNotification());
                }
            }
        }

        /// <summary>
        /// Unqueue and display the notifications
        /// </summary>
        IEnumerator CoroutineNotification()
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