using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using MohammedAlaa.GifLoading;
using System;
using Account = FinalProject_PU.Control.Account;

namespace FinalProject_PU
{
    [Activity(Label = "ForgotPassActivity",NoHistory =true)]
    public class ForgotPassActivity : Activity
    {
        EditText email; 
        ImageView imgbtn;
        
        TextView textv1, textv2;
        Typeface tf;
        LoadingView loader;
        protected  override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ForgotPASS);

            loader = FindViewById<LoadingView>(Resource.Id.loading_view);

            imgbtn = FindViewById<ImageView>(Resource.Id.imagbtn);
            email = FindViewById<EditText>(Resource.Id.edit1);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            email.SetTypeface(tf, TypefaceStyle.Bold);

            imgbtn.Click += async delegate
            {
                loader.Visibility = Android.Views.ViewStates.Visible;
                imgbtn.Enabled = false;
                email.Enabled = false;
                try
                {
                    await Account.forgotton_password_functionality(email.Text, this);
                }
                catch (Exception e)
                {

                }
                finally
                {
                    loader.Visibility = Android.Views.ViewStates.Visible;
                    imgbtn.Enabled = true;
                    email.Enabled = true;
                }
                
                
            };
            
            
            textv1 = (TextView)FindViewById(Resource.Id.textv1);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            textv1.SetTypeface(tf, TypefaceStyle.Bold);

            textv2 = (TextView)FindViewById(Resource.Id.textv2);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            textv2.SetTypeface(tf, TypefaceStyle.Bold);

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

    }
}