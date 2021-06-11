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
    [Activity(Label = "MyAdsFragmentSettings")]
    public class MyAdsFragmentSettings : Activity
    {
        ImageView back, uploadimg, submit;
        //CircleImageView userimage;
        TextView Username;
        EditText edturl;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MyAds);
            back = (ImageView)FindViewById(Resource.Id.imgbackgo);
            back.Click += Back_Click;
            //userimage = (CircleImageView)FindViewById(Resource.Id.usericon);
            Username = (TextView)FindViewById(Resource.Id.username);
            uploadimg = (ImageView)FindViewById(Resource.Id.imguploadimg);
            uploadimg.Click += Uploadimg_Click;
            edturl = (EditText)FindViewById(Resource.Id.edtUrl);
            submit = (ImageView)FindViewById(Resource.Id.imgsubmitt);
            submit.Click += Submit_Click;

            // Create your fragment here
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