using Android.App;
using Android.Content;
using Android.Gms.Maps.Model;
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
using Xamarin.Essentials;

namespace FinalProject_PU
{
    [Activity(Label = "MissingVehicle3")]
    public class MissingVehicle3 : Activity
    {
        static string location_lati, location_longi;
        ImageView back_MissingVehicle3, next_MissingVehicle3, MissingVehicle3_HomeLocation, MissingVehicle3_WorkLocation, MissingVehicle3_OtherLocation,
              iconSettngs, iconMap, iconNotifications, iconFunds, iconHome;
        RadioButton MissingVehicle3_radiobtn1, MissingVehicle3_radiobtn2, MissingVehicle3_radiobtn3;

        CircleImageView circleImageView_MissingVehicle3;
        TextView MissingVehicle3_tvusername, MissingVehicle3_head, MissingVehicle3_info;
        Typeface tf;
        static string LocationName;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.MissingVehicle3);

            circleImageView_MissingVehicle3 = (CircleImageView)FindViewById(Resource.Id.circleImageView_MissingVehicle3);
            MissingVehicle3_tvusername = (TextView)FindViewById(Resource.Id.MissingVehicle3_tvusername);
            Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            MissingVehicle3_tvusername.SetTypeface(tf, TypefaceStyle.Bold);
            MissingVehicle3_head = (TextView)FindViewById(Resource.Id.MissingVehicle3_tev1);
            Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            MissingVehicle3_head.SetTypeface(tf, TypefaceStyle.Bold);
            MissingVehicle3_info = (TextView)FindViewById(Resource.Id.MissingVehicle3_tv);
            Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            MissingVehicle3_info.SetTypeface(tf, TypefaceStyle.Bold);
            next_MissingVehicle3 = (ImageView)FindViewById(Resource.Id.next_MissingVehicle3);
            next_MissingVehicle3.Click += Next_MissingVehicle3_Click;
            MissingVehicle3_HomeLocation = (ImageView)FindViewById(Resource.Id.MissingVehicle3_homelocation);
            MissingVehicle3_HomeLocation.Click += MissingVehicle3_HomeLocation_Click;
            MissingVehicle3_WorkLocation = (ImageView)FindViewById(Resource.Id.MissingVehicle3_WorkLocation);
            MissingVehicle3_WorkLocation.Click += MissingVehicle3_WorkLocation_Click;
            MissingVehicle3_OtherLocation = (ImageView)FindViewById(Resource.Id.FoundVehicle2_otherlocation);
            MissingVehicle3_OtherLocation.Click += MissingVehicle3_OtherLocation_Click;
            MissingVehicle3_radiobtn1 = (RadioButton)FindViewById(Resource.Id.MissingVehicle3_Radiobtn1);
            MissingVehicle3_radiobtn1.Click += MissingVehicle3_radiobtn1_Click;
            MissingVehicle3_radiobtn2 = (RadioButton)FindViewById(Resource.Id.MissingVehicle3_Radiobtn2);
            MissingVehicle3_radiobtn2.Click += MissingVehicle3_radiobtn2_Click;
            MissingVehicle3_radiobtn3 = (RadioButton)FindViewById(Resource.Id.MissingVehicle3_Radiobtn3);
            MissingVehicle3_radiobtn3.Click += MissingVehicle3_radiobtn3_Click;
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
        }

        private void MissingVehicle3_radiobtn3_Click(object sender, EventArgs e)
        {
            MissingVehicle3_OtherLocation.PerformClick();
        }

        private void MissingVehicle3_radiobtn2_Click(object sender, EventArgs e)
        {
            MissingVehicle3_WorkLocation.PerformClick();
        }

        private void MissingVehicle3_radiobtn1_Click(object sender, EventArgs e)
        {
            MissingVehicle3_HomeLocation.PerformClick();
        }

        private void MissingVehicle3_OtherLocation_Click(object sender, EventArgs e)
        {
            location_lati = FinalProject_PU.Control.UserInfoHolder.homelocatlati;
            location_longi = FinalProject_PU.Control.UserInfoHolder.homelocatlongi;
            MissingVehicle3_radiobtn3.Checked = true;
            MissingVehicle3_radiobtn2.Checked = false;
            MissingVehicle3_radiobtn1.Checked = false;
        }

        private void MissingVehicle3_WorkLocation_Click(object sender, EventArgs e)
        {
            location_lati = FinalProject_PU.Control.UserInfoHolder.joblocatlati;
            location_longi = FinalProject_PU.Control.UserInfoHolder.joblocatlongi;
            MissingVehicle3_radiobtn3.Checked = false;
            MissingVehicle3_radiobtn2.Checked = true;
            MissingVehicle3_radiobtn1.Checked = false;
        }

        private void MissingVehicle3_HomeLocation_Click(object sender, EventArgs e)
        {
            MissingVehicle3_radiobtn3.Checked = false;
            MissingVehicle3_radiobtn2.Checked = false;
            MissingVehicle3_radiobtn1.Checked = true;
        }

        private async  void Next_MissingVehicle3_Click(object sender, EventArgs e)
        {

            if (MissingVehicle3_radiobtn3.Checked)
            {
                var p = JsonConvert.DeserializeObject<Model.Missingvehicle>(Intent.GetStringExtra("objtopass"));
                Control.DataOper.PutData<Issuelocationpickup_MissingVehicle>(this, p);
            }
            else
            {
                await Task.Run(async () =>
                {
                    var p = JsonConvert.DeserializeObject<Model.Missingvehicle>(Intent.GetStringExtra("objtopass"));
                    p.locationLatitude = location_lati;
                    p.locationLongitude = location_longi;
                    p.Status = "unverified";
                    p.issueDate = DateTime.Now;
                    
                    Xamarin.Essentials.Location loc = new Location();
                    loc.Latitude = Convert.ToDouble(location_lati);
                    loc.Longitude = Convert.ToDouble(location_longi);

                    try
                    {


                        var placemarks = await Geocoding.GetPlacemarksAsync(Convert.ToDouble(location_lati), Convert.ToDouble(location_longi));

                        var placemark = placemarks?.FirstOrDefault();
                        if (placemark != null)
                        {

                            LocationName = placemark.SubLocality;
                            p.issueStatement = "Vehicle gone Missing since" + p.missingDate.Date + "Plate No." + p.plateNumber + "near" + LocationName;
                            await Control.IssueController.PostIssue<Model.Missingvehicle>(p, this);
                        }
                    }
                    catch (FeatureNotSupportedException fnsEx)
                    {
                        // Feature not supported on device
                    }
                    catch (Exception ex)
                    {
                        // Handle exception that may have occurred in geocoding
                    }

                    await Control.IssueController.PostIssue<Model.Missingvehicle>(p, this);


                });
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