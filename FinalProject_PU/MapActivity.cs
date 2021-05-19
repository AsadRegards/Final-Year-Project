using Android.App;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Support.V4.App;
using Android.Widget;
using System;
using System.Collections;
using System.Collections.Generic;
using Google.Places;
using Xamarin.Essentials;
using Android.Content;

namespace FinalProject_PU
{
    [Activity(Label = "MapActivity")]
        
    public class MapActivity : Activity, IOnMapReadyCallback, ILocationSourceOnLocationChangedListener
    {
     
        private MapFragment map1;
        Button set_location, srch_button;
        EditText srch_text;
        private GoogleMap googleMap;
        LatLng Final_Position;
        

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

            //setting on click events
            set_location.Click += Set_location_Click;
            //srch_button.Click += Srch_button_Click;

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
                .SetCountry("PK")
                .Build(this);
            StartActivityForResult(intent, 0);

        }

        //search button
        private async void Srch_button_Click(object sender, EventArgs e)
        {
            List<Location> loca = new List<Location>();   
            loca=(List<Location>) await Geocoding.GetLocationsAsync(srch_text.Text);
            var loc = loca[0];
            LatLng latlng_src = new LatLng(loc.Latitude, loc.Longitude);
            MarkerOptions mo = new MarkerOptions();
            mo.SetPosition(latlng_src);
            mo.Draggable(true);

            googleMap.AddMarker(mo);
            CameraUpdate camup = CameraUpdateFactory.NewLatLngZoom(latlng_src, 17);
            googleMap.MoveCamera(camup);
        }

        private void Set_location_Click(object sender, EventArgs e)
        {
            try
            {
                string pos = Final_Position.Latitude.ToString() + "%%%" + Final_Position.Longitude.ToString();
                Toast.MakeText(this, pos, ToastLength.Long).Show();
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
            }
            
            
        }

        public async void OnMapReady(GoogleMap mapp)
        {
            googleMap=mapp;
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
            Final_Position= e.Marker.Position;
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