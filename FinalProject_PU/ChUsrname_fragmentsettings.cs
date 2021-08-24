using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Refractored.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject_PU
{
    [Activity(Label = "ChUsrname_fragmentsettings",NoHistory =true)]
    public class ChUsrname_fragmentsettings : Activity
    {
        TextView Username;

        public CircleImageView user_image { get; private set; }

        ImageView back, submit;
        EditText edtCurrentPass, edtNewUsername;
        //CircleImageView userimage;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ChangeUsername);
            back = (ImageView)FindViewById(Resource.Id.imgbackgo);
            back.Click += Back_Click;
            Username = (TextView)FindViewById(Resource.Id.username);
            edtCurrentPass = (EditText)FindViewById(Resource.Id.edtCurrentPass);
            edtNewUsername = (EditText)FindViewById(Resource.Id.edtNewUsername);
            submit = (ImageView)FindViewById(Resource.Id.imgsubmitt);
            submit.Click += Submit_Click;

            //UserName and Profile Pic
            Username = (TextView)FindViewById(Resource.Id.username); //User Name
            user_image = (CircleImageView)FindViewById(Resource.Id.usericon);  //user profile pic
            //Fetching user profile and name from UserInfoHolder
            Username.Text = Control.UserInfoHolder.User_name;
            byte[] arr = Convert.FromBase64String(Control.UserInfoHolder.Profile_pic);
            Bitmap UserImageBitmap = BitmapFactory.DecodeByteArray(arr, 0, arr.Length);
            user_image.SetImageBitmap(UserImageBitmap);
            //
            // Create your fragment here
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
            if (Control.InputValidation.ValidateUsername(edtNewUsername.Text, Application.Context))
            {
                Control.Account.SetNewUserName(edtCurrentPass.Text.GetHashCode(), Control.UserInfoHolder.email, edtNewUsername.Text, Application.Context);
            }

        }

        private void Back_Click(object sender, EventArgs e)
        {
            base.OnBackPressed();

        }
    }
}