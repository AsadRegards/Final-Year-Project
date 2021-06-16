using Android.App;
using Android.Content;
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
    [Activity(Label = "AdTitleandTextActivity")]
    public class AdTitleandTextActivity : Activity
    {
        EditText adTitle, adText;
        Button preview;
        string base64Image;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.adtextandtitle);
            adTitle = FindViewById<EditText>(Resource.Id.adtitle);
            adText = FindViewById<EditText>(Resource.Id.adtext);
            preview = FindViewById<Button>(Resource.Id.preview);
            preview.Click += Preview_Click;

        }

        private void Preview_Click(object sender, EventArgs e)
        {
            if (adText.Text != "" && adTitle.Text != "")
                //to create adpreview activity
                base64Image = JsonConvert.DeserializeObject<string>(Intent.GetStringExtra("baseimage"));
            string adtitle = JsonConvert.DeserializeObject<string>(Intent.GetStringExtra("adtitle"));
            string adtext = JsonConvert.DeserializeObject<string>(Intent.GetStringExtra("adtext"));

            Intent i = new Intent(this, typeof(AdPreviewActivity));
            i.PutExtra("baseimage", JsonConvert.SerializeObject(base64Image));
            i.PutExtra("adtitle", JsonConvert.SerializeObject(adtitle));
            i.PutExtra("adtext", JsonConvert.SerializeObject(adtext));
            StartActivity(i);



        }
    }
}