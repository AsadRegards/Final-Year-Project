using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FinalProject_PU.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinalProject_PU.Control;
using Android.Graphics;
using Lsjwzh.Widget.MaterialLoadingProgressBar;
using System.ComponentModel;
using Xamarin.Essentials;
using System.Threading.Tasks;
using MohammedAlaa.GifLoading;

namespace FinalProject_PU
{
    [Activity(Label = "EmailVerifyActivity",NoHistory =true)]
    public class EmailVerifyActivity : Activity
    {
        ImageView verifybutton, back;
        EditText codetext;
        TextView heading, textx;
        Typeface tf;
        static int code;
        static User u;
        LoadingView loader;



        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //Create your application here
            SetContentView(Resource.Layout.VerificationEmail);
            loader = FindViewById<LoadingView>(Resource.Id.loading_view);


            //send OTP to email
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerAsync();
            

         

            //UI SETTING CODE FOR GOOD UI
            verifybutton = FindViewById<ImageView>(Resource.Id.imgbtn);
            verifybutton.Click += Verifybutton_Click;

            codetext = FindViewById<EditText>(Resource.Id.edt1);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            codetext.SetTypeface(tf, TypefaceStyle.Bold);

            heading = FindViewById<TextView>(Resource.Id.tv1);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            heading.SetTypeface(tf, TypefaceStyle.Bold);

            textx = FindViewById<TextView>(Resource.Id.tv2);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            textx.SetTypeface(tf, TypefaceStyle.Bold);
            //UI CODE ENDS HERE


        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            u = JsonConvert.DeserializeObject<User>(Intent.GetStringExtra("usertoemail"));
            if(u!=null)
            {
                code = Account.verifyEmail(u.email_address,this);
            }

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



        private async void Verifybutton_Click(object sender, EventArgs e)
        {
            if(codetext.Text!=code.ToString())
            {
                Toast.MakeText(this, "Invalid OTP Code", ToastLength.Long).Show();
            }
            else
            {
                await Task.Run(() =>
                {


                    var intent = new Intent(this, typeof(LocationActivity));
                    intent.PutExtra("userObj", JsonConvert.SerializeObject(u));
                    MainThread.BeginInvokeOnMainThread(() => 
                    {
                        this.StartActivity(intent);
                    });

                });
                
              
            }
        }

    }
}