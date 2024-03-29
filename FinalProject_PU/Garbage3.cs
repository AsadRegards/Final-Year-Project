﻿using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FinalProject_PU.Model;
using Newtonsoft.Json;
using Refractored.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_PU
{
    [Activity(Label = "Garbage3",NoHistory =true)]
    public class Garbage3 : Activity
    {
        static string selected;
        ImageView createissue5_back, creatissue5_btnnext, createissue5_radio1,
                    createissue5_radio2, createissue5_radio3, iconSettngs, iconMap, iconNotifications, iconFunds, iconHome;
        RadioButton createissue5_radiobtn1, createissue5_radiobtn2, createissue5_radiobtn3;
        CircleImageView circleImageView5;
        TextView create_issue_5_tvusername, create_issue_5_tv, tev1;
        Typeface tf;
        User u;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.createissue5);
            circleImageView5 = (CircleImageView)FindViewById(Resource.Id.circleImageView5);
            create_issue_5_tvusername = (TextView)FindViewById(Resource.Id.create_issue_5_tvusername);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            create_issue_5_tvusername.SetTypeface(tf, TypefaceStyle.Bold);
            //runtime py profile change krna or name change krna 
            //start
            circleImageView5 = (CircleImageView)FindViewById(Resource.Id.circleImageView5);
          char[] arr = Control.UserInfoHolder.User_name.ToCharArray();
            create_issue_5_tvusername.SetText(arr, 0, arr.Length);
            byte[] arra = Convert.FromBase64String(Control.UserInfoHolder.Profile_pic);

            ImageView img2 = FindViewById<ImageView>(Resource.Id.circleImageView5);
            Android.Graphics.Bitmap bitmapp = BitmapFactory.DecodeByteArray(arra, 0, arra.Length);
            img2.SetImageBitmap(bitmapp);
            //end //runtime py profile change krna or name change krna 

            create_issue_5_tv = (TextView)FindViewById(Resource.Id.create_issue_5_tv);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            create_issue_5_tv.SetTypeface(tf, TypefaceStyle.Bold);

            tev1 = (TextView)FindViewById(Resource.Id.tev1);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            tev1.SetTypeface(tf, TypefaceStyle.Bold);

         
            creatissue5_btnnext = (ImageView)FindViewById(Resource.Id.create_issue5_btnnext);
            creatissue5_btnnext.Click += Creatissue5_btnnext_Click;

            createissue5_radio1 = (ImageView)FindViewById(Resource.Id.crt_issue_5_radio1);
            createissue5_radio1.Click += Createissue5_radio1_Click;
            createissue5_radio2 = (ImageView)FindViewById(Resource.Id.crt_issue_5_radio2);
            createissue5_radio2.Click += Createissue5_radio2_Click;
            createissue5_radio3 = (ImageView)FindViewById(Resource.Id.crt_issue_5_radio3);
            createissue5_radio3.Click += Createissue5_radio3_Click;

            createissue5_radiobtn1 = (RadioButton)FindViewById(Resource.Id.create_issue5_radiobtn1);
            createissue5_radiobtn1.Click += Createissue5_radiobtn1_Click1;
            createissue5_radiobtn2 = (RadioButton)FindViewById(Resource.Id.create_issue5_radiobtn2);
            createissue5_radiobtn2.Click += Createissue5_radiobtn2_Click;
            createissue5_radiobtn3 = (RadioButton)FindViewById(Resource.Id.create_issue5_radiobtn3);
            createissue5_radiobtn3.Click += Createissue5_radiobtn3_Click;


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
        private void Createissue5_radiobtn3_Click(object sender, EventArgs e)
        {
            createissue5_radio3.PerformClick();
        }

        private void Createissue5_radiobtn2_Click(object sender, EventArgs e)
        {
            createissue5_radio2.PerformClick();
        }

        private void Createissue5_radiobtn1_Click1(object sender, EventArgs e)
        {
            createissue5_radio1.PerformClick();
        }


        private void Createissue5_radio3_Click(object sender, EventArgs e)
        {
            selected = "heavytraffic";
            createissue5_radiobtn3.Checked = true;
            createissue5_radiobtn1.Checked = false;
            createissue5_radiobtn2.Checked = false;
        }

        private void Createissue5_radio2_Click(object sender, EventArgs e)
        {
            selected = "moderatetraffic";
            createissue5_radiobtn1.Checked = false;
            createissue5_radiobtn2.Checked = true;
            createissue5_radiobtn3.Checked = false;
        }

        private void Createissue5_radio1_Click(object sender, EventArgs e)
        {
            selected = "heavytraffic";
            createissue5_radiobtn1.Checked = true;
            createissue5_radiobtn2.Checked = false;
            createissue5_radiobtn3.Checked = false;
        }

        private async void Creatissue5_btnnext_Click(object sender, EventArgs e)
        {
            if (selected != "")
            {


                await Task.Run(() =>
                {
                    var json = Intent.GetStringExtra("objtopass");
                    var gar = JsonConvert.DeserializeObject<Model.Garbage>(Intent.GetStringExtra("objtopass"));
                    gar.traffic = selected;
                    FinalProject_PU.Control.DataOper.PutData<Garbage4>(this, gar);


                });
            }
            else
            {
                Toast.MakeText(this, "Please select any one from them", ToastLength.Long).Show();
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