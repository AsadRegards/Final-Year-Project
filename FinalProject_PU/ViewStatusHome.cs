using Android.App;
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
    [Activity(Label = "ViewStatusHome")]
    public class ViewStatusHome : Activity
    {
        TextView working_started, resolved, estimated_amount, collected_amount, Contributor_name;
        CircleImageView userimage;
        ImageView issueImage, close;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ViewStatus_Funds);

            var IssueObj = JsonConvert.DeserializeObject<Helper.Data>(Intent.GetStringExtra("issueobj"));

            working_started = (TextView)FindViewById(Resource.Id.tv_WorkingStatus);
            resolved = (TextView)FindViewById(Resource.Id.tvResolvedStatus);
            estimated_amount = (TextView)FindViewById(Resource.Id.textEstimated);
            collected_amount = (TextView)FindViewById(Resource.Id.textCollected);
            Contributor_name = (TextView)FindViewById(Resource.Id.tvname);
            userimage = (CircleImageView)FindViewById(Resource.Id.imgProfile);
            issueImage = (ImageView)FindViewById(Resource.Id.imgissuev);
            close = (ImageView)FindViewById(Resource.Id.close);
            close.Click += Close_Click;

            if (IssueObj.isworkingstarted == 0)
            {
                working_started.Text = "No";
            }
            else if (IssueObj.isworkingstarted == 1)
            {
                working_started.Text = "Yes";
            }
            if (IssueObj.isResolved == 0)
            {
                resolved.Text = "No";
            }
            else if (IssueObj.isResolved == 1)
            {
                resolved.Text = "Yes";
            }

            estimated_amount.Text = IssueObj.estimatedCost.ToString();
            if (IssueObj.amountCollected > IssueObj.estimatedCost)
            {
                collected_amount.Text = IssueObj.estimatedCost.ToString();
            }
            else
            {
                collected_amount.Text = IssueObj.amountCollected.ToString();
            }

            if (IssueObj.estimatedCost != 0)
            {
                Task.Run(async () =>
                {
                    var user = await GetTopContributer(IssueObj.IssueId);
                    if (user != null)
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