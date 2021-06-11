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
    [Activity(Label = "ChPass_fragmentSettings")]
    public class ChPass_fragmentSettings : Activity
    {
        TextView Username;
        ImageView back, submit;
        CircleImageView user_image;
        EditText edtCurrentPass, edtNewPass, edtConfrimPass;
        //CircleImageView userimage;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
            SetContentView(Resource.Layout.ChangePass);
            back = (ImageView)FindViewById(Resource.Id.imgbackgo);
            back.Click += Back_Click;
            edtCurrentPass = (EditText)FindViewById(Resource.Id.edtCurrentPass);
            edtNewPass = (EditText)FindViewById(Resource.Id.edtNewPass);
            edtConfrimPass = (EditText)FindViewById(Resource.Id.edtConfirmPass);
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
        }

    

        private async void Submit_Click(object sender, EventArgs e)
        {
            if(Control.InputValidation.ValidatePassword(edtNewPass.Text,edtConfrimPass.Text,Application.Context))
            {
               Control.Account.SetNewPassword(edtCurrentPass.Text.GetHashCode(),Control.UserInfoHolder.email, edtNewPass.Text.GetHashCode(),Application.Context);

            }
        }

        private void Back_Click(object sender, EventArgs e)
        {
            base.OnBackPressed();
            this.Finish();
        }
    }
}