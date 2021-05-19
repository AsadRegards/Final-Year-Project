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
    [Activity(Label = "createissue6")]
    public class createissue6 : Activity
    {
        ImageView create_issue4_back, create_issue4_btnnext, iconSettngs, iconMap, iconNotifications, iconFunds, iconHome , crt_issue_6_radio1, crt_issue_6_radio2, crt_issue_6_radio3;
        RadioButton create_issue6_radiobtn1, create_issue6_radiobtn2, create_issue6_radiobtn3;
        CircleImageView circleimageview6;
        TextView tvusername, create_issue_4_tv, heading_tev1;
        Typeface tf;
        static string selected;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.createissue6);
            //runtime py change krna hai tvusername , circleimgview
            circleimageview6 = (CircleImageView)FindViewById(Resource.Id.circleImageView6);

            tvusername = (TextView)FindViewById(Resource.Id.create_issue_3_tvusername);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            tvusername.SetTypeface(tf, TypefaceStyle.Bold);

            create_issue_4_tv = (TextView)FindViewById(Resource.Id.create_issue_4_tv);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            create_issue_4_tv.SetTypeface(tf, TypefaceStyle.Bold);

            heading_tev1 = (TextView)FindViewById(Resource.Id.tev1);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            heading_tev1.SetTypeface(tf, TypefaceStyle.Bold);

            iconSettngs = (ImageView)FindViewById(Resource.Id.iconSettings);
            iconSettngs.Click += IconSettngs_Click;
            iconMap = (ImageView)FindViewById(Resource.Id.iconMap);
            iconMap.Click += IconMap_Click;
            iconNotifications = (ImageView)FindViewById(Resource.Id.iconNotifications);
            iconNotifications.Click += IconNotifications_Click;
            iconFunds = (ImageView)FindViewById(Resource.Id.iconFunds);
            iconFunds.Click += IconFunds_Click;
            iconHome = (ImageView)FindViewById(Resource.Id.iconHome);
            iconHome.Click += IconHome_Click;
           
            create_issue4_btnnext = (ImageView)FindViewById(Resource.Id.create_issue4_btnnext);
            create_issue4_btnnext.Click += Create_issue4_btnnext_Click;

            circleimageview6 = (CircleImageView)FindViewById(Resource.Id.circleImageView6);
            crt_issue_6_radio1 = (ImageView)FindViewById(Resource.Id.crt_issue_6_radio1);
            crt_issue_6_radio1.Click += Crt_issue_6_radio1_Click;
            crt_issue_6_radio2 = (ImageView)FindViewById(Resource.Id.crt_issue_6_radio2);
            crt_issue_6_radio2.Click += Crt_issue_6_radio2_Click;
            crt_issue_6_radio3 = (ImageView)FindViewById(Resource.Id.crt_issue_6_radio3);
            crt_issue_6_radio3.Click += Crt_issue_6_radio3_Click;


            create_issue6_radiobtn1 = (RadioButton)FindViewById(Resource.Id.create_issue6_radiobtn1);
            create_issue6_radiobtn1.Click += Create_issue6_radiobtn1_Click;
            create_issue6_radiobtn2 = (RadioButton)FindViewById(Resource.Id.create_issue6_radiobtn2);
            create_issue6_radiobtn2.Click += Create_issue6_radiobtn2_Click;
            create_issue6_radiobtn3 = (RadioButton)FindViewById(Resource.Id.create_issue6_radiobtn3);
            create_issue6_radiobtn3.Click += Create_issue6_radiobtn3_Click;

            //runtime py profile change krna or name change krna 
            //start
            
            char[] arr = Control.UserInfoHolder.User_name.ToCharArray();
            byte[] arra = Convert.FromBase64String(Control.UserInfoHolder.Profile_pic);
            Android.Graphics.Bitmap bitmapp = BitmapFactory.DecodeByteArray(arra, 0, arra.Length);
            circleimageview6.SetImageBitmap(bitmapp);
            tvusername.SetText(arr, 0, arr.Length);
            //end //runtime py profile change krna or name change krna 
        }

        private void Create_issue6_radiobtn3_Click(object sender, EventArgs e)
        {
            crt_issue_6_radio3.PerformClick();
        }

        private void Create_issue6_radiobtn2_Click(object sender, EventArgs e)
        {
            crt_issue_6_radio2.PerformClick();
        }

        private void Create_issue6_radiobtn1_Click(object sender, EventArgs e)
        {
            crt_issue_6_radio1.PerformClick();
        }

        private void Crt_issue_6_radio3_Click(object sender, EventArgs e)
        {
            selected = "middle";
            create_issue6_radiobtn3.Checked = true;
            create_issue6_radiobtn2.Checked = false;
            create_issue6_radiobtn1.Checked = false;
        }

        private void Crt_issue_6_radio2_Click(object sender, EventArgs e)
        {
            selected = "leftslowtrack";
            create_issue6_radiobtn1.Checked = false;
            create_issue6_radiobtn2.Checked = true;
            create_issue6_radiobtn3.Checked = false;
        }

        private void Crt_issue_6_radio1_Click(object sender, EventArgs e)
        {
            selected = "rightfasttrack";
            create_issue6_radiobtn3.Checked = false;
            create_issue6_radiobtn2.Checked = false;
            create_issue6_radiobtn1.Checked = true;
        }

        private void Create_issue4_btnnext_Click(object sender, EventArgs e)
        {
            
            var p = JsonConvert.DeserializeObject<Pothole>(Intent.GetStringExtra("objtopass"));
            if (selected != "")
            {
                p.issuePositionwrtRoad = selected;
                Control.DataOper.PutData<IssueLocationPickup_Pothole>(this, p);
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