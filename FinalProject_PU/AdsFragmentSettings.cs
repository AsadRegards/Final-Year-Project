using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using FinalProject_PU.Control;
using Refractored.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject_PU
{
    [Activity(Label = "AdsFragmentSettings")]
    public class AdsFragmentSettings : Activity
    {
        ImageView back, crtNewAd, manageAds;
        TextView Username;

        public ImageView userimage { get; private set; }

        //CircleImageView userimage;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Ads);
           

            // Create your fragment here
            back = (ImageView)FindViewById(Resource.Id.imgbackgo);
            back.Click += Back_Click;
            Username = (TextView)FindViewById(Resource.Id.username);

            userimage = (ImageView)FindViewById(Resource.Id.usericon);
            crtNewAd = (ImageView)FindViewById(Resource.Id.imgcrtnewads);
            crtNewAd.Click += CrtNewAd_Click;
            manageAds = (ImageView)FindViewById(Resource.Id.imgManageAds);
            manageAds.Click += ManageAds_Click;
            //runtime py name and user image change start
            char[] arr = UserInfoHolder.User_name.ToCharArray();
            byte[] arra = Convert.FromBase64String(Control.UserInfoHolder.Profile_pic);
            Android.Graphics.Bitmap bitmapp = BitmapFactory.DecodeByteArray(arra, 0, arra.Length);
            userimage.SetImageBitmap(bitmapp);
            Username.SetText(arr, 0, arr.Length);
            //end
        }



        private void CrtNewAd_Click(object sender, EventArgs e)
        {
            var intent = new Intent(Application.Context, typeof(MyAdsFragmentSettings));
            intent.AddFlags(ActivityFlags.NewTask);
            Application.Context.StartActivity(intent);
            
        }

        private void ManageAds_Click(object sender, EventArgs e)
        {
            //screen nai bni v
            //Intent i = new Intent(Application.Context, typeof());
            //this.StartActivity(i);
        }

        private void Back_Click(object sender, EventArgs e)
        {
            base.OnBackPressed();
            
        }
    }
}