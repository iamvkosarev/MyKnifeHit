using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;


namespace KnifeHit.Core
{
    public class AndroidNotificationController : MonoBehaviour
    {
        [SerializeField] private GameProperies _gameProperies;
        public static AndroidNotificationController instance = null;

        private AndroidNotificationChannel _channel;
        private string _CHANNEL_ID = "channel_id";
        private int _lastIdentifier = -1;
        private AndroidNotification _notification;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance == this)
            {
                Destroy(gameObject);
            }

            UpdateNotificationData();
        }

        private void UpdateNotificationData()
        {
            // Create a notification channel
            _channel = new AndroidNotificationChannel()
            {
                Id = _CHANNEL_ID,
                Name = "Default Channel",
                Importance = Importance.High,
                Description = "Generic notifications",
            };
            AndroidNotificationCenter.RegisterNotificationChannel(_channel);
            _notification = new AndroidNotification();

        }

        public void CreatNotification()
        {
            CancelNotification();
            SetNotificationData();
            _lastIdentifier = AndroidNotificationCenter.SendNotification(_notification, _CHANNEL_ID);
        }

        private void SetNotificationData()
        {

            _notification.Title = _gameProperies.notificationTitle;
            _notification.Text = _gameProperies.notificationText;
            _notification.FireTime = System.DateTime.Now.AddMinutes(_gameProperies.notificationTimeDelay);
        }

        public void CancelNotification()
        {
            var notificationIntentData = AndroidNotificationCenter.GetLastNotificationIntent();

            if (notificationIntentData != null)
            {
                int lastNotificationIdentifier = notificationIntentData.Id;
                AndroidNotificationCenter.CancelNotification(lastNotificationIdentifier);
            }
            if(_lastIdentifier != -1)
            {
                AndroidNotificationCenter.CancelNotification(_lastIdentifier);
            }
        }

    }
}

