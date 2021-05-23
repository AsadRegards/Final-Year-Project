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

namespace FinalProject_PU
{
    [Activity(Label = "Issuelocationpickup_FoundVehicle")]
    public class Issuelocationpickup_FoundVehicle : Activity ,IOnMapReadyCallback, ILocationSourceOnLocationChangedListener
    {
        static string LocationName;
        private MapFragment map1;
        Button set_location;
        private GoogleMap googleMap;
        LatLng Final_Position;
        Button imgsearchicon;
        LoadingView loader;

        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.Locations);
            //initializing components
            set_location = FindViewById<Button>(Resource.Id.btn_set_1);
            //srch_button = FindViewById<Button>(Resource.Id.btn_srch_1);
            //srch_text = FindViewById<EditText>(Resource.Id.search_text);
            loader = FindViewById<LoadingView>(Resource.Id.loading_view);


            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            map1 = MapFragment.NewInstance();
            var ft = FragmentManager.BeginTransaction();
            ft.Add(Resource.Id.map_placeholder, map1).Commit();
            map1.GetMapAsync(this);

            imgsearchicon = (Button)FindViewById(Resource.Id.btnsearchimg);
            



            //setting on click events
            set_location.Click += Set_location_Click;
            imgsearchicon.Click += Imgsearchicon_Click;

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

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            try
            {
                var place = Autocomplete.GetPlaceFromIntent(data);
                //getting location of the searched place
                var loc = place.LatLng;

                //creating camera update options to move camera to the searched location
                CameraUpdate cam = CameraUpdateFactory.NewLatLngZoom(loc, 17);

                //create marker options
                MarkerOptions markerOption = new MarkerOptions();
                markerOption.SetPosition(loc);
                markerOption.Draggable(true);
                int id0 = (int)typeof(Resource.Drawable).GetField("locationpoint").GetValue(null);
                BitmapDescriptor bmd0 = BitmapDescriptorFactory.FromResource(id0);
                markerOption.SetIcon(bmd0);
                //googleMap.Clear();
                googleMap.AddMarker(markerOption);
                googleMap.MoveCamera(cam);
                googleMap.MarkerDragEnd += GoogleMap_MarkerDragEnd1;


            }
            catch (Exception e)
            {

            }






        }

        private void GoogleMap_MarkerDragEnd1(object sender, GoogleMap.MarkerDragEndEventArgs e)
        {
            Final_Position = e.Marker.Position;

        }

        private async void Set_location_Click(object sender, EventArgs e)
        {
            
            try
            {
                loader.Visibility = Android.Views.ViewStates.Visible;
                set_location.Enabled = false;

                var p = JsonConvert.DeserializeObject<Model.Missingvehicle>(Intent.GetStringExtra("objtopass"));
                p.locationLatitude = Final_Position.Latitude.ToString();
                p.locationLongitude = Final_Position.Longitude.ToString();
                p.Status = "unverified";
                p.isWorkingStarted = 0;
                p.amount_collected = 0;
                p.estimated_cost = 0;
                p.isresolved = 0;
                try
                {


                    var placemarks = await Geocoding.GetPlacemarksAsync (Final_Position.Latitude, Final_Position.Longitude);

                    var placemark = placemarks?.FirstOrDefault();
                    if (placemark != null)
                    {

                        LocationName = placemark.SubLocality;
                        p.issueStatement = "Vehicle found on" + p.missingDate.Date + "Plate No." + p.plateNumber + "near" + LocationName;
                        await Control.IssueController.PostIssue<Missingvehicle>(p, this);
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



            }
            catch (Exception)
            {
                Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                AlertDialog alert = dialog.Create();
                alert.SetTitle("Select Location");
                alert.SetMessage("Please select issue location by dragging pin on Issue Location");
                alert.SetButton("OK", (c, ev) =>
                {
                    alert.Dismiss();
                });
                alert.Show();
                set_location.Enabled = true;
                loader.Visibility = Android.Views.ViewStates.Gone;
            }


        }

        public async void OnMapReady(GoogleMap mapp)
        {
            googleMap = mapp;
            googleMap.MapType = GoogleMap.MapTypeNormal;

            var location = await Geolocation.GetLocationAsync(new GeolocationRequest
            {
                DesiredAccuracy = GeolocationAccuracy.Medium,
                Timeout = TimeSpan.FromSeconds(10)
            });

            if (location != null)
            {
                LatLng loc = new LatLng(location.Latitude, location.Longitude + 0.002);
                LatLng loc1 = new LatLng(location.Latitude, location.Longitude);

                CameraUpdate cam = CameraUpdateFactory.NewLatLngZoom(loc, 17);
                MarkerOptions m = new MarkerOptions();
                m.Draggable(true);
                m.SetPosition(loc);
                int id0 = (int)typeof(Resource.Drawable).GetField("locationpoint").GetValue(null);
                BitmapDescriptor bmd0 = BitmapDescriptorFactory.FromResource(id0);
                m.SetIcon(bmd0);


                MarkerOptions mo2 = new MarkerOptions();
                mo2.SetPosition(loc1);
                int id = (int)typeof(Resource.Drawable).GetField("locationpin").GetValue(null);
                BitmapDescriptor bmd = BitmapDescriptorFactory.FromResource(id);
                mo2.SetIcon(bmd);



                googleMap.MoveCamera(cam);
                googleMap.AddMarker(m);
                googleMap.AddMarker(mo2);
                googleMap.MarkerDragEnd += GoogleMap_MarkerDragEnd;

            }


        }

        private void GoogleMap_MarkerDragEnd(object sender, GoogleMap.MarkerDragEndEventArgs e)
        {
            Final_Position = e.Marker.Position;
        }

        public void OnLocationChanged(Android.Locations.Location location)
        {

            LatLng newloc = new LatLng(location.Latitude, location.Longitude);
            MarkerOptions m = new MarkerOptions();
            m.SetPosition(newloc);
            int id0 = (int)typeof(Resource.Drawable).GetField("locationpoint").GetValue(null);
            BitmapDescriptor bmd0 = BitmapDescriptorFactory.FromResource(id0);
            m.SetIcon(bmd0);
            googleMap.AddMarker(m);


        }
    }
}
