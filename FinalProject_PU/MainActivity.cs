using Android.App;
using Android.Content;
using Android.Nfc;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FinalProject_PU.Model;
using Android.Content.PM;
using Android;
using AndroidX.Core.Content;

namespace FinalProject_PU
{
    //
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme",MainLauncher =true)]
    public class MainActivity : Activity
    {
        static readonly string TAG = "X:" + typeof(MainActivity).Name;
        int userId, issueId;
        Issue IssueObj;
        User UserObj;

        private static int REQUEST_EXTERNAL_STORAGE = 1;
        private static String[] PERMISSIONS_STORAGE = {
        Android.Manifest.Permission.ReadExternalStorage,
        Android.Manifest.Permission.WriteExternalStorage};

        public void verifyStoragePermissions(Activity activity)
        {
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.WriteExternalStorage) == (int)Permission.Granted)
            {
                // We have permission, go ahead and use the camera.
            }
            else
            {
                RequestPermissions(PERMISSIONS_STORAGE, REQUEST_EXTERNAL_STORAGE); 
            }
        }

        //public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        //{
        //    base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        //}
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
           // verifyStoragePermissions(this);
            // Create your application here
            SetContentView(Resource.Layout.ActivityMain);
            
            if (Intent.Extras != null)
            {
                var userid = Intent.GetStringExtra("userid");
                var issueid = Intent.GetStringExtra("issueid");
                if(userid!=null && issueid!=null)
                {
                    userId = JsonConvert.DeserializeObject<int>(userid);
                    issueId = JsonConvert.DeserializeObject<int>(issueid);
                    BackgroundWorker worker = new BackgroundWorker();
                    worker.DoWork += Worker_DoWork;
                    worker.RunWorkerAsync();
                }
                else
                {
                    SimulateStartup();
                }
               

               
            }
            else
            {
                SimulateStartup();
            }
            Log.Debug(TAG, "SplashActivity.OnCreate");
        }

        private async void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
           UserObj = await new Control.IssueOper().GetUserById(userId);
           IssueObj = await new Control.IssueOper().GetIssueById(issueId);

            Intent NBintent = new Intent(this, typeof(NearbyUser));
            NBintent.PutExtra("UserObject", JsonConvert.SerializeObject(UserObj));
            NBintent.PutExtra("IssueObject", JsonConvert.SerializeObject(IssueObj));
            RunOnUiThread(() => {
                StartActivity(NBintent);
            });
            
        }

       

        // Simulates background work that happens behind the splash screen
        async void SimulateStartup()
        {
            Log.Debug(TAG, "Performing some startup work that takes a bit of time.");
            await Task.Delay(3000); // Simulate a bit of startup work.
            Log.Debug(TAG, "Startup work is finished - starting MainActivity.");
            StartActivity(new Intent(Application.Context, typeof(Login)));
        }

    }
}