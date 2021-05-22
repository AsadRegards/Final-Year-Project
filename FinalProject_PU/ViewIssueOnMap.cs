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
    [Activity(Label = "ViewIssueOnMap")]
    public class ViewIssueOnMap : Activity, IOnMapReadyCallback
    {

        private MapFragment map1;  private GoogleMap googleMap;
        private LatLng issuePosition;
        Button goBack;
      


        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layout1);

            string issuePositionLatitude = JsonConvert.DeserializeObject<string>(Intent.GetStringExtra("lattopass"));
            string issuePositionLongitude = JsonConvert.DeserializeObject<string>(Intent.GetStringExtra("longtopass"));
            issuePosition = new LatLng(Convert.ToDouble(issuePositionLatitude), Convert.ToDouble(issuePositionLongitude));
            map1 = MapFragment.NewInstance();
            var ft = FragmentManager.BeginTransaction();
            ft.Add(Resource.Id.map_placeholder, map1).Commit();
            MainThread.BeginInvokeOnMainThread(() =>
            {
                map1.GetMapAsync(this);
            });

            goBack = FindViewById<Button>(Resource.Id.backbutton);
            goBack.Click += GoBack_Click;

           
        }

        private void GoBack_Click(object sender, EventArgs e)
        {
            base.OnBackPressed();
        }

        public void OnMapReady(GoogleMap mapp)
        {
            googleMap = mapp;
            
            mapp.UiSettings.MyLocationButtonEnabled = true;

            googleMap.MapType = GoogleMap.MapTypeNormal;

            MarkerOptions m = new MarkerOptions();
            m.SetPosition(issuePosition);
            m.SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueBlue));
            m.Draggable(false);


            googleMap.AddMarker(m);
            CameraUpdate c = CameraUpdateFactory.NewLatLngZoom(issuePosition, 17);
            googleMap.AnimateCamera(c);
            


        }

       



        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        


        


    }
}

