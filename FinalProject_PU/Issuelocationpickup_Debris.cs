using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Widget;
using Google.Places;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using FinalProject_PU.Model;
using System.Linq;
using MohammedAlaa.GifLoading;
using System.ComponentModel;
using System.Threading.Tasks;
using Android.Locations;

namespace FinalProject_PU
{
    [Activity(Label = "Issuelocationpickup_Debris")]
    public class Issuelocationpickup_Debris : Activity, IOnMapReadyCallback
    {

        private MapFragment map1; Button set_location; private GoogleMap googleMap; LatLng Final_Position;
        static LatLng current_location;
        Button imgsearchicon;

        LoadingView loader;
        BackgroundWorker worker, worker2;


        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);


            // Create your application here

            SetContentView(Resource.Layout.Locations);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            //initializing components
            set_location = FindViewById<Button>(Resource.Id.btn_set_1);
            imgsearchicon = (Button)FindViewById(Resource.Id.btnsearchimg);
            Button view = FindViewById<Button>(Resource.Id.pinid);

            loader = FindViewById<LoadingView>(Resource.Id.loading_view);

            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerAsync();




            //setting on click events
            set_location.Click += Set_location_Click;
            imgsearchicon.Click += Imgsearchicon_Click;



        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {

            map1 = MapFragment.NewInstance();
            var ft = FragmentManager.BeginTransaction();
            ft.Add(Resource.Id.map_placeholder, map1).Commit();
            MainThread.BeginInvokeOnMainThread(() =>
            {
                map1.GetMapAsync(this);
            });


        }

        public async Task<LatLng> getCurrentLocation()
        {
            var location = await Geolocation.GetLocationAsync(new GeolocationRequest
            {
                DesiredAccuracy = GeolocationAccuracy.Medium,
                Timeout = TimeSpan.FromSeconds(10)
            });
            if (location != null)
            {

                var location_current = new LatLng(location.Latitude, location.Longitude);
                return location_current;

            }
            return new LatLng(63, 42);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            try
            {
                var place = Autocomplete.GetPlaceFromIntent(data);
                //getting location of the searched place
                var loc = place.LatLng;
                //creating camera update options to move camera to the searched location
                CameraUpdate cam = CameraUpdateFactory.NewLatLngZoom(loc, 15);
                googleMap.AnimateCamera(cam);

            }
            catch (Exception e)
            {

            }
        }

        public async void OnMapReady(GoogleMap mapp)
        {
            googleMap = mapp;
            current_location = await getCurrentLocation();
            mapp.UiSettings.MyLocationButtonEnabled = true;

            googleMap.MapType = GoogleMap.MapTypeNormal;
            try
            {
                CameraUpdate cam = CameraUpdateFactory.NewLatLngZoom(current_location, 15);
                googleMap.AnimateCamera(cam);
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, "Current Location cannot be detected at the moment", ToastLength.Long).Show();
            }

        }


        private void Imgsearchicon_Click(object sender, EventArgs e)
        {

            if (!PlacesApi.IsInitialized)
            {
                PlacesApi.Initialize(this, "AIzaSyD8-hqAD2UZX-8VSVoxOpabG2zW1RnmfzE");
            }

            List<Place.Field> fields = new List<Place.Field>();


            fields.Add(Place.Field.Id);
            fields.Add(Place.Field.Name);
            fields.Add(Place.Field.LatLng);
            fields.Add(Place.Field.Address);


            Intent intent = new Autocomplete.IntentBuilder(AutocompleteActivityMode.Overlay, fields)
                .Build(this);
            StartActivityForResult(intent, 0);

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        string APIKEY = "AIzaSyD8-hqAD2UZX-8VSVoxOpabG2zW1RnmfzE";
        MapFunctions.MapFunctionHelper mapFuncHelper;
        private async void Set_location_Click(object sender, EventArgs e)
        {
            Final_Position = googleMap.CameraPosition.Target;

            try
            {


                var p = JsonConvert.DeserializeObject<Model.Debris>(Intent.GetStringExtra("objtopass"));
                p.locationLatitude = Final_Position.Latitude.ToString();
                p.locationLongitude = Final_Position.Longitude.ToString();
                p.Status = "unverified";
                p.issueFlag = new Control.IssueFlagDetector().DetectDebrisFlag(p);
                p.isresolved = 0;
                p.issueType = "Debris";



                try
                {

                    mapFuncHelper = new MapFunctions.MapFunctionHelper(APIKEY, googleMap);
                    p.location_name = await mapFuncHelper.FindCordinateAddress(Final_Position);
                    p.location_name = p.location_name.Replace(", Karachi, Karachi City, Sindh, Pakistan", string.Empty);
                    string s_statement;
                    if (p.roadCoverage == "halfblocked")
                    {
                        s_statement = " is blocking half road ";
                        p.issueStatement = "Debris " + s_statement + " near "+p.location_name;
                        
                    }
                    else if (p.roadCoverage == "fullblocked")
                    {
                        s_statement = " is blocking full road ";
                        p.issueStatement = "Debris " + s_statement + " near " + p.location_name;

                    }
                    else if (p.roadCoverage == "sidearea")
                    {
                        s_statement = " blocking a side of road ";
                        p.issueStatement = "Debris " + s_statement + " near " + p.location_name;

                    }
                    //Alert Dialogue Box
                    Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                    AlertDialog alert = dialog.Create();
                    alert.SetMessage("Please wait while your issue is being posted ...");
                    alert.Show();
                    await Control.IssueController.PostIssue<Model.Debris>(p, this);

                }

                catch (Exception ex)
                {
                    // Handle exception that may have occurred in geocoding or posting issue.
                }



            }
            catch (Exception)
            {
                set_location.Enabled = true;
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Toast.MakeText(this, "Unfortunately! Your Issue cannot be posted at this time", ToastLength.Long).Show();
                });
            }


        }


    }
}

