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
    [Activity(Label = "UserFund")]
    public class UserFund : Activity
    {
        ImageView issueimg, btnContribute, goBack, close;
        TextView tvHead, tvEstimatedCost, tvCollectedAmount, textEstimatedCost, textCollectedAmount;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.UserFund);

            issueimg = (ImageView)FindViewById(Resource.Id.imgissuev);
            btnContribute = (ImageView)FindViewById(Resource.Id.imgContribute);
            goBack = (ImageView)FindViewById(Resource.Id.imggoback);
            close = (ImageView)FindViewById(Resource.Id.close);

            tvHead = (TextView)FindViewById(Resource.Id.tvinfo);
            //nabeel will make fancy fonts
            //tvEstimatedCost = (TextView)FindViewById(Resource.Id.tvEstimatedCost);
            //tvCollectedAmount = (TextView)FindViewById(Resource.Id.tvCollected);
            textEstimatedCost = (TextView)FindViewById(Resource.Id.tvEstimatedCostAmount);
            textCollectedAmount = (TextView)FindViewById(Resource.Id.tvCollectedAmount);

            var IssueObj = JsonConvert.DeserializeObject<Model.OpenForFundsIssues>(Intent.GetStringExtra("issueObj"));

            byte[] arr = Convert.FromBase64String(IssueObj.IssueImage);
            Bitmap b = BitmapFactory.DecodeByteArray(arr, 0, arr.Length);
            issueimg.SetImageBitmap(b);
            goBack.Click += GoBack_Click;
            close.Click += GoBack_Click;
            textEstimatedCost.Text = IssueObj.estimated_cost.ToString();
            textCollectedAmount.Text = IssueObj.amount_collected.ToString();
            btnContribute.Click += delegate
            {
                Intent i = new Intent(Application.Context, typeof(ContributeFund));
                i.PutExtra("issueObj", JsonConvert.SerializeObject(IssueObj));
                Application.Context.StartActivity(i);
            };







        }

       

        private void GoBack_Click(object sender, EventArgs e)
        {
            base.OnBackPressed();
        }
    }
}