using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinalProject_PU.Model;
using Newtonsoft.Json;

namespace FinalProject_PU
{
    [Activity(Label = "LocationsActivity",NoHistory =true)]
    public class LocationsActivity : Activity
    {
        ImageView imgback, imgjobloca, imgview9;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Locationwork);

            imgback = (ImageView)FindViewById(Resource.Id.imgback);
            imgback.Click += Imgback_Click;

            imgjobloca = (ImageView)FindViewById(Resource.Id.imgJobLoca);
            imgjobloca.Click += Imgjobloca_Click;
        }

        private void Imgjobloca_Click(object sender, EventArgs e)
        {
            var user = JsonConvert.DeserializeObject<User>(Intent.GetStringExtra("userObjtoJob"));
            var intent = new Intent(this, typeof(Location_pickup_job));
            intent.PutExtra("userObjtoLocation",JsonConvert.SerializeObject(user));
            this.StartActivity(intent);
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
        private void Imgback_Click(object sender, EventArgs  e)
        {
            var i = new Intent(this,typeof(LocationActivity));
            this.StartActivity(i);

        }
    }
}