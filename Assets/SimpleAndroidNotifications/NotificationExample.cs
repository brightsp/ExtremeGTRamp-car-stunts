using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Assets.SimpleAndroidNotifications
{
    public class NotificationExample : MonoBehaviour
    {
        public void Rate()
        {
            Application.OpenURL("http://u3d.as/y6r");
        }

        public void OpenWiki()
        {
            Application.OpenURL("https://github.com/hippogamesunity/SimpleAndroidNotificationsPublic/wiki");
        }

        public void ScheduleSimple()
        {
            NotificationManager.Send(TimeSpan.FromSeconds(5), "Simple notification", "Customize icon and color", new Color(1, 0.3f, 0.15f));
        }

        public void ScheduleNormal()
        {
            NotificationManager.SendWithAppIcon(TimeSpan.FromSeconds(30), "Notification", "Notification with app icon", new Color(1,1,0), NotificationIcon.Star); ;

            StartCoroutine(sendnext());
        }

        IEnumerator sendnext()
        {
            yield return new WaitForSeconds(5);
            NotificationManager.SendWithAppIcon(TimeSpan.FromSeconds(60), "Notification2", "Notification with app icon", new Color(1, 0.3f, 0.15f), NotificationIcon.Bell); ;


            yield return new WaitForSeconds(5);
            NotificationManager.SendWithAppIcon(TimeSpan.FromSeconds(90), "Notification3", "Notification with app icon", new Color(0, 0.5f, 0), NotificationIcon.Power); ;

        }

        public void ScheduleCustom()
        {
            ScheduleCustom(1);
            ScheduleCustom(2);
            ScheduleCustom(5);
        }
        public void ScheduleCustom(int numm)
        {

            DateTime timetonotify = DateTime.Now.AddHours(numm);
            TimeSpan time = timetonotify - DateTime.Now;

            Debug.Log("numm  "+time);
            var notificationParams = new NotificationParams
            {


                Id = UnityEngine.Random.Range(0, int.MaxValue),
                Delay = time,
                Title = "Custom notification",
                Message = ("Message "+numm),
                Ticker = "Ticker",
                Sound = true,
                Vibrate = true,
                Light = true,
                SmallIcon = NotificationIcon.Star,
                SmallIconColor = new Color(0, 0.5f, 0),
                LargeIcon = "app_icon"
            };

            NotificationManager.SendCustom(notificationParams);
        }

        public void CancelAll()
        {
            NotificationManager.CancelAll();
        }
    }
}