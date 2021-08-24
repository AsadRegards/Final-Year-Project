using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using Android.Graphics;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace FinalProject_PU
{
    [Activity(Label = "ReportIssueHome",NoHistory =true)]
    public class ReportIssueHome : Activity
    {
        ImageView close, back, next, ReportText1, ReportText2, ReportText3, ReportText4, ReportText5;
        RadioButton Radio1, Radio2, Radio3, Radio4, Radio5;
        TextView textLocation, tvLoaction, textIssueType, tvIssueType, textheadi;
        Typeface tf;
        Helper.Data issueData;
        static string selected, locationName;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ReportIssue);

            close = (ImageView)FindViewById(Resource.Id.close);
            close.Click += Close_Click;
            next = (ImageView)FindViewById(Resource.Id.next);
            next.Click += Next_Click;
            back = (ImageView)FindViewById(Resource.Id.imggoback);
            back.Click += Back_Click;
            Radio1 = (RadioButton)FindViewById(Resource.Id.IssueReportRadio1);
            Radio1.Click += Radio1_Click;
            Radio2 = (RadioButton)FindViewById(Resource.Id.IssueReportRadio2);
            Radio2.Click += Radio2_Click;
            Radio3 = (RadioButton)FindViewById(Resource.Id.IssueReportRadio3);
            Radio3.Click += Radio3_Click;
            Radio4 = (RadioButton)FindViewById(Resource.Id.IssueReportRadio4);
            Radio4.Click += Radio4_Click;
            Radio5 = (RadioButton)FindViewById(Resource.Id.IssueReportRadio5);
            Radio5.Click += Radio5_Click;
            ReportText1 = (ImageView)FindViewById(Resource.Id.IssueReport1);
            ReportText1.Click += ReportText1_Click;
            ReportText2 = (ImageView)FindViewById(Resource.Id.IssueReport2);
            ReportText2.Click += ReportText2_Click;
            ReportText3 = (ImageView)FindViewById(Resource.Id.IssueReport3);
            ReportText3.Click += ReportText3_Click;
            ReportText4 = (ImageView)FindViewById(Resource.Id.IssueReport4);
            ReportText4.Click += ReportText4_Click;
            ReportText5 = (ImageView)FindViewById(Resource.Id.IssueReport5);
            ReportText5.Click += ReportText5_Click;
            textLocation = (TextView)FindViewById(Resource.Id.text_location);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            textLocation.SetTypeface(tf, TypefaceStyle.Bold);
            tvLoaction = (TextView)FindViewById(Resource.Id.tvLocation);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            tvLoaction.SetTypeface(tf, TypefaceStyle.Bold);
            textIssueType = (TextView)FindViewById(Resource.Id.txtIssueType);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            textIssueType.SetTypeface(tf, TypefaceStyle.Bold);
            tvIssueType = (TextView)FindViewById(Resource.Id.tvIssueType);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            tvIssueType.SetTypeface(tf, TypefaceStyle.Bold);
            textheadi= (TextView)FindViewById(Resource.Id.txtHeadi);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            textheadi.SetTypeface(tf, TypefaceStyle.Bold);

            issueData = JsonConvert.DeserializeObject<Helper.Data>(Intent.GetStringExtra("issueobj"));
           

            tvIssueType.Text = issueData.IssueType;
            tvLoaction.Text = issueData.location_name;
            


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
        private void ReportText5_Click(object sender, EventArgs e)
        {
            selected = "Issue Location is not correct";
            Radio5.Checked = true;
            Radio4.Checked = false;
            Radio3.Checked = false;
            Radio2.Checked = false;
            Radio1.Checked = false;
        }

        private void ReportText4_Click(object sender, EventArgs e)
        {
            selected = "Issue Information is not correct";
            Radio5.Checked = false;
            Radio4.Checked = true;
            Radio3.Checked = false;
            Radio2.Checked = false;
            Radio1.Checked = false;

        }

        private void ReportText3_Click(object sender, EventArgs e)
        {
            selected = "Issue is not solvable";
            Radio5.Checked = false;
            Radio4.Checked = false;
            Radio3.Checked =true;
            Radio2.Checked = false;
            Radio1.Checked = false;
        }

        private void ReportText2_Click(object sender, EventArgs e)
        {
            selected = "Issue has been resolved";
            Radio5.Checked = false;
            Radio4.Checked = false;
            Radio3.Checked = false;
            Radio2.Checked = true;
            Radio1.Checked = false;
        }

        private void ReportText1_Click(object sender, EventArgs e)
        {
            selected = "Isse not present";
            Radio5.Checked = false;
            Radio4.Checked = false;
            Radio3.Checked = false;
            Radio2.Checked = false;
            Radio1.Checked = true;
        }

        private void Radio5_Click(object sender, EventArgs e)
        {
            Radio5.PerformClick();
        }

        private void Radio4_Click(object sender, EventArgs e)
        {
            Radio4.PerformClick();
        }

        private void Radio3_Click(object sender, EventArgs e)
        {
            Radio3.PerformClick();
        }

        private void Radio2_Click(object sender, EventArgs e)
        {
            Radio2.PerformClick();
        }

        private void Radio1_Click(object sender, EventArgs e)
        {
            Radio1.PerformClick();
        }

        private void Back_Click(object sender, EventArgs e)
        {
            base.OnBackPressed();
        }

        private async void Next_Click(object sender, EventArgs e)
        {
            if (selected != null)
            {
                
                Model.Report repo = new Model.Report();
                repo.issue_id = issueData.IssueId;
                repo.user_id = Control.UserInfoHolder.User_id;
                repo.status = "Not Viewed";
                repo.report_text = selected;
                bool result=await new Control.IssueController().reportanissue(repo);
                if(result)
                {
                    Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                    AlertDialog alert = dialog.Create();
                    alert.SetMessage("Your report is submitted successfully");
                    alert.SetButton("OK", (c, ev) =>
                    {
                        alert.Dismiss();
                        base.OnBackPressed();
                    });

                    alert.Show();
                    
                }
            }
            else
            {
                Toast.MakeText(this, "Please select any option", ToastLength.Long).Show();
            }

          
            
        }

        private void Close_Click(object sender, EventArgs e)
        {
            base.OnBackPressed();
        }
    }
}