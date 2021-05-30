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
    public class ChPhno_fragmentSettings : AndroidX.Fragment.App.Fragment
    {
        TextView Username;

        public CircleImageView user_image { get; private set; }

        ImageView back, submit;
        EditText edtCurrentPass, edtNewPhno;
        //CircleImageView userimage;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override Android.Views.View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            var rootview = inflater.Inflate(Resource.Layout.Changeno, container, false);
            back = (ImageView)rootview.FindViewById(Resource.Id.imgbackgo);
            back.Click += Back_Click;
            edtCurrentPass = (EditText)rootview.FindViewById(Resource.Id.edtCurrentPass);
            edtNewPhno = (EditText)rootview.FindViewById(Resource.Id.edtNewMob);
            submit = (ImageView)rootview.FindViewById(Resource.Id.imgsubmitt);
            submit.Click += Submit_Click;


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

        private void Submit_Click(object sender, EventArgs e)
        {
            if(Control.InputValidation.ValidateContact(edtNewPhno.Text,Application.Context))
            {
                Control.Account.SetNewPhoneNo(edtCurrentPass.Text.GetHashCode(), Control.UserInfoHolder.email, edtNewPhno.Text, Application.Context);
            }
        }

        private void Back_Click(object sender, EventArgs e)
        {
            
        }
    }
}