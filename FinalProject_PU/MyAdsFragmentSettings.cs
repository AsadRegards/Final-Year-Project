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
    public class MyAdsFragmentSettings : AndroidX.Fragment.App.Fragment
    {
        ImageView back, uploadimg, submit;
        //CircleImageView userimage;
        TextView Username;
        EditText edturl;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override Android.Views.View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            var rootview = inflater.Inflate(Resource.Layout.MyAds, container, false);
            back = (ImageView)rootview.FindViewById(Resource.Id.imgbackgo);
            back.Click += Back_Click;
            //userimage = (CircleImageView)rootview.FindViewById(Resource.Id.usericon);
            Username = (TextView)rootview.FindViewById(Resource.Id.username);
            uploadimg = (ImageView)rootview.FindViewById(Resource.Id.imguploadimg);
            uploadimg.Click += Uploadimg_Click;
            edturl = (EditText)rootview.FindViewById(Resource.Id.edtUrl);
            submit = (ImageView)rootview.FindViewById(Resource.Id.imgsubmitt);
            submit.Click += Submit_Click;


            return rootview;
        }

        private void Submit_Click(object sender, EventArgs e)
        {
            
        }

        private void Uploadimg_Click(object sender, EventArgs e)
        {
            //upload image ka work
        }

        private void Back_Click(object sender, EventArgs e)
        {
            
        }
    }
}