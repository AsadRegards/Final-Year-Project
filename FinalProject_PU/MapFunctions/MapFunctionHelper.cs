using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Graphics;
using Com.Google.Maps.Android;
using Java.Util;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using yucee.Helpers;

namespace FinalProject_PU.MapFunctions
{
    public class MapFunctionHelper
    {
        string mapkey;
        GoogleMap googleMap;

        public MapFunctionHelper(string mMapkey, GoogleMap mmap)
        {
            mapkey = mMapkey;
            googleMap = mmap;
        }

        public string GetGeoCodeUrl(double lat, double lng)
        {
            string url = "https://maps.googleapis.com/maps/api/geocode/json?latlng=" + lat + "," + lng + "&key=" + mapkey;
            return url;
        }

        public async Task<string> GetGeoJsonAsync(string url)
        {
            
            var handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            string result = await client.GetStringAsync(url);
            return result;
        }

        public async Task<string> FindCordinateAddress(LatLng position)
        {
            string url = GetGeoCodeUrl(position.Latitude, position.Longitude);
            string json = "";
            string placeAddress = "";

            //Check for Internet connection
            json = await GetGeoJsonAsync(url);

            if (!string.IsNullOrEmpty(json))
            {
                var geoCodeData = JsonConvert.DeserializeObject<GeocodingParser>(json);
                if (!geoCodeData.status.Contains("ZERO"))
                {
                    if (geoCodeData.results[0] != null)
                    {
                        placeAddress = geoCodeData.results[0].formatted_address;
                    }
                }
            }

            return placeAddress;
        }

        public async Task<string> GetDirectionJsonAsync(LatLng location, LatLng destination)
        {
            //Origin of route
            string str_origin = "origin=" + location.Latitude + "," + location.Longitude;
            //Destination of route
            string str_destination = "destination=" + destination.Latitude + "," + destination.Longitude;
            //mode
            string mode = "mode=driving";
            //Building the parameters to the webservice
            string parameters = str_origin + "&" + str_destination + "&" + "&" + mode + "&key=";
            //output format
            string output = "json";
            string key = mapkey;
            //Building the final url string
            string url = "https://maps.googleapis.com/maps/api/directions/" + output + "?" + parameters + key;
            string json = "";
            json = await GetGeoJsonAsync(url);
            return json;
        }

        Android.Gms.Maps.Model.Polyline mPolyline;
        Marker PickupMarker, destinationMarker;
        public void DrawTripOnMap(string Json)
        {
            // TryCatch to handle any null Object exception.
            try
            {
                mPolyline.Remove();
                PickupMarker.Remove();
                destinationMarker.Remove();
            } 
            catch (Exception)
            {
                
            } 
            var directionData = JsonConvert.DeserializeObject<DirectionParser>(Json);

            //Decode Encoded route
            var Points = directionData.routes[0].overview_polyline.points;

            var Line = PolyUtil.Decode(Points);

            ArrayList routeList = new ArrayList();
            foreach(LatLng item in Line)
            {
                routeList.Add(item);
            }

            //Draw Polylines on map
            PolylineOptions polylineOptions = new PolylineOptions()
                .AddAll(routeList)
                .InvokeWidth(10)
                .InvokeColor(Color.BlueViolet)
                .InvokeStartCap(new SquareCap())
                .InvokeEndCap(new RoundCap())
                .InvokeJointType(JointType.Round)
                .Geodesic(true);


            
            mPolyline = googleMap.AddPolyline(polylineOptions);

            //Get first and last point
            LatLng firstpoint = Line[0];
            LatLng lastpoint = Line[Line.Count - 1];

            //Creating Marker Options
            MarkerOptions pickupMarkerOptions = new MarkerOptions();
            pickupMarkerOptions.SetPosition(firstpoint);
            pickupMarkerOptions.SetTitle("Start Location");
            pickupMarkerOptions.SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueGreen));


            MarkerOptions destinationMarkerOptions = new MarkerOptions();
            destinationMarkerOptions.SetPosition(lastpoint);
            destinationMarkerOptions.SetTitle("Destination");
            pickupMarkerOptions.SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueOrange));

            PickupMarker = googleMap.AddMarker(pickupMarkerOptions);
            destinationMarker = googleMap.AddMarker(destinationMarkerOptions);

            //Get Trip bounds
            double southlng = directionData.routes[0].bounds.southwest.lng;
            double southlat = directionData.routes[0].bounds.southwest.lat;
            double northlng = directionData.routes[0].bounds.northeast.lng;
            double northlat = directionData.routes[0].bounds.northeast.lat;

            LatLng southwest = new LatLng(southlat, southlng);
            LatLng northeast = new LatLng(northlat, northlng);

            LatLngBounds tripBounds = new LatLngBounds(southwest, northeast);

            googleMap.AnimateCamera(CameraUpdateFactory.NewLatLngBounds(tripBounds, 4));
            //googleMap.SetPadding(40, 70, 40, 70);


            

        }
    }
}