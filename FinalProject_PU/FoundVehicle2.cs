using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using FinalProject_PU.Model;
using Newtonsoft.Json;
using Refractored.Controls;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace FinalProject_PU
{
    [Activity(Label = "FoundVehicle2")]
    public class FoundVehicle2 : Activity
    {
        static string location_lati, location_longi, LocationName,selected;
        ImageView back_FoundVehicle2, next_FoundVehicle2, FoundVehicle2_HomeLocation, FoundVehicle2_WorkLocation, FoundVehicle2_OtherLocation,
                 iconSettngs, iconMap, iconNotifications, iconFunds, iconHome;
        RadioButton FoundVehicle2_radiobtn1, FoundVehicle2_radiobtn2, FoundVehicle2_radiobtn3;

        CircleImageView circleImageView_FoundVehicle2;
        TextView FoundVehicle2_tvusername, FoundVehicle2_head, FoundVehicle2_info;
        Typeface tf;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.FoundVehicle2);
            circleImageView_FoundVehicle2 = (CircleImageView)FindViewById(Resource.Id.circleImageView_FoundVehicle2);
            FoundVehicle2_tvusername = (TextView)FindViewById(Resource.Id.FoundVehicle2_tvusername);
            Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            FoundVehicle2_tvusername.SetTypeface(tf, TypefaceStyle.Bold);
            FoundVehicle2_head = (TextView)FindViewById(Resource.Id.FoundVehicle2_tev1);
            Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            FoundVehicle2_head.SetTypeface(tf, TypefaceStyle.Bold);
            FoundVehicle2_info = (TextView)FindViewById(Resource.Id.FoundVehicle2_tv);
            Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            FoundVehicle2_info.SetTypeface(tf, TypefaceStyle.Bold);
            next_FoundVehicle2 = (ImageView)FindViewById(Resource.Id.next_FoundVehicle2);
            next_FoundVehicle2.Click += Next_FoundVehicle2_Click;
            FoundVehicle2_HomeLocation = (ImageView)FindViewById(Resource.Id.FoundVehicle2_homelocation);
            FoundVehicle2_HomeLocation.Click += FoundVehicle2_HomeLocation_Click;
            FoundVehicle2_WorkLocation = (ImageView)FindViewById(Resource.Id.FoundVehicle2_worklocation);
            FoundVehicle2_WorkLocation.Click += FoundVehicle2_WorkLocation_Click;
            FoundVehicle2_OtherLocation = (ImageView)FindViewById(Resource.Id.FoundVehicle2_otherlocation);
            FoundVehicle2_OtherLocation.Click += FoundVehicle2_OtherLocation_Click;
            FoundVehicle2_radiobtn1 = (RadioButton)FindViewById(Resource.Id.FoundVehicle2_Radiobtn1);
            FoundVehicle2_radiobtn1.Click += FoundVehicle2_radiobtn1_Click;
            FoundVehicle2_radiobtn2 = (RadioButton)FindViewById(Resource.Id.FoundVehicle2_Radiobtn2);
            FoundVehicle2_radiobtn2.Click += FoundVehicle2_radiobtn2_Click;
            FoundVehicle2_radiobtn3 = (RadioButton)FindViewById(Resource.Id.FoundVehicle2_Radiobtn3);
            FoundVehicle2_radiobtn3.Click += FoundVehicle2_radiobtn3_Click;
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

        private void FoundVehicle2_radiobtn3_Click(object sender, EventArgs e)
        {
            FoundVehicle2_OtherLocation.PerformClick();
        }

        private void FoundVehicle2_radiobtn2_Click(object sender, EventArgs e)
        {
            FoundVehicle2_WorkLocation.PerformClick();
        }

        private void FoundVehicle2_radiobtn1_Click(object sender, EventArgs e)
        {
            FoundVehicle2_HomeLocation.PerformClick();
        }

        private void FoundVehicle2_OtherLocation_Click(object sender, EventArgs e)
        {
            
            FoundVehicle2_radiobtn3.Checked = true;
            FoundVehicle2_radiobtn2.Checked = false;
            FoundVehicle2_radiobtn1.Checked = false;
        }

        private void FoundVehicle2_WorkLocation_Click(object sender, EventArgs e)
        {
            location_lati = FinalProject_PU.Control.UserInfoHolder.joblocatlati;
            location_longi = FinalProject_PU.Control.UserInfoHolder.joblocatlongi;
            FoundVehicle2_radiobtn3.Checked = false;
            FoundVehicle2_radiobtn2.Checked = true;
            FoundVehicle2_radiobtn1.Checked = false;
        }

        private void FoundVehicle2_HomeLocation_Click(object sender, EventArgs e)
        {
            location_lati = FinalProject_PU.Control.UserInfoHolder.homelocatlati;
            location_longi = FinalProject_PU.Control.UserInfoHolder.homelocatlongi;
            FoundVehicle2_radiobtn3.Checked = false;
            FoundVehicle2_radiobtn2.Checked = false;
            FoundVehicle2_radiobtn1.Checked = true;
        }

        private async void Next_FoundVehicle2_Click(object sender, EventArgs e)
        {
            if (selected != "")
            {
                if (FoundVehicle2_radiobtn3.Checked)
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

                        await Control.IssueController.PostIssue<Missingvehicle>(p, this);


                    });
                }
            }
            else
            {
                Toast.MakeText(this, "Please fill out the data!", ToastLength.Long).Show();
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