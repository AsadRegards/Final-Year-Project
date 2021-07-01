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
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace FinalProject_PU
{
    [Activity(Label = "NearbyUser")]
    public class NearbyUser : Activity
    {
        ImageView UserImage, IssueImage;
        TextView UserName, IssueStatement, VerificationText, NotVerifiedText;
        Switch VerificationSwitch;
        Model.User User;
        Model.Issue Issue;
        Bitmap userImgBitmap, issueImgBitmap;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your activity here
            SetContentView(Resource.Layout.nearbyUser);
            UserImage = FindViewById<ImageView>(Resource.Id.usericon);
            UserName = FindViewById<TextView>(Resource.Id.username);
            IssueImage = FindViewById<ImageView>(Resource.Id.imgIssue);
            IssueStatement = FindViewById<TextView>(Resource.Id.issuetxt);
            VerificationSwitch = FindViewById<Switch>(Resource.Id.switch1);
            VerificationText = FindViewById<TextView>(Resource.Id.verify3txt);
            NotVerifiedText = FindViewById<TextView>(Resource.Id.verify2txt);

            VerificationSwitch.CheckedChange += VerificationSwitch_CheckedChange;

            BackgroundWorker Worker = new BackgroundWorker();
            Worker.DoWork += Worker_DoWork;
            Worker.RunWorkerAsync();


            

            

            







        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            User = JsonConvert.DeserializeObject<Model.User>(Intent.GetStringExtra("UserObject"));
            Issue = JsonConvert.DeserializeObject<Model.Issue>(Intent.GetStringExtra("IssueObject"));

            //converting base64 userImage and issueImage to bitmap
            Byte[] userImgArray = Convert.FromBase64String(User.profile_pic);
            userImgBitmap = BitmapFactory.DecodeByteArray(userImgArray,0,userImgArray.Length);

            Byte[] issueImgArray = Convert.FromBase64String(Issue.IssueImage);
            issueImgBitmap = BitmapFactory.DecodeByteArray(issueImgArray,0, issueImgArray.Length);

            //check if this can be put into background worker block without cross thread exception!!
            IssueImage.SetImageBitmap(issueImgBitmap);
            UserImage.SetImageBitmap(userImgBitmap);
            char[] arr0 = User.name.ToCharArray();
            UserName.SetText(arr0, 0, arr0.Length);
            char[] arr1 = Issue.issueStatement.ToCharArray();
            IssueStatement.SetText(arr1, 0, arr1.Length);


            //check code ends here



        }

        private void VerificationSwitch_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if(VerificationSwitch.Checked)
            {
                VerificationText.Text = "Verified";
                VerificationText.SetTextColor(Android.Graphics.Color.Green);
                NotVerifiedText.Enabled = false;
            }
            else
            {
                VerificationText.Text = "Un-verified";
                VerificationText.SetTextColor(Android.Graphics.Color.DarkRed);
                NotVerifiedText.Enabled = true;
            }
        }
    }
}