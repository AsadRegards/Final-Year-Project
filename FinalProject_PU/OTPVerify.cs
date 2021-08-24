using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using FinalProject_PU.Control;
using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace FinalProject_PU
{
    [Activity(Label = "OTPVerify",NoHistory =true)]
    public class OTPVerify : Activity
    {
        EditText otp, eedt1;
        ImageView imgsubmit;
        
        TextView ttv2;
        Typeface tf;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.OTPVerify);
            otp = (EditText)FindViewById(Resource.Id.eedt1);

           

            imgsubmit = (ImageView)FindViewById(Resource.Id.imgbtn);
            imgsubmit.Click += Imgsubmit_Click;

            eedt1 = (EditText)FindViewById(Resource.Id.eedt1);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            eedt1.SetTypeface(tf, TypefaceStyle.Bold);

            ttv2 = (TextView)FindViewById(Resource.Id.ttv2);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            ttv2.SetTypeface(tf, TypefaceStyle.Bold);
        }

        private void Imgsubmit_Click(object sender, EventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerAsync();
            
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
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            int codets = JsonConvert.DeserializeObject<int>(Intent.GetStringExtra("codetotest"));
            string useremail = JsonConvert.DeserializeObject<string>(Intent.GetStringExtra("email"));

            Account.enter_new_password(useremail, codets, otp, this);
        }
    }
}