using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Google.Places;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace FinalProject_PU
{
    public class DirectionFragment : AndroidX.Fragment.App.Fragment,IOnMapReadyCallback
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        string APIKEY = "AIzaSyD8-hqAD2UZX-8VSVoxOpabG2zW1RnmfzE";
        Button GetDirection;
        TextView startLocation, endLocation;
        RadioButton radioStart, radioEnd;

        MapFragment mapFragment;
        GoogleMap googleMap;

        LatLng startCoardinates, endCoardinates;

        public override Android.Views.View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            var view=inflater.Inflate(Resource.Layout.DirectionMapLayout, container, false);
            Xamarin.Essentials.Platform.Init(this.Activity, savedInstanceState);

            //Initializing UI component
            GetDirection = view.FindViewById<Button>(Resource.Id.getdirection);
            startLocation = view.FindViewById<TextView>(Resource.Id.materialTextView1);
            endLocation = view.FindViewById<TextView>(Resource.Id.materialTextView2);
            radioStart = view.FindViewById<RadioButton>(Resource.Id.radioButton1);
            radioEnd = view.FindViewById<RadioButton>(Resource.Id.radioButton2);

            //Initializing Click events
            startLocation.Click += StartLocation_Click;
            endLocation.Click += EndLocation_Click;
            radioStart.Click += RadioStart_Click;
            radioEnd.Click += RadioEnd_Click;
            GetDirection.Click += GetDirection_Click;

            //Setting up Map
            mapFragment = MapFragment.NewInstance();
            var ft = this.Activity.FragmentManager.BeginTransaction();
            ft.Add(Resource.Id.map_placeholder, mapFragment).Commit();
            mapFragment.GetMapAsync(this);

            

            return view;
        }

        
        private void RadioEnd_Click(object sender, EventArgs e)
        {
            radioEnd.Checked = true;
            radioStart.Checked = false;
        }

        private void RadioStart_Click(object sender, EventArgs e)
        {
            radioStart.Checked = true;
            radioEnd.Checked = false;
        }

        
        private async void GetDirection_Click(object sender, EventArgs e)
        {
            char[] msg = "PLEASE WAIT....".ToCharArray();
            GetDirection.SetText(msg, 0, msg.Length);
            GetDirection.Enabled = false;
            if(startCoardinates!=null && endCoardinates!=null)
            {
                string json = await mapFuncHelper.GetDirectionJsonAsync(startCoardinates, endCoardinates);
                if (json != null && json != "")
                {
                    mapFuncHelper.DrawTripOnMap(json);

                }
                else
                {
                    Toast.MakeText(this.Activity, "Direction cannot be provided at this time", ToastLength.Long).Show();
                }
                GetDirection.Enabled = true;
                GetDirection.SetText("GET DIRECTION".ToCharArray(), 0, "GET DIRECTION".ToCharArray().Length);
              


            }
            else
            {
                Toast.MakeText(this.Activity, "Please select Start and End Locaton", ToastLength.Long).Show();
                GetDirection.Enabled = true;
                GetDirection.SetText("GET DIRECTION".ToCharArray(), 0, "GET DIRECTION".ToCharArray().Length);

            }
            
        }

  
        private void EndLocation_Click(object sender, EventArgs e)
        {

            if (!radioStart.Checked && !radioEnd.Checked)
            {
                Toast.MakeText(Application.Context, "Please select one Type\nDestination OR Location", ToastLength.Long).Show();

            }
            else
            {
                if (!PlacesApi.IsInitialized)
                {
                    PlacesApi.Initialize(this.Activity, APIKEY);
                }

                List<Place.Field> fields = new List<Place.Field>();


                fields.Add(Place.Field.Id);
                fields.Add(Place.Field.Name);
                fields.Add(Place.Field.LatLng);
                fields.Add(Place.Field.Address);


                Intent intent = new Autocomplete.IntentBuilder(AutocompleteActivityMode.Overlay, fields)
                    .Build(this.Activity);
                StartActivityForResult(intent, 0);
            }

        }

        private void StartLocation_Click(object sender, EventArgs e)
        {
            if(!radioStart.Checked && !radioEnd.Checked)
            {
                Toast.MakeText(Application.Context, "Please select one Type\nDestination OR Location", ToastLength.Long).Show();

            }
            else
            {
                if (!PlacesApi.IsInitialized)
                {
                    PlacesApi.Initialize(this.Activity, "AIzaSyD8-hqAD2UZX-8VSVoxOpabG2zW1RnmfzE");
                }

                List<Place.Field> fields = new List<Place.Field>();


                fields.Add(Place.Field.Id);
                fields.Add(Place.Field.Name);
                fields.Add(Place.Field.LatLng);
                fields.Add(Place.Field.Address);


                Intent intent = new Autocomplete.IntentBuilder(AutocompleteActivityMode.Overlay, fields)
                    .Build(this.Activity);
                StartActivityForResult(intent, 0);
            }
          
            
        }

        public async void OnMapReady(GoogleMap googleMap)
        {
            this.googleMap = googleMap;
            LatLng current_location = await getCurrentLocation();
            googleMap.CameraIdle += GoogleMap_CameraIdle;
            mapFuncHelper = new MapFunctions.MapFunctionHelper(APIKEY, googleMap);
            googleMap.MapType = GoogleMap.MapTypeNormal;
            try
            {
                CameraUpdate cam = CameraUpdateFactory.NewLatLngZoom(current_location, 15);
                googleMap.AnimateCamera(cam);
                showMarkerOnMap();
            }
            catch (Exception ex)
            {
                Toast.MakeText(Application.Context, "Current Location cannot be detected at the moment", ToastLength.Long).Show();
            }
        }

        MapFunctions.MapFunctionHelper mapFuncHelper;
        private async void GoogleMap_CameraIdle(object sender, EventArgs e)
        {
            
            if (radioStart.Checked == true)
            {
                startCoardinates = googleMap.CameraPosition.Target;
                string startLoc = await mapFuncHelper.FindCordinateAddress(startCoardinates);
                char[] startLocArray = startLoc.ToCharArray();
                startLocation.SetText(startLocArray, 0, startLocArray.Length);
            }
            else if (radioEnd.Checked == true)
            {
                endCoardinates = googleMap.CameraPosition.Target;
                string endLoc = await mapFuncHelper.FindCordinateAddress(endCoardinates);
                char[] endLocArray = endLoc.ToCharArray();
                endLocation.SetText(endLocArray, 0, endLocArray.Length);
            }
        }

        private async Task<LatLng> getCurrentLocation()
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

        public async override  void OnActivityResult(int requestCode, int resultCode, Intent data)
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
                if (radioStart.Checked == true)
                {
                    startCoardinates = googleMap.CameraPosition.Target;
                    string startLoc = await mapFuncHelper.FindCordinateAddress(startCoardinates);
                    char[] startLocArray = startLoc.ToCharArray();
                    startLocation.SetText(startLocArray, 0, startLocArray.Length);
                }
                else if (radioEnd.Checked == true)
                {
                    endCoardinates = googleMap.CameraPosition.Target;
                    string endLoc = await mapFuncHelper.FindCordinateAddress(endCoardinates);
                    char[] endLocArray = endLoc.ToCharArray();
                    endLocation.SetText(endLocArray, 0, endLocArray.Length);
                }

            }
            catch (Exception e)
            {

            }

        }

        public  async Task<List<Helper.Data>> FetchPostList()
        {

            HttpClient client = new HttpClient();
            var uri = Control.Account.BaseAddressUri + "/api/issue/fetchallissues";
            var response = await client.GetStringAsync(uri);
            var list = JsonConvert.DeserializeObject<List<Helper.Data>>(response);
            return list;
        }

        public async Task<bool> showMarkerOnMap()
        {
            var issueList = await FetchPostList();
            foreach(var issue in issueList)
            {
                MarkerOptions mOptions = new MarkerOptions();
                
                mOptions.SetPosition(issue.GetLocation());
                if(issue.Issueflag=="green")
                {
                    mOptions.SetTitle(issue.IssueType + ": No Blockage");
                    mOptions.SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueGreen));
                    googleMap.AddMarker(mOptions);
                }
                else if(issue.Issueflag=="yellow")
                {
                    mOptions.SetTitle(issue.IssueType + ": Partial Blockage");
                    mOptions.SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueYellow));
                    googleMap.AddMarker(mOptions);

                }
                else if(issue.Issueflag=="red")
                {
                    mOptions.SetTitle(issue.IssueType + ": Full Blockage");
                    mOptions.SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueRed));
                    googleMap.AddMarker(mOptions);
                }
              
            }
            return true;
        }

    }
}