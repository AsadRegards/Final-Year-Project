using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject_PU
{
    [Activity(Label = "AdPreviewActivity")]
    public class AdPreviewActivity : Activity
    {
        ImageView AdImage;
        TextView adTitle, adText;
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.AdItems);
            AdImage = FindViewById<ImageView>(Resource.Id.imgIssue);
            adTitle = FindViewById<TextView>(Resource.Id.adtitle);
            adText = FindViewById<TextView>(Resource.Id.adtext);

            string base64Image = JsonConvert.DeserializeObject<string>(Intent.GetStringExtra("baseimage"));
            string adtitle = JsonConvert.DeserializeObject<string>(Intent.GetStringExtra("adtitle"));
            string adtext = JsonConvert.DeserializeObject<string>(Intent.GetStringExtra("adtext"));

            AdsData data = new AdsData();
            data.Adsimage = base64Image;
            data.Adstitle = adtitle;
            data.Adstext = adtext;

            var arr = Convert.FromBase64String(base64Image);
            Bitmap bitmap = BitmapFactory.DecodeByteArray(arr, 0, arr.Length);
            AdImage.SetImageBitmap(bitmap);
            adTitle.Text = data.Adstitle;
            adText.Text = data.Adstext;

            //call method to send data to Database
            if(await data.StoreAd(data))
            {
                Toast.MakeText(this, "Ads saved succesfully", ToastLength.Long).Show();
                Intent i = new Intent(this, typeof(FragmentHomeActivity));
                StartActivity(i);

            }


        }
    }
}