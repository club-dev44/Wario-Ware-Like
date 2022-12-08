using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core
{
    public class NotificationManager : MonoBehaviour
    {
        
        public static NotificationManager LastInstanceCreated { get; private set; }

        
        private void Awake() {
            LastInstanceCreated = this;
        }
        

        [SerializeField] private Vector3 positionNewPopUp;
        [SerializeField] private Vector3 scaleNewPopUp;
        
        [SerializeField] private Object infoPopUpPrefab;
        [SerializeField] private Object warningPopUpPrefab;
        [SerializeField] private Object errorPopUpPrefab;


        public void addNotification(string message, NotificationType notificationType = NotificationType.INFO) {
            GameObject popUp;
            switch (notificationType) {
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
            popUp.GetComponent<popUpManager>().setNotificationText(message);
        }

    }
}