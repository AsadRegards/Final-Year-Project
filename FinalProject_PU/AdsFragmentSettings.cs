using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject_PU
{
    public class AdsFragmentSettings : AndroidX.Fragment.App.Fragment
    {
        ImageView back, crtNewAd, manageAds;
        TextView Username;
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
            var rootview = inflater.Inflate(Resource.Layout.Ads, container, false);
            back = (ImageView)rootview.FindViewById(Resource.Id.imgbackgo);
            back.Click += Back_Click;
            Username = (TextView)rootview.FindViewById(Resource.Id.username);
            
            //userimage = (CircleImageView)rootview.FindViewById(Resource.Id.usericon);
            crtNewAd = (ImageView)rootview.FindViewById(Resource.Id.imgcrtnewads);
            crtNewAd.Click += CrtNewAd_Click;
            manageAds=(ImageView)rootview.FindViewById(Resource.Id.imgManageAds);
            manageAds.Click += ManageAds_Click;
            return rootview;
        }

        private void CrtNewAd_Click(object sender, EventArgs e)
        {
            
        }

        private void ManageAds_Click(object sender, EventArgs e)
        {
            //screen nai bni v
            //Intent i = new Intent(Application.Context, typeof());
            //this.StartActivity(i);
        }

        private void Back_Click(object sender, EventArgs e)
        {
           
        }
    }
}