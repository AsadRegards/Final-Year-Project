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
    [Activity(Label = "Rainwater")]
    public class Rainwater : Activity
    {
        static string selected;
        ImageView Rainwater_back, Rainwater_next, allVehicle, noVehicle,
                  highVehicle, iconSettngs, iconMap, iconNotifications, iconFunds, iconHome;
        RadioButton radiobtn1Rainwater, radiobtn2Rainwater, radiobtn3Rainwater;

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
          
            Rainwater_next = (ImageView)FindViewById(Resource.Id.Rainwater_btnnext);
            Rainwater_next.Click += Rainwater_next_Click;

            Rainwater_tvusername = (TextView)FindViewById(Resource.Id.Rainwater_tvusername);
            Rainwater_tv = (TextView)FindViewById(Resource.Id.Rainwater_tv);
            tev1 = (TextView)FindViewById(Resource.Id.tev1);
        }

        private void Radiobtn3Rainwater_Click(object sender, EventArgs e)
        {
            noVehicle.PerformClick();
        }

        private void Radiobtn2Rainwater_Click(object sender, EventArgs e)
        {
            highVehicle.PerformClick();
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




        private void Rainwater_next_Click(object sender, EventArgs e)
        {
            Model.Rainwater m = new Model.Rainwater();
            m.Vehiclepassing = selected;
            m.IssueImage = JsonConvert.DeserializeObject<string>(Intent.GetStringExtra("objtopass"));
            Control.DataOper.PutData<Issuelocationpickup_Rainwater>(this, m);
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