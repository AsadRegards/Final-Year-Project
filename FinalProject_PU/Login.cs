using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Widget;
using FinalProject_PU.Control;
using FinalProject_PU.Model;
using MohammedAlaa.GifLoading;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Net.Http;
using System.Threading;
using Xamarin.Essentials;

namespace FinalProject_PU
{
    [Activity(Label = "Login")]
    public class Login : Activity,IWifiAccess
    {
        EditText email;
        LoadingView loadingIndicator, loginbuttonindicator;
        ImageView imglogin, register;
        TextView forgotpass, emailtv, passtv, heading;
        BackgroundWorker worker = new BackgroundWorker();
        TextInputEditText pass;
        Typeface tf;
        

        //public override void OnBackPressed()
        //{
        //    ViewFunctions.HideCircleProgessbar(circle5);
        //}
        protected override void OnCreate(Bundle savedInstanceState)
        {
            
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.LoginActivity);
            register = (ImageView)FindViewById(Resource.Id.imageView9);

            register.Click += Register_Click;

           
            email = (EditText)FindViewById(Resource.Id.edtEmail);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            email.SetTypeface(tf, TypefaceStyle.Normal);
            email.Click += Email_Click;


            pass = (TextInputEditText)FindViewById(Resource.Id.editpass);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            pass.SetTypeface(tf, TypefaceStyle.Normal);

            forgotpass = (TextView)FindViewById(Resource.Id.tvForgotPass);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            forgotpass.SetTypeface(tf, TypefaceStyle.Bold);
            forgotpass.Click += Forgotpass_Click;

            imglogin = (ImageView)FindViewById(Resource.Id.imglogin);
            imglogin.Click += Imglogin_Click;

            emailtv = (TextView)FindViewById(Resource.Id.textView2);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            emailtv.SetTypeface(tf, TypefaceStyle.Normal);
            
           

            passtv = (TextView)FindViewById(Resource.Id.textView3);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            passtv.SetTypeface(tf, TypefaceStyle.Normal);

            heading = (TextView)FindViewById(Resource.Id.textView1);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            heading.SetTypeface(tf, TypefaceStyle.Normal);

            loadingIndicator = FindViewById<LoadingView>(Resource.Id.loading_view);
            loginbuttonindicator = FindViewById<LoadingView>(Resource.Id.loading_view_button);

        }

        private void Email_Click(object sender, EventArgs e)
        {

        }

        private void Forgotpass_Click(object sender, EventArgs e)
        {
            // circle5.Visibility = Android.Views.ViewStates.Visible;

           
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerAsync();

            
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            var duration = TimeSpan.FromMilliseconds(100);
            Vibration.Vibrate(duration);
            var i = new Intent(this, typeof(ForgotPassActivity));
            this.StartActivity(i);
        }

        private void Imglogin_Click(object sender, EventArgs e)
        {
            email.Enabled = false;
            pass.Enabled = false;
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork1;
            worker.RunWorkerAsync();
           
           
        }

        private async void Worker_DoWork1(object sender, DoWorkEventArgs e)
        {
            var duration = TimeSpan.FromMilliseconds(100);
            Vibration.Vibrate(duration);
           
            if (Android.Util.Patterns.EmailAddress.Matcher(email.Text).Matches() == true)
            {
                if (InputValidation.ValidatePassword(pass.Text, pass.Text, this))
                {
                    MainThread.BeginInvokeOnMainThread(() => {
                        loadingIndicator.Visibility = Android.Views.ViewStates.Visible;
                        //loginbuttonindicator.Visibility = Android.Views.ViewStates.Visible;
                    });

                    try
                    {
                        if(CheckWifiStatus())
                        {
                            var user = await Account.UserLogin(email.Text, pass.Text.GetHashCode(), this);
                            if (user == null)
                            {
                                MainThread.BeginInvokeOnMainThread(() =>
                                {
                                    Toast.MakeText(this, "Invalid login credintials provided!", ToastLength.Long).Show();
                                    int hash = pass.Text.GetHashCode();
                                    loadingIndicator.Visibility = Android.Views.ViewStates.Gone;
                                    loginbuttonindicator.Visibility = Android.Views.ViewStates.Gone;
                                });
                            }

                            else
                            {
                                var iss = new Intent(this, typeof(FragmentHomeActivity));
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
                                loadingIndicator.Visibility = Android.Views.ViewStates.Gone;
                                email.Enabled = true;
                                pass.Enabled = true;
                            });
                        }
                        
                       
                    }
                    catch(Exception ex)
                    {
                        MainThread.BeginInvokeOnMainThread(() => 
                        {
                            Toast.MakeText(this, "Please Connect to the Internet| No Internet Connection", ToastLength.Long).Show();
                            loadingIndicator.Visibility = Android.Views.ViewStates.Gone;
                            email.Enabled = true;
                            pass.Enabled = true;
                        });
                                            }
                    
                    
                  
                }
               

                else
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        Toast.MakeText(this, "PLEASE ENTER CORRECT PASSWORD", ToastLength.Long).Show();
                        loadingIndicator.Visibility = Android.Views.ViewStates.Gone;
                        email.Enabled = true;
                        pass.Enabled = true;
                    });
                    
                }
            }
            else
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Toast.MakeText(this, "PLEASE ENTER CORRECT EMAIL ADDRESS", ToastLength.Long).Show();
                    loadingIndicator.Visibility = Android.Views.ViewStates.Gone;
                    email.Enabled = true;
                    pass.Enabled = true;
                });
            }
            
         

           
        }

        private async void FetchandSendTokenWithEmail()
        {
            ISharedPreferences sharedPreferences = Application.Context.GetSharedPreferences("tokenfile", FileCreationMode.Private);
            string token=sharedPreferences.GetString("fcmtoken", string.Empty);
            if(token!=string.Empty)
            {
                HttpClient Client = new HttpClient();
                var uri = Control.Account.BaseAddressUri + "/api/pushnotification/savetoken/?token=" + token+"&userid="+UserInfoHolder.User_id;
                var response = await Client.PostAsync(uri, null);
            }
        }

        private void Register_Click(object sender, EventArgs e)
        {
            BackgroundWorker signupworker = new BackgroundWorker();
            signupworker.DoWork += Signupworker_DoWork;
            signupworker.RunWorkerAsync();
          
           
           

        }

        private void Signupworker_DoWork(object sender, DoWorkEventArgs e)
        {
            var duration = TimeSpan.FromMilliseconds(100);
            Vibration.Vibrate(duration);
            var i = new Intent(this, typeof(Register1Activity));

            this.StartActivity(i);
        }

        //To correct this method, not working as expected
        public bool CheckWifiStatus()
        {

            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                
                return true;
            }

            return true; //To return false but set true temperory
        }
    }
}