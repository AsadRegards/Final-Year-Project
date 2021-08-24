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
    [Activity(Label = "MissingVehicle",NoHistory =true)]
    public class MissingVehicle : Activity
    {
        static string selected;
        ImageView back_MissingVehicle, next_MissingVehicle, MissingVehicle_image, FoundVehicle_image,
              iconSettngs, iconMap, iconNotifications, iconFunds, iconHome;
        RadioButton MissingVehicle_radiobtn1, FoundVehicle_radiobtn2;
       
        CircleImageView circleImageView_MissingVehicle;
        TextView MissingVehicle_tvusername, MissingVehicle_tv, MissingVehicle_tev1;
        Typeface tf;
        User u;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.MissingVehicle);

            circleImageView_MissingVehicle = (CircleImageView)FindViewById(Resource.Id.circleImageView_MissingVehicle);
            MissingVehicle_tvusername = (TextView)FindViewById(Resource.Id.MissingVehicle_tvusername);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            MissingVehicle_tvusername.SetTypeface(tf, TypefaceStyle.Bold);

            MissingVehicle_tv = (TextView)FindViewById(Resource.Id.MissingVehicle_tv);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            MissingVehicle_tv.SetTypeface(tf, TypefaceStyle.Bold);

            MissingVehicle_tev1 = (TextView)FindViewById(Resource.Id.tev1);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            MissingVehicle_tev1.SetTypeface(tf, TypefaceStyle.Bold);

            //runtime py profile change krna or name change krna 
            //start
            // circleImageView5 = (CircleImageView)FindViewById(Resource.Id.circleImageView5);
          char[] arr = Control.UserInfoHolder.User_name.ToCharArray();
            MissingVehicle_tvusername.SetText(arr, 0, arr.Length);
            byte[] arra = Convert.FromBase64String(Control.UserInfoHolder.Profile_pic);

            ImageView img2 = FindViewById<ImageView>(Resource.Id.circleImageView_MissingVehicle);
            Android.Graphics.Bitmap bitmapp = BitmapFactory.DecodeByteArray(arra, 0, arra.Length);
            img2.SetImageBitmap(bitmapp);
            //end //runtime py profile change krna or name change krna 

          
            next_MissingVehicle = (ImageView)FindViewById(Resource.Id.next_MissingVehicle);
            next_MissingVehicle.Click += Next_MissingVehicle_Click;
            MissingVehicle_image = (ImageView)FindViewById(Resource.Id.MissingVehicle_image);
            MissingVehicle_image.Click += MissingVehicle_image_Click;
            FoundVehicle_image = (ImageView)FindViewById(Resource.Id.FoundVehicle_image);
            FoundVehicle_image.Click += FoundVehicle_image_Click;
            MissingVehicle_radiobtn1 = (RadioButton)FindViewById(Resource.Id.MissingVehicle_Radiobtn1);
            MissingVehicle_radiobtn1.Click += MissingVehicle_radiobtn1_Click;
            FoundVehicle_radiobtn2 = (RadioButton)FindViewById(Resource.Id.FoundVehicle_Radiobtn2);
            FoundVehicle_radiobtn2.Click += MissingVehicle_radiobtn2_Click;


           
        }

        private void MissingVehicle_image_Click(object sender, EventArgs e)
        {
            MissingVehicle_radiobtn1.Checked = true;
            FoundVehicle_radiobtn2.Checked = false;
        }

        private void FoundVehicle_image_Click(object sender, EventArgs e)
        {
            MissingVehicle_radiobtn1.Checked = false;
            FoundVehicle_radiobtn2.Checked = true;
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
        private void MissingVehicle_radiobtn2_Click(object sender, EventArgs e)
        {
            FoundVehicle_image.PerformClick();
        }

        private void MissingVehicle_radiobtn1_Click(object sender, EventArgs e)
        {
            MissingVehicle_image.PerformClick();
        }

        
        private void Next_MissingVehicle_Click(object sender, EventArgs e)
        {
            string issueImageString = JsonConvert.DeserializeObject<string>(Intent.GetStringExtra("objtopass"));
            if (FoundVehicle_radiobtn2.Checked)
            {
                Model.Missingvehicle m = new Missingvehicle();
                m.isVehicleLostorFound = "found";
                m.IssueImage = issueImageString;
                FinalProject_PU.Control.DataOper.PutData<FoundVehicle>(this, m);
            }
            else
            {
                Model.Missingvehicle m = new Missingvehicle();
                m.isVehicleLostorFound = "lost";
                m.IssueImage = issueImageString;
                FinalProject_PU.Control.DataOper.PutData<MissingVehicle2>(this, m);
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