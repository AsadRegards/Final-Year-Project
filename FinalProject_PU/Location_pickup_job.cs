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

namespace FinalProject_PU
{
    [Activity(Label = "Location_pickup_job")]
    public class Location_pickup_job : Activity, IOnMapReadyCallback
    {
        private MapFragment map1;
        Button set_location;
        private GoogleMap googleMap;
        LatLng Final_Position;
        Button imgsearchicon;


        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.Locations);
            //initializing components
            set_location = FindViewById<Button>(Resource.Id.btn_set_1);
            //srch_button = FindViewById<Button>(Resource.Id.btn_srch_1);
            //srch_text = FindViewById<EditText>(Resource.Id.search_text);


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
                googleMap.AnimateCamera(cam);
                


            }
            catch (Exception e)
            {

            }

        }

      

        private void Set_location_Click(object sender, EventArgs e)
        {
            try
            {

                var User = JsonConvert.DeserializeObject<User>(Intent.GetStringExtra("userObjtoLocation"));
                User.joblocatlati = Final_Position.Latitude.ToString();
                User.joblocatlongi = Final_Position.Longitude.ToString();
                var intent = new Intent(this, typeof(SignUpFinal));
                intent.PutExtra("userObj", JsonConvert.SerializeObject(User));
                this.StartActivity(intent);

            }
            catch (Exception)
            {
                Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                AlertDialog alert = dialog.Create();
                alert.SetTitle("Select Location");
                alert.SetMessage("Please select correct location");
                alert.SetButton("OK", (c, ev) =>
                {
                    alert.Dismiss();
                });
                alert.Show();
            }


        }

        public async void OnMapReady(GoogleMap mapp)
        {
            googleMap = mapp;
            googleMap.MapType = GoogleMap.MapTypeNormal;
            googleMap.CameraIdle += GoogleMap_CameraIdle;

            var location = await Geolocation.GetLocationAsync(new GeolocationRequest
            {
                DesiredAccuracy = GeolocationAccuracy.Medium,
                Timeout = TimeSpan.FromSeconds(10)
            });

            if (location != null)
            {
                
                LatLng loc1 = new LatLng(location.Latitude, location.Longitude);

                CameraUpdate cam = CameraUpdateFactory.NewLatLngZoom(loc1, 17);
                googleMap.AnimateCamera(cam);
               

            }


        }

        private void GoogleMap_CameraIdle(object sender, EventArgs e)
        {
            Final_Position = googleMap.CameraPosition.Target;
        }
    }
}