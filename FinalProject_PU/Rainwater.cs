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
using System.Threading.Tasks;

namespace FinalProject_PU
{
    [Activity(Label = "Rainwater",NoHistory =true)]
    public class Rainwater : Activity
    {
        static string selected;
        ImageView Rainwater_back, Rainwater_next, allVehicle, noVehicle,
                  highVehicle, close;
        RadioButton radiobtn1Rainwater, radiobtn2Rainwater, radiobtn3Rainwater;
        CircleImageView circleImageView_Rainwater;
        TextView Rainwater_tvusername, Rainwater_tv, tev1;
        Typeface tf;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Rainwater);

            radiobtn1Rainwater = (RadioButton)FindViewById(Resource.Id.radiobtn1Rainwater);
            radiobtn1Rainwater.Click += Radiobtn1Rainwater_Click;
            radiobtn2Rainwater = (RadioButton)FindViewById(Resource.Id.radiobtn2Rainwater);
            radiobtn2Rainwater.Click += Radiobtn2Rainwater_Click;
            radiobtn3Rainwater = (RadioButton)FindViewById(Resource.Id.radiobtn3Rainwater);
            radiobtn3Rainwater.Click += Radiobtn3Rainwater_Click;

            allVehicle = (ImageView)FindViewById(Resource.Id.allVehicle);
            allVehicle.Click += AllVehicle_Click;
            highVehicle = (ImageView)FindViewById(Resource.Id.highVehicle);
            highVehicle.Click += HighVehicle_Click;
            noVehicle = (ImageView)FindViewById(Resource.Id.noVehicle);
            noVehicle.Click += NoVehicle_Click;
            Rainwater_back = (ImageView)FindViewById(Resource.Id.backbtn6);
            Rainwater_back.Click += Rainwater_back_Click;
            Rainwater_next = (ImageView)FindViewById(Resource.Id.Rainwater_btnnext);
            Rainwater_next.Click += Rainwater_next_Click;
            close = (ImageView)FindViewById(Resource.Id.close);
            close.Click += Close_Click;

            Rainwater_tvusername = (TextView)FindViewById(Resource.Id.Rainwater_tvusername);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            Rainwater_tvusername.SetTypeface(tf, TypefaceStyle.Bold);
            Rainwater_tv = (TextView)FindViewById(Resource.Id.Rainwater_tv);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            Rainwater_tv.SetTypeface(tf, TypefaceStyle.Bold);
            tev1 = (TextView)FindViewById(Resource.Id.tev1);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            tev1.SetTypeface(tf, TypefaceStyle.Bold);

            circleImageView_Rainwater = (CircleImageView)FindViewById(Resource.Id.circleImageView_Rainwater);

            //runtime py profile change krna or name change krna 
            //start

            char[] arr = Control.UserInfoHolder.User_name.ToCharArray();
            Rainwater_tvusername.SetText(arr, 0, arr.Length);
            byte[] arra = Convert.FromBase64String(Control.UserInfoHolder.Profile_pic);


            Android.Graphics.Bitmap bitmapp = BitmapFactory.DecodeByteArray(arra, 0, arra.Length);
            circleImageView_Rainwater.SetImageBitmap(bitmapp);
            //end //runtime py profile change krna or name change krna 

        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.OnBackPressed();
        }

        private void Rainwater_back_Click(object sender, EventArgs e)
        {
            base.OnBackPressed();
        }

        private void Radiobtn3Rainwater_Click(object sender, EventArgs e)
        {
            noVehicle.PerformClick();
        }

        private void Radiobtn2Rainwater_Click(object sender, EventArgs e)
        {
            highVehicle.PerformClick();
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
        private void Radiobtn1Rainwater_Click(object sender, EventArgs e)
        {
            allVehicle.PerformClick();
        }
        //no vehhicle radio 3
        private void NoVehicle_Click(object sender, EventArgs e)
        {
            selected = "novehiclecanpass";
            radiobtn2Rainwater.Checked = false;
            radiobtn1Rainwater.Checked = false;
            radiobtn3Rainwater.Checked = true;
        }
        //radio button 2 high vehcicle

        private void HighVehicle_Click(object sender, EventArgs e)
        {
            selected = "highvehiclecanpass";
            radiobtn2Rainwater.Checked = true;
            radiobtn1Rainwater.Checked = false;
            radiobtn3Rainwater.Checked = false;

        }

        private void AllVehicle_Click(object sender, EventArgs e)
        {
            selected = "allvehiclecanpass";
            radiobtn2Rainwater.Checked = false;
            radiobtn1Rainwater.Checked = true;
            radiobtn3Rainwater.Checked = false;
        }




        private async void Rainwater_next_Click(object sender, EventArgs e)
        {
            Model.Rainwater m = new Model.Rainwater();
            m.Vehiclepassing = selected;
            m.IssueImage = JsonConvert.DeserializeObject<string>(Intent.GetStringExtra("objtopass"));
            Control.DataOper.PutData<Issuelocationpickup_Rainwater>(this, m);

            //if (selected != "")
            //{


            //    await Task.Run(() =>
            //    {
            //        var gar = JsonConvert.DeserializeObject<Model.Rainwater>(Intent.GetStringExtra("objtopass"));
            //        gar.Vehiclepassing = selected;
            //        Control.DataOper.PutData<Issuelocationpickup_Rainwater>(this, gar);


            //    });
            //}
            //else
            //{
            //    Toast.MakeText(this, "Please select any one from them", ToastLength.Long).Show();
            //}
        }


    }
}