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
    public class AccountFragmentSettings : AndroidX.Fragment.App.Fragment
    {
        ImageView ChangePass, Changeno, ChangeUsername, back;
        private TextView Username;
        private CircleImageView user_image;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
           
            // Create your application here

            
        }

        

        public override Android.Views.View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            var rootview = inflater.Inflate(Resource.Layout.Accounts, container, false);
            ChangePass = (ImageView)rootview.FindViewById(Resource.Id.imgchangepass);
            ChangePass.Click += ChangePass_Click;
            Changeno = (ImageView)rootview.FindViewById(Resource.Id.imgchangeno);
            Changeno.Click += Changeno_Click;
            ChangeUsername = (ImageView)rootview.FindViewById(Resource.Id.imgchangeusername);
            ChangeUsername.Click += ChangeUsername_Click;
            back = (ImageView)rootview.FindViewById(Resource.Id.imgbackgo);
            back.Click += Back_Click;

            //UserName and Profile Pic
            Username = (TextView)rootview.FindViewById(Resource.Id.username); //User Name
            user_image = (CircleImageView)rootview.FindViewById(Resource.Id.usericon);  //user profile pic
            //Fetching user profile and name from UserInfoHolder
            Username.Text = Control.UserInfoHolder.User_name;
            byte[] arr = Convert.FromBase64String(Control.UserInfoHolder.Profile_pic);
            Bitmap UserImageBitmap = BitmapFactory.DecodeByteArray(arr, 0, arr.Length);
            user_image.SetImageBitmap(UserImageBitmap);
            //

            return rootview;
        }
        private void Back_Click(object sender, EventArgs e)
        {
            Activity.SupportFragmentManager.BeginTransaction()
                                           .Replace(Resource.Id.fragment_main, new Settings(), "settings")
                                           .Commit();
        }

        

        private void ChangeUsername_Click(object sender, EventArgs e)
        {
            Activity.SupportFragmentManager.BeginTransaction()
                                            .Replace(Resource.Id.fragment_main, new ChUsrname_fragmentsettings())
                                            .Commit();
        }

        private void Changeno_Click(object sender, EventArgs e)
        {
            Activity.SupportFragmentManager.BeginTransaction()
                                            .Replace(Resource.Id.fragment_main, new ChPhno_fragmentSettings())
                                            .Commit();
        }

        private void ChangePass_Click(object sender, EventArgs e)
        {
            Activity.SupportFragmentManager
                .BeginTransaction()
                .Replace(Resource.Id.fragment_main, new ChPass_fragmentSettings())
                .Commit();
        }

    }
}