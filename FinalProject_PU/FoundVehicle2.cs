﻿using Android.App;
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
    [Activity(Label = "FoundVehicle2",NoHistory =true)]
    public class FoundVehicle2 : Activity
    {
        static string location_lati, location_longi, LocationName;
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
            //Changing userProfile and userName at runtime
            char[] nameArr = Control.UserInfoHolder.User_name.ToCharArray();
            FoundVehicle2_tvusername.SetText(nameArr, 0, nameArr.Length);
            byte[] arr = Convert.FromBase64String(Control.UserInfoHolder.Profile_pic);
            Bitmap arrBitmap = BitmapFactory.DecodeByteArray(arr, 0, arr.Length);
            circleImageView_FoundVehicle2.SetImageBitmap(arrBitmap);
            //
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

        string APIKEY = "AIzaSyD8-hqAD2UZX-8VSVoxOpabG2zW1RnmfzE";
        MapFunctions.MapFunctionHelper mapFuncHelper;
        private async void Next_FoundVehicle2_Click(object sender, EventArgs e)
        {


            if (FoundVehicle2_radiobtn3.Checked)
            {
                var p = JsonConvert.DeserializeObject<Model.Missingvehicle>(Intent.GetStringExtra("objtopass"));
                p.issueType = "Found Vehicle";
                Control.DataOper.PutData<Issuelocationpickup_MissingVehicle>(this, p);
            }
            else
            {
                if (location_lati != "" && location_longi!="")
                {
                    await Task.Run(async () =>
                    {
                        var p = JsonConvert.DeserializeObject<Model.Missingvehicle>(Intent.GetStringExtra("objtopass"));
                        p.locationLatitude = location_lati;
                        p.locationLongitude = location_longi;
                        p.Status = "unverified";
                        p.isresolved = 0; // Issue is not resolved yet.
                        p.issueFlag = "green";
                        p.issueType = "Found Vehicle";
                        p.isWorkingStarted = 0;
                        p.amount_collected = 0;
                        p.estimated_cost = 0;



                        try
                        {

                            mapFuncHelper = new MapFunctions.MapFunctionHelper(APIKEY,null);
                            var placemark = await mapFuncHelper.FindCordinateAddress(new Android.Gms.Maps.Model.LatLng(Convert.ToDouble(location_lati), Convert.ToDouble(location_longi)));
                            if (placemark != null)
                            {

                                LocationName = placemark.Replace(", Karachi, Karachi City, Sindh, Pakistan", string.Empty);
                                p.location_name = LocationName;
                                p.issueStatement = "Unidentified vehicle found since " + p.foundDate.Date.ToShortDateString() + " with Plate No. " + p.plateNumber + " near " + LocationName;
                                Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                                AlertDialog alert = dialog.Create();

                                alert.SetMessage("Please wait while your issue is being posted ...");
                                alert.Show();
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

                        


                    });

                }
                else
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        Toast.MakeText(this, "Please select Vehicle Location", ToastLength.Long).Show();
                    });
                    
                }
            }
        }

        

       
    }
}