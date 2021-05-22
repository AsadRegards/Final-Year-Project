using Android.App;
using Android.Content;
using Android.Gms.Maps.Model;
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
    [Activity(Label = "ViewIssueFund")]
    public class ViewIssueFund : Activity
    {
        ImageView imgissuev, imgprofile, imgviewonmap, close;
        TextView tvtime, tvname, tvinfo;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ViewIssue_Fund);

            imgissuev = (ImageView)FindViewById(Resource.Id.imgissuev);
            imgprofile = (ImageView)FindViewById(Resource.Id.imgProfile);
            imgviewonmap = (ImageView)FindViewById(Resource.Id.imgvieonmap);
            tvtime = (TextView)FindViewById(Resource.Id.tvtime);
            tvname = (TextView)FindViewById(Resource.Id.tvname);
            tvinfo = (TextView)FindViewById(Resource.Id.tvinfo);
            close = (ImageView)FindViewById(Resource.Id.close);

            var IssueObj = JsonConvert.DeserializeObject<Model.OpenForFundsIssues>(Intent.GetStringExtra("issueObj"));

            byte[] arr1 = Convert.FromBase64String(IssueObj.IssueImage);
            Bitmap b1 = BitmapFactory.DecodeByteArray(arr1, 0, arr1.Length);
            imgissuev.SetImageBitmap(b1);

            byte[] arr2 = Convert.FromBase64String(IssueObj.userImage);
            Bitmap b2 = BitmapFactory.DecodeByteArray(arr2, 0, arr2.Length);
            imgprofile.SetImageBitmap(b2);

            tvtime.Text = IssueObj.issueDate.ToShortDateString();
            tvname.Text = IssueObj.userName;
            tvinfo.Text = IssueObj.issueStatement;

            close.Click += Close_Click;

            imgviewonmap.Click += (sender, EventArgs) => 
            {
                Imgviewonmap_Click(sender, EventArgs, IssueObj);
            };
            



        }

        private void Imgviewonmap_Click(object sender, EventArgs e, Model.OpenForFundsIssues issue)
        {
            Intent i = new Intent(Application.Context, typeof(ViewIssueOnMap));
            i.PutExtra("lattopass", JsonConvert.SerializeObject(issue.issueLatitude));
            i.PutExtra("longtopass", JsonConvert.SerializeObject(issue.issueLongitude));
            Application.Context.StartActivity(i);

        }

        private void Close_Click(object sender, EventArgs e)
        {
            base.OnBackPressed();
        }
    }
}