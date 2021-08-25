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
    [Activity(Label = "IssueLocationPickup_Manhole",NoHistory =true)]
    public class IssueLocationPickup_Manhole : Activity, IOnMapReadyCallback
    {

        private MapFragment map1; Button set_location; private GoogleMap googleMap; LatLng Final_Position;
        static LatLng current_location;
        Button imgsearchicon;
        static string LocationName;
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
                googleMap.MoveCamera(cam);

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
                googleMap.MoveCamera(cam);
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
                PlacesApi.Initialize(this, APIKEY);
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

                loader.Visibility = Android.Views.ViewStates.Visible;
                set_location.Enabled = false;

                var Issue = JsonConvert.DeserializeObject<Manhole>(Intent.GetStringExtra("objtopass"));
                Issue.locationLatitude = Final_Position.Latitude.ToString();
                Issue.locationLongitude = Final_Position.Longitude.ToString();
                Issue.Status = "unverified";
                Issue.issueType = "Manhole";
                Issue.isWorkingStarted = 0;
                Issue.amount_collected = 0;
                Issue.estimated_cost = 0;
                //using (Control.IssueFlagDetector i = new Control.IssueFlagDetector())
                //{
                //    Issue.issueFlag = i.DetectManholeFlag(Issue);
                //}
                Issue.issueFlag = new Control.IssueFlagDetector().DetectManholeFlag(Issue);
                Issue.issueDate = DateTime.Now;
                try
                {


                    mapFuncHelper = new MapFunctions.MapFunctionHelper(APIKEY, googleMap);
                    var LocationName = await mapFuncHelper.FindCordinateAddress(Final_Position);
                    if (LocationName == " ")
                    {


                        loader.Visibility = Android.Views.ViewStates.Gone;
                        set_location.Enabled = true;
                        Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                        AlertDialog alert = dialog.Create();
                        alert.SetTitle("Zoom In");
                        alert.SetMessage("Please be more specific when selecting location, you can zoom in the map to point the exact location.");
                        alert.SetButton("OK", (c, ev) =>
                        {
                            alert.Dismiss();
                        });
                        alert.Show();



                    }
                    else
                    {

                        string s_stat;
                        if (Issue.Iswateroverflow == true)
                        {
                            s_stat = " spilling dirty water ";
                            Issue.issueStatement = "Open Manhole" + s_stat + "near " + LocationName;
                            

                        }
                        else if (Issue.Iswateroverflow == false)
                        {
                            s_stat = "Open ";
                            Issue.issueStatement = s_stat + "Manhole " + "near " + LocationName;
                            

                        }
                        //
                        Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                        AlertDialog alert = dialog.Create();
                        //alert.SetContentView(Resource.Layout.createisue3);
                        alert.SetMessage("Please wait while your issue is being posted ...");
                        alert.Show();

                        _ = await Control.IssueController.PostIssue<Model.Manhole>(Issue, this);


                    }

                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
                }

            }



            catch (Exception)
            {
                set_location.Enabled = true;

            }


        }
    }
}

