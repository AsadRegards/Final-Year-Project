﻿using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Refractored.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace FinalProject_PU
{
    [Activity(Label = "ViewStatusFund",NoHistory =true)]
    public class ViewStatusFund : Activity
    {
        TextView working_started, resolved, estimated_amount, collected_amount, Contributor_name;
        CircleImageView userimage;
        ImageView issueImage,close;
        Typeface tf;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ViewStatus_Funds);

            working_started = (TextView)FindViewById(Resource.Id.tv_WorkingStatus);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            working_started.SetTypeface(tf, TypefaceStyle.Bold);

            resolved = (TextView)FindViewById(Resource.Id.tvResolvedStatus);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            resolved.SetTypeface(tf, TypefaceStyle.Bold);

            estimated_amount = (TextView)FindViewById(Resource.Id.textEstimated);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            estimated_amount.SetTypeface(tf, TypefaceStyle.Bold);

            collected_amount = (TextView)FindViewById(Resource.Id.textCollected);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            collected_amount.SetTypeface(tf, TypefaceStyle.Bold);

            Contributor_name = (TextView)FindViewById(Resource.Id.tvname);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            Contributor_name.SetTypeface(tf, TypefaceStyle.Bold);

            userimage = (CircleImageView)FindViewById(Resource.Id.imgProfile);
            issueImage = (ImageView)FindViewById(Resource.Id.imgissuev);
            close = (ImageView)FindViewById(Resource.Id.close);

            var IssueObj = JsonConvert.DeserializeObject<Model.OpenForFundsIssues>(Intent.GetStringExtra("issueObj"));
            if(IssueObj.isworkingstarted==0)
            {
                working_started.Text = "No";
            }
            else if(IssueObj.isworkingstarted==1)
            {
                working_started.Text = "Yes";
            }
            if(IssueObj.isResolved==0)
            {
                resolved.Text = "No";
            }
            else if(IssueObj.isResolved==1)
            {
                resolved.Text = "Yes";
            }
            
            estimated_amount.Text = IssueObj.estimated_cost.ToString();
            if(IssueObj.amount_collected>IssueObj.estimated_cost)
            {
                collected_amount.Text = IssueObj.estimated_cost.ToString();
            }
            else
            {
                collected_amount.Text = IssueObj.amount_collected.ToString();
            }

            if(IssueObj.amount_collected!=0)
            {
                Task.Run(async() =>
                {
                    var user = await GetTopContributer(IssueObj.issue_id);
                    if(user!=null)
                    {
                        
                        byte[] arr = Convert.FromBase64String(user.profile_pic);
                        Bitmap b1 = BitmapFactory.DecodeByteArray(arr, 0, arr.Length);
                        MainThread.BeginInvokeOnMainThread(() => 
                        {
                            userimage.SetImageBitmap(b1);
                            Contributor_name.Text = user.name;
                        });
                        

                    }
                    else
                    {
                        userimage.Visibility = Android.Views.ViewStates.Gone;
                        Contributor_name.Text = "No Contributer yet";


                    }
                });
               
            }
            byte[] arr1 = Convert.FromBase64String(IssueObj.IssueImage);
            Bitmap b2 = BitmapFactory.DecodeByteArray(arr1, 0, arr1.Length);
            issueImage.SetImageBitmap(b2);
            close.Click += Close_Click;
           


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
        private void Close_Click(object sender, EventArgs e)
        {
            base.OnBackPressed();
        }

        public async Task<Model.User> GetTopContributer(int Issueid)
        {
            Control.Funds f = new Control.Funds();
            var User = await f.getTopContributer(Issueid);
            return User;

        }

        
    }
}