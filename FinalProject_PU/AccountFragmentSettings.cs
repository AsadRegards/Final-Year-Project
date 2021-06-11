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

    [Activity(Label = "AccountFragmentSettings")]
    public class AccountFragmentSettings : Activity
    {
        ImageView ChangePass, Changeno, ChangeUsername, back;
        private TextView Username;
        private CircleImageView user_image;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Accounts);
            // Create your application here
            ChangePass = (ImageView)FindViewById(Resource.Id.imgchangepass);
            ChangePass.Click += ChangePass_Click;
            Changeno = (ImageView)FindViewById(Resource.Id.imgchangeno);
            Changeno.Click += Changeno_Click;
            ChangeUsername = (ImageView)FindViewById(Resource.Id.imgchangeusername);
            ChangeUsername.Click += ChangeUsername_Click;
            back = (ImageView)FindViewById(Resource.Id.imgbackgo);
            back.Click += Back_Click;

            //UserName and Profile Pic
            Username = (TextView)FindViewById(Resource.Id.username); //User Name
            user_image = (CircleImageView)FindViewById(Resource.Id.usericon);  //user profile pic
            //Fetching user profile and name from UserInfoHolder
            Username.Text = Control.UserInfoHolder.User_name;
            byte[] arr = Convert.FromBase64String(Control.UserInfoHolder.Profile_pic);
            Bitmap UserImageBitmap = BitmapFactory.DecodeByteArray(arr, 0, arr.Length);
            user_image.SetImageBitmap(UserImageBitmap);
            //


        }




        private void Back_Click(object sender, EventArgs e)
        {
            base.OnBackPressed();
            this.Finish();
        }



        private void ChangeUsername_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(ChUsrname_fragmentsettings));
            StartActivity(intent);

        }

        private void Changeno_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(ChPhno_fragmentSettings));
            StartActivity(intent);
        }

        private void ChangePass_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(ChPass_fragmentSettings));
            StartActivity(intent);
        }

    }
}