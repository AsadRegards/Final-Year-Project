using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using FinalProject_PU.Control;
using MohammedAlaa.GifLoading;
using Newtonsoft.Json;
using System;

namespace FinalProject_PU
{
    [Activity(Label = "Newpassword",NoHistory =true)]
    public class Newpassword : Activity
    {
        ImageView submit;
        TextView tv1, tv2, tv5;
        EditText newPass, cn_pass;
        
        Typeface tf;
        LoadingView loader;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.newPass);
            submit = FindViewById<ImageView>(Resource.Id.imgbtn);
            submit.Click += Submit_Click;

            newPass = (EditText)FindViewById(Resource.Id.eddt1);
            loader = FindViewById<LoadingView>(Resource.Id.loading_view);
            cn_pass = (EditText)FindViewById(Resource.Id.eddt5);

           

            tv1 = (TextView)FindViewById(Resource.Id.tv1);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            tv1.SetTypeface(tf, TypefaceStyle.Bold);

            tv2 = (TextView)FindViewById(Resource.Id.tv2);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            tv2.SetTypeface(tf, TypefaceStyle.Bold);

            tv5 = (TextView)FindViewById(Resource.Id.tv5);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            tv5.SetTypeface(tf, TypefaceStyle.Bold);

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
        private void Submit_Click(object sender, EventArgs e)
        {
            newPass.Enabled = false;
            cn_pass.Enabled = false;
            loader.Visibility = Android.Views.ViewStates.Visible;
            try
            {
                if (InputValidation.ValidatePassword(newPass.Text, cn_pass.Text, this))
                {
                    string user_email = JsonConvert.DeserializeObject<string>(Intent.GetStringExtra("email"));
                    try 
                    {
                        Account.update_new_password(newPass.Text.GetHashCode(), user_email, this);
                    } 
                    catch (Exception ex) 
                    {
                        Toast.MakeText(this, "Couln't set password right now!, Please try later\n"+ex.ToString(), ToastLength.Long).Show();
                    } 
                    finally 
                    {
                        var i = new Intent(this, typeof(Login));
                        this.StartActivity(i);
                    }
                   
                  
                }
            }
            catch(Exception ex) { }
            finally
            {
                newPass.Enabled = true;
                cn_pass.Enabled = true;
                loader.Visibility = Android.Views.ViewStates.Gone;
            }
           
        }
    }
}