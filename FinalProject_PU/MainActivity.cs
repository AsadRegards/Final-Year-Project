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
using FinalProject_PU.Control;
using Xamarin.Essentials;
using System.Net.Http;

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
            ISharedPreferences sharedPreferences = Application.Context.GetSharedPreferences("loginfile", FileCreationMode.Private);
            string email = sharedPreferences.GetString("email", string.Empty);
            string password = sharedPreferences.GetString("password", string.Empty);
            if(string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                Log.Debug(TAG, "Performing some startup work that takes a bit of time.");
                await Task.Delay(1000); // Simulate a bit of startup work.
                Log.Debug(TAG, "Startup work is finished - starting MainActivity.");
                StartActivity(new Intent(Application.Context, typeof(Login)));
            }
            else
            {
                Login login = new Login();
                if (login.CheckWifiStatus())
                {
                    var user = await Account.UserLogin(email,Convert.ToInt32(password));
                    if (user == null)
                    {
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            Toast.MakeText(this, "Invalid login credintials provided!", ToastLength.Long).Show();
                            StartActivity(new Intent(Application.Context, typeof(Login)));



                        });
                    }

                    else
                    {
                        var iss = new Intent(this, typeof(FragmentHomeActivity));
                        ISharedPreferences sharedPrefrences = Application.Context.GetSharedPreferences("loginfile", FileCreationMode.Private);
                        ISharedPreferencesEditor spEdit = sharedPrefrences.Edit();
                        spEdit.PutString("email", user.email_address);
                        spEdit.PutString("password", user.password_hash.ToString());
                        spEdit.Apply();
                        UserInfoHolder.FetchUserInfo(user);
                        FetchandSendTokenWithEmail();
                        MainThread.BeginInvokeOnMainThread(() => {

                            this.StartActivity(iss);
                            // circle5.Visibility = Android.Views.ViewStates.Visible;
                            this.Finish();

                        });

                    }
                }
                else
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        Toast.MakeText(this, "Please connect to Wifi", ToastLength.Long).Show();
                        StartActivity(new Intent(Application.Context, typeof(Login)));

                    });
                }
            }
        }

        private async void FetchandSendTokenWithEmail()
        {
            ISharedPreferences sharedPreferences = Application.Context.GetSharedPreferences("tokenfile", FileCreationMode.Private);
            string token = sharedPreferences.GetString("fcmtoken", string.Empty);
            if (token != string.Empty)
            {
                HttpClient Client = new HttpClient();
                var uri = Control.Account.BaseAddressUri + "/api/pushnotification/savetoken/?token=" + token + "&userid=" + UserInfoHolder.User_id;
                var response = await Client.PostAsync(uri, null);
            }
        }

    }
}