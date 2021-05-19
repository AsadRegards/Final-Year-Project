using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using FinalProject_PU.Model;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace FinalProject_PU
{
    [Activity(Label = "LocationActivity")]
    public class LocationActivity : Activity
    {
        ImageView imgview9, imghomeloca, imgback;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Location);

            imgview9 = (ImageView)FindViewById(Resource.Id.imageView9);
            imgview9.Click += Imgview9_Click;

            imghomeloca = (ImageView)FindViewById(Resource.Id.imgHomeLoca);
            imghomeloca.Click += Imghomeloca_Click;

            imgback = (ImageView)FindViewById(Resource.Id.imgback);
            imgback.Click += Imgback_Click;
        }

        private void Imghomeloca_Click(object sender, EventArgs e)
        {
            var user = JsonConvert.DeserializeObject<User>(Intent.GetStringExtra("userObj"));
            var intent = new Intent(this, typeof(Location_pickup_home));
            intent.PutExtra("userObj", JsonConvert.SerializeObject(user));
            this.StartActivity(intent);
            
        }

        private void Imgback_Click(object sender, EventArgs e)
        {
            var i = new Intent(this, typeof(PhotoUploading));
            this.StartActivity(i);

        }



        private void Imgview9_Click(object sender, EventArgs e)
        {
            var i = new Intent(this, typeof(Login));
            this.StartActivity(i);
        }
    }
}