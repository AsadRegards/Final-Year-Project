using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject_PU
{
    [Activity(Label = "HelpActivity",NoHistory =true)]
    public class HelpActivity : Activity
    {
        TextView tev1, text1, Details, Contact, Email, Contact_No, Email_Address;
        EditText editText1;
        ImageView imgbackgo;
        Button button1;
        Typeface tf;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Help);

            tev1 = FindViewById<TextView>(Resource.Id.tev1);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            tev1.SetTypeface(tf, TypefaceStyle.Bold);

            text1 = FindViewById<TextView>(Resource.Id.text1);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            text1.SetTypeface(tf, TypefaceStyle.Bold);

            Details = FindViewById<TextView>(Resource.Id.Details);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            Details.SetTypeface(tf, TypefaceStyle.Bold);

            Contact = FindViewById<TextView>(Resource.Id.Contact);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            Contact.SetTypeface(tf, TypefaceStyle.Bold);

            Contact_No = FindViewById<TextView>(Resource.Id.Contact_No);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            Contact_No.SetTypeface(tf, TypefaceStyle.Bold);

            Email = FindViewById<TextView>(Resource.Id.Email);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            Email.SetTypeface(tf, TypefaceStyle.Bold);

            Email_Address = FindViewById<TextView>(Resource.Id.Email_Address);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            Email_Address.SetTypeface(tf, TypefaceStyle.Bold);

            editText1 = FindViewById<EditText>(Resource.Id.editText1);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            editText1.SetTypeface(tf, TypefaceStyle.Bold);

            imgbackgo = FindViewById<ImageView>(Resource.Id.imgbackgo);
            imgbackgo.Click += Imgbackgo_Click;

            button1 = FindViewById<Button>(Resource.Id.button1);
            button1.Click += Button1_Click;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Toast.MakeText(this,"You Feedback has been sent to Admin!",ToastLength.Long).Show();
        }

        private void Imgbackgo_Click(object sender, EventArgs e)
        {
            base.OnBackPressed();
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