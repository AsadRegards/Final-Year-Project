﻿using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FinalProject_PU.Control;
using FinalProject_PU.Model;
using Newtonsoft.Json;
using Refractored.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject_PU
{
    [Activity(Label = "createissue4",NoHistory =true)]
    public class createissue4 : Activity
    {
        ImageView createissue4_back, createissue4_next, createissue4_radio1, 
                    createissue4_radio2, createissue4_radio3, iconSettngs, iconMap, iconNotifications, iconFunds, iconHome;
        RadioButton createissue4_radiobtn1, createissue4_radiobtn2, createissue4_radiobtn3;
        CircleImageView circleimageview24;
        TextView createissue3_tvusername, create_issue_4_tv;
        Typeface tf;
        static string selected;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.createissue4);

            //runtime py user name or imageview change krna hai
            createissue3_tvusername = (TextView)FindViewById(Resource.Id.create_issue_3_tvusername);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            createissue3_tvusername.SetTypeface(tf, TypefaceStyle.Bold);

            create_issue_4_tv = (TextView)FindViewById(Resource.Id.create_issue_4_tv);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            create_issue_4_tv.SetTypeface(tf, TypefaceStyle.Bold);

            circleimageview24 = FindViewById<CircleImageView>(Resource.Id.circleImageView24);
            
            createissue4_next = (ImageView)FindViewById(Resource.Id.create_issue4_btnnext);
            createissue4_next.Click += Createissue4_next_Click;
            createissue4_back = (ImageView)FindViewById(Resource.Id.backbtncreate4);
            createissue4_back.Click += Createissue4_back_Click;
            ImageView close = (ImageView)FindViewById(Resource.Id.close);
            close.Click += Close_Click;
            createissue4_radiobtn1 = (RadioButton)FindViewById(Resource.Id.create_issue4_radiobtn1);
            createissue4_radiobtn1.Click += Createissue4_radiobtn1_Click;
            createissue4_radiobtn2 = (RadioButton)FindViewById(Resource.Id.create_issue4_radiobtn2);
            createissue4_radiobtn2.Click += Createissue4_radiobtn2_Click;
            createissue4_radiobtn3 = (RadioButton)FindViewById(Resource.Id.create_issue4_radiobtn3);
            createissue4_radiobtn3.Click += Createissue4_radiobtn3_Click;

            createissue4_radio1 = (ImageView)FindViewById(Resource.Id.crt_issue_4_radio1);
            createissue4_radio1.Click += Createissue4_radio1_Click;
            createissue4_radio2 = (ImageView)FindViewById(Resource.Id.crt_issue_4_radio2);
            createissue4_radio2.Click += Createissue4_radio2_Click;
            createissue4_radio3 = (ImageView)FindViewById(Resource.Id.crt_issue_4_radio3);
            createissue4_radio3.Click += Createissue4_radio3_Click;
            
            //runtime py profile change krna or name change krna 
            //start
          
            char[] arr = UserInfoHolder.User_name.ToCharArray();
            byte[] arra = Convert.FromBase64String(Control.UserInfoHolder.Profile_pic);
            Android.Graphics.Bitmap bitmapp = BitmapFactory.DecodeByteArray(arra, 0, arra.Length);
            circleimageview24.SetImageBitmap(bitmapp);
            createissue3_tvusername.SetText(arr, 0, arr.Length);
            //end //runtime py profile change krna or name change krna 
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Intent i = new Intent(this, typeof(FragmentHomeActivity));
            this.StartActivity(i);
        }

        private void Createissue4_back_Click(object sender, EventArgs e)
        {
            base.OnBackPressed();
        }

        private void Createissue4_radiobtn1_Click(object sender, EventArgs e)
        {
            createissue4_radio1.PerformClick();
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
        private void Createissue4_radiobtn2_Click(object sender, EventArgs e)
        {
            createissue4_radio2.PerformClick();
        }

        private void Createissue4_radiobtn3_Click(object sender, EventArgs e)
        {
            createissue4_radio1.PerformClick();
        }

        private void Createissue4_radio3_Click(object sender, EventArgs e)
        {
            
            selected = "fourlane";
           

            createissue4_radiobtn3.Checked = true;
            createissue4_radiobtn2.Checked = false;
            createissue4_radiobtn1.Checked = false;
        }

        private void Createissue4_radio2_Click(object sender, EventArgs e)
        {
         
            selected = "threelane";
           
            createissue4_radiobtn2.Checked = true;
            createissue4_radiobtn3.Checked = false;
            createissue4_radiobtn1.Checked = false;
        }

        private void Createissue4_radio1_Click(object sender, EventArgs e)
        {
           
            
            selected = "twolane";
            createissue4_radiobtn1.Checked = true;
            createissue4_radiobtn2.Checked = false;
            createissue4_radiobtn3.Checked = false;
        }

        private void Createissue4_next_Click(object sender, EventArgs e)
        {
            if (selected != null || selected != "") 
            {
                var p = JsonConvert.DeserializeObject<Pothole>(Intent.GetStringExtra("objtopass"));
                p.roadSize = selected;
                Control.DataOper.PutData<createissue5>(this, p);
            }
            else
            {
                Toast.MakeText(this, "Please select any option", ToastLength.Long).Show();
            }
            
            

        }

        private void IconHome_Click(object sender, EventArgs e)
        {
            var i = new Intent(this, typeof(HomeActivity));
            this.StartActivity(i);
        }

        private void IconFunds_Click(object sender, EventArgs e)
        {
            var i = new Intent(this, typeof(FundsActivity));
            this.StartActivity(i);
        }

        private void IconNotifications_Click(object sender, EventArgs e)
        {
            var i = new Intent(this, typeof(NotificationsActivity));
            this.StartActivity(i);
        }

        private void IconMap_Click(object sender, EventArgs e)
        {
            var i = new Intent(this, typeof(MapActivity));
            this.StartActivity(i);
        }

        private void IconSettngs_Click(object sender, EventArgs e)
        {
            var i = new Intent(this, typeof(SettingsActivity));
            this.StartActivity(i);
        }
    }
}