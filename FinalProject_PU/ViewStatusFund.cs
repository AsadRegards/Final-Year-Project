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

namespace FinalProject_PU
{
    [Activity(Label = "ViewStatusFund")]
    public class ViewStatusFund : Activity
    {
        TextView working_started, resolved, estimated_amount, collected_amount, Contributor_name;
        CircleImageView userimage;
        ImageView issueImage,close;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ViewStatus_Funds);

            working_started = (TextView)FindViewById(Resource.Id.tv_WorkingStatus);
            resolved = (TextView)FindViewById(Resource.Id.tvResolvedStatus);
            estimated_amount = (TextView)FindViewById(Resource.Id.textEstimated);
            collected_amount = (TextView)FindViewById(Resource.Id.textCollected);
            Contributor_name = (TextView)FindViewById(Resource.Id.tvname);
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

            Contributor_name.Text = IssueObj.userName.ToString();
            byte[] arr = Convert.FromBase64String(IssueObj.userImage);
            Bitmap b1 = BitmapFactory.DecodeByteArray(arr, 0, arr.Length);
            userimage.SetImageBitmap(b1);

            byte[] arr1 = Convert.FromBase64String(IssueObj.IssueImage);
            Bitmap b2 = BitmapFactory.DecodeByteArray(arr1, 0, arr1.Length);
            issueImage.SetImageBitmap(b2);
            close.Click += Close_Click;


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