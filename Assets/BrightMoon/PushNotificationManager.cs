using UnityEngine;
using Unity.Notifications.Android;
using System.Collections.Generic;
using System;



#if UNITY_ANDROID
using UnityEngine.Android;
#endif
public class PushNotificationManager : MonoBehaviour
{
    public void Start()
    {

#if UNITY_ANDROID && !UNITY_EDITOR

        // Android 13+
        if (AndroidVersion() >= 33)
        {
            if (!Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS"))
            {
                Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
            }
        }

#endif


        Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
        Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;


        UnityNotificationInit();
    }


    int AndroidVersion()
    {
        using (AndroidJavaClass version = new AndroidJavaClass("android.os.Build$VERSION"))
        {
            return version.GetStatic<int>("SDK_INT");
        }
    }


    public void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
    {
        UnityEngine.Debug.Log("Received Registration Token: " + token.Token);
    }

    public void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e)
    {
        UnityEngine.Debug.Log("Received a new message from: " + e.Message.From);
    }

    ///
    /// 
    /// 
    void UnityNotificationInit()
    {
        var channel = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Default Channel",
            Importance = Importance.Default,
            Description = "Generic notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);

        if (PlayerPrefs.HasKey("Mfrstinstall_notify") == false)
        {

            PlayerPrefs.SetString("Mfrstinstall_notify", "done");
            ScheduleNotifications();

        }
    }


    public List<String> TitleTxts = new List<string>();
    public List<String> DescriptinTxts = new List<string>();
    public void ScheduleNotifications()
    {
        // Clear previously scheduled notifications
        AndroidNotificationCenter.CancelAllScheduledNotifications();

        for (int day = 1; day <= 10; day++)
        {
            int index = UnityEngine.Random.Range(0, TitleTxts.Count);

            var notification = new AndroidNotification
            {
                Title = TitleTxts[index],
                Text = DescriptinTxts[index],

                // Send every day at 7:00 PM
                FireTime = DateTime.Now.Date
                    .AddDays(day)
                    .AddHours(19),

                SmallIcon = "ic_stat_notification",

                ShouldAutoCancel = true
            };

            AndroidNotificationCenter.SendNotificationWithExplicitID(
                notification,
                "channel_id",
                day);      // Unique ID (1..10)
        }

        Debug.Log("Scheduled 10 daily notifications.");
    }
}
