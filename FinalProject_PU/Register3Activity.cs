using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FinalProject_PU.Model;
using FinalProject_PU.Control;
using Newtonsoft.Json;
using Xamarin.Essentials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lsjwzh.Widget.MaterialLoadingProgressBar;
using System.ComponentModel;
using Android.Support.Design.Widget;
using MohammedAlaa.GifLoading;

namespace FinalProject_PU
{
    [Activity(Label = "Register3Activity",NoHistory =true)]
    public class Register3Activity : Activity
    {
       
        TextView RegisterHeading, Passtv, Confirmtv;
        ImageView  login, back;
        Typeface tf;
        TextInputEditText pass, confirmPass;
        LoadingView loader, button_loader;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Register3);
            loader = FindViewById<LoadingView>(Resource.Id.loading_view);
            button_loader = FindViewById<LoadingView>(Resource.Id.loading_view_button);

            ImageView go = (ImageView)FindViewById(Resource.Id.imglogin);
            go.Click += Go_Click;

            RegisterHeading = (TextView)FindViewById(Resource.Id.textView1);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            RegisterHeading.SetTypeface(tf, TypefaceStyle.Bold);

            Passtv = (TextView)FindViewById(Resource.Id.textView2);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            Passtv.SetTypeface(tf, TypefaceStyle.Normal);

            Confirmtv = (TextView)FindViewById(Resource.Id.textView3);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            Confirmtv.SetTypeface(tf, TypefaceStyle.Normal);

            pass = (TextInputEditText)FindViewById(Resource.Id.edtPassreg);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            pass.SetTypeface(tf, TypefaceStyle.Normal);

            confirmPass = (TextInputEditText)FindViewById(Resource.Id.edtConfirmPassreg);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            confirmPass.SetTypeface(tf, TypefaceStyle.Bold);




            login = (ImageView)FindViewById(Resource.Id.imageView9);
            login.Click += Login_Click;



        }

        long lastPress;
        public override void OnBackPressed()
        {
            // source https://stackoverflow.com/a/27124904/3814729
            long currentTime = DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond;

            // source https://stackoverflow.com/a/14006485/3814729
            if (currentTime - lastPress > 5000)
            {
                Toast.MakeText(this, "Press back again to exit", ToastLength.Long).Show();
                lastPress = currentTime;
            }
            else
            {

                FinishAffinity();

            }
        }


        private void Login_Click(object sender, EventArgs e)
        {

            var i = new Intent(this, typeof(Login));
            this.StartActivity(i);
        }
        //upload photo ka kam

        private void Go_Click(object sender, EventArgs e)
        {
            if (InputValidation.ValidatePassword(pass.Text, confirmPass.Text, this))
            {
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += Worker_DoWork;
                worker.RunWorkerAsync();
            }



        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {

            User u = JsonConvert.DeserializeObject<User>(Intent.GetStringExtra("usera"));
            int hc = pass.Text.GetHashCode();
            u.password_hash = hc;
            MainThread.BeginInvokeOnMainThread(() =>
            {
                loader.Visibility = Android.Views.ViewStates.Visible;
                button_loader.Visibility = Android.Views.ViewStates.Visible;
                pass.Enabled = false;
                confirmPass.Enabled = false;
                var i = new Intent(this, typeof(PhotoUploading));
                i.PutExtra("userb", JsonConvert.SerializeObject(u));
                this.StartActivity(i);
                loader.Visibility = Android.Views.ViewStates.Gone;
                button_loader.Visibility = Android.Views.ViewStates.Gone;
                pass.Enabled = true;
                confirmPass.Enabled = true;
            });
        }
    }
}