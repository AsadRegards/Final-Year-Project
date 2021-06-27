using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Media;
using Android.OS;
using AndroidX.Core.App;
using Firebase.Messaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;

namespace FinalProject_PU.PushNotification
{
    [Service()]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    class MessagingService : FirebaseMessagingService
    {
        private readonly string NOTIFICATION_CHANNEL_ID = "com.companyname.finalproject_pu";

        public override void OnNewToken(string p0)
        {
            base.OnNewToken(p0);
            SendTokenToSharedPrefrences(p0);
        }

        private void SendTokenToSharedPrefrences(string refreshedToken)
        {
            

            //Sending FCM TOKEN to shared preferences
            ISharedPreferences sharedPrefrences = Application.Context.GetSharedPreferences("tokenfile", FileCreationMode.Private);
            ISharedPreferencesEditor spEdit = sharedPrefrences.Edit();
            spEdit.PutString("fcmtoken", refreshedToken);
            spEdit.Apply();


        }

        public override void OnMessageReceived(RemoteMessage message)
        {
            //when we recieve msg from fcm first we check custom data , if it is not empty , show this data,
            //else just show tittle in body

            if (!message.Data.GetEnumerator().MoveNext())
                SendNotification(message.GetNotification().Title, message.GetNotification().Body);
            else
                SendNotification(message.Data);


        }

        private void SendNotification(IDictionary<string, string> data)
        {
            data.TryGetValue("title", out string title);
            data.TryGetValue("body", out string body);
            SendNotification(title, body);
        }

        private void SendNotification(string title, string body)
        {
            // Set custom push notification sound.
            Android.Net.Uri alarmUri = Android.Net.Uri.Parse($"{ContentResolver.SchemeAndroidResource}://{this.Application.ApplicationContext.PackageName}/{Resource.Raw.notificationsound}");
            var alarmAttributes = new AudioAttributes.Builder()
               .SetContentType(AudioContentType.Sonification)
               .SetUsage(AudioUsageKind.Notification).Build();
            //custom sounds end here

            try
            {
                var intent = new Intent(Application.Context, typeof(MainActivity));
                intent.AddFlags(ActivityFlags.ClearTop);
                var pendingIntent = PendingIntent.GetActivity(Application.Context, 0, intent, PendingIntentFlags.OneShot);

                NotificationManager notificationmanager = (NotificationManager)GetSystemService(Context.NotificationService);
                if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
                {
                    NotificationChannel notificationchannel = new NotificationChannel(NOTIFICATION_CHANNEL_ID, "Notification Channel", Android.App.NotificationImportance.Max);
                    notificationchannel.Description = "ProblemUpdate Channel";
                    notificationchannel.EnableLights(true);
                    notificationchannel.LightColor = Android.Graphics.Color.Blue;
                    notificationchannel.SetSound(alarmUri, alarmAttributes);
                    notificationmanager.CreateNotificationChannel(notificationchannel);


                }
                NotificationCompat.Builder notificationbuilder = new NotificationCompat.Builder(this, NOTIFICATION_CHANNEL_ID);

                Android.Graphics.Bitmap bitmap = BitmapFactory.DecodeResource(Resources, Resource.Drawable.appiconfinal);

                Android.Net.Uri alar_mUri = Android.Net.Uri.Parse($"{ContentResolver.SchemeAndroidResource}://{this.Application.ApplicationContext.PackageName}/{Resource.Raw.notificationsound}");
                notificationbuilder.SetAutoCancel(true)
                    .SetWhen(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds())
                    .SetContentTitle(title)
                    .SetContentText(body)
                    .SetSmallIcon(Resource.Drawable.r_myicon)
                    .SetLargeIcon(bitmap)
                    .SetSound(alar_mUri)
                    .SetContentInfo("info")
                    .SetContentIntent(pendingIntent);

                notificationmanager.Notify(new Random().Next(), notificationbuilder.Build());
            }
            catch(Exception)
            {

            }
         
        }

    }
}