using Android.App;
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

namespace FinalProject_PU
{
    [Activity(Label = "createissue5")]
    public class createissue5 : Activity
    {
        ImageView createissue5_back, creatissue5_btnnext,createissue5_radio1,
                    createissue5_radio2, createissue5_radio3, iconSettngs, iconMap, iconNotifications, iconFunds, iconHome;
        RadioButton createissue5_radiobtn1, createissue5_radiobtn2, createissue5_radiobtn3;
        CircleImageView circleimageview5;
        Typeface tf;
        TextView createissue5_tvusername, create_issue_5_tv, create_issue_5_heading;
        static string selected;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.createissue5);
            circleimageview5 = FindViewById<CircleImageView>(Resource.Id.circleImageView5);
            createissue5_radiobtn1 = (RadioButton)FindViewById(Resource.Id.create_issue5_radiobtn1);
            createissue5_radiobtn1.Click += Createissue5_radiobtn1_Click1;
            createissue5_radiobtn2 = (RadioButton)FindViewById(Resource.Id.create_issue5_radiobtn2);
            createissue5_radiobtn2.Click += Createissue5_radiobtn2_Click;
            createissue5_radiobtn3 = (RadioButton)FindViewById(Resource.Id.create_issue5_radiobtn3);
            createissue5_radiobtn3.Click += Createissue5_radiobtn3_Click;
            creatissue5_btnnext = (ImageView)FindViewById(Resource.Id.create_issue5_btnnext);
            creatissue5_btnnext.Click += Creatissue5_btnnext_Click;

            createissue5_tvusername = (TextView)FindViewById(Resource.Id.create_issue_5_tvusername);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            createissue5_tvusername.SetTypeface(tf, TypefaceStyle.Bold);

            create_issue_5_tv = (TextView)FindViewById(Resource.Id.create_issue_5_tv);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            create_issue_5_tv.SetTypeface(tf, TypefaceStyle.Bold);

            create_issue_5_heading = (TextView)FindViewById(Resource.Id.tev1);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            create_issue_5_heading.SetTypeface(tf, TypefaceStyle.Bold);

            createissue5_radio1 = (ImageView)FindViewById(Resource.Id.crt_issue_5_radio1);
            createissue5_radio1.Click += Createissue5_radio1_Click;
            createissue5_radio2 = (ImageView)FindViewById(Resource.Id.crt_issue_5_radio2);
            createissue5_radio2.Click += Createissue5_radio2_Click;
            createissue5_radio3 = (ImageView)FindViewById(Resource.Id.crt_issue_5_radio3);
            createissue5_radio3.Click += Createissue5_radio3_Click;



 
            var arr = Control.UserInfoHolder.User_name.ToCharArray();
            createissue5_tvusername.SetText(arr, 0, arr.Length);
            var barr = Convert.FromBase64String(Control.UserInfoHolder.Profile_pic);
            var imagebitmap = BitmapFactory.DecodeByteArray(barr, 0, barr.Length);
            circleimageview5.SetImageBitmap(imagebitmap);

           
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
            selected = "lighttraffic";
            createissue5_radiobtn1.Checked = true;
            createissue5_radiobtn2.Checked = false;
            createissue5_radiobtn3.Checked = false;
        }



        private void Creatissue5_btnnext_Click(object sender, EventArgs e)
        {
            var p = JsonConvert.DeserializeObject<Pothole>(Intent.GetStringExtra("objtopass"));
            p.traffic = selected;
            Control.DataOper.PutData<createissue6>(this, p);
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