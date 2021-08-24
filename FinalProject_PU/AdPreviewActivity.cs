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
    [Activity(Label = "AdPreviewActivity",NoHistory =true)]
    public class AdPreviewActivity : Activity
    {
        ImageView AdImage;
        TextView adTitle, adText;
        AdsData data;
        Button submitButton;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.AdPreview);
            AdImage = FindViewById<ImageView>(Resource.Id.imgIssue);
            adTitle = FindViewById<TextView>(Resource.Id.adtitle);
            adText = FindViewById<TextView>(Resource.Id.adtext);
            submitButton = FindViewById<Button>(Resource.Id.adsubmit);
            submitButton.Click += SubmitButton_Click;

            string base64Image = JsonConvert.DeserializeObject<string>(Intent.GetStringExtra("baseimage"));
            string adtitle = JsonConvert.DeserializeObject<string>(Intent.GetStringExtra("adtitle"));
            string adtext = JsonConvert.DeserializeObject<string>(Intent.GetStringExtra("adtext"));
            int budget = JsonConvert.DeserializeObject<int>(Intent.GetStringExtra("adbudget"));
            string link= JsonConvert.DeserializeObject<string>(Intent.GetStringExtra("link"));

            data = new AdsData();
            data.Adsimage = base64Image;
            data.Adstitle = adtitle;
            data.Adstext = adtext;
            data.budget = budget;
            data.websitelink = link;

            var arr = Convert.FromBase64String(base64Image);
            Bitmap bitmap = BitmapFactory.DecodeByteArray(arr, 0, arr.Length);
            AdImage.SetImageBitmap(bitmap);
            adTitle.Text = data.Adstitle;
            adText.Text = data.Adstext;

           


        }

        private async void SubmitButton_Click(object sender, EventArgs e)
        {
            data.Status = "pending";
            data.User_id = Control.UserInfoHolder.User_id;
            data.Elapsed_Days = 0;
            data.Date = DateTime.Now;
            //call method to send data to Database
            if (await data.StoreAd(data))
            {
              
                Intent i = new Intent(this, typeof(AdsPayment));
                StartActivity(i);

            }
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
    }
}