using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FinalProject_PU.Model;
using Newtonsoft.Json;
using Refractored.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinalProject_PU.Control;


namespace FinalProject_PU
{
    [Activity(Label = "createissue2")]
    public class createissue2 : Activity
    {
        static string selected;
        
        ImageView back_imga1;
        ImageView next_imga2;
        ImageView issueImg, iconSettngs, iconMap, iconNotifications, iconFunds, iconHome,
                 Potholeimgbtn1, Manholeimgbtn2, Debrisimgbtn3, Garbageimgbtn4, BrokenWiresimgbtn5, Plantingimgbtn6, MissingVehicleimgbtn7,
                Rainwaterimgbtn8, imgviewissue;
        RadioButton radiobtn1, radiobtn2, radiobtn3, radiobtn4, radiobtn5, radiobtn6, radiobtn7, radiobtn8;
        TextView tvusername, infoprob;
        Typeface tf;
        CircleImageView circleimageview2;
        static string base64image;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.createissue2);
            issueImg = FindViewById<ImageView>(Resource.Id.imgbtn9);
            //to change with pp on runtime
            circleimageview2 = FindViewById<CircleImageView>(Resource.Id.circleImageView2);

            //to change on runtime with user name
            tvusername = (TextView)FindViewById(Resource.Id.tvusername);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            tvusername.SetTypeface(tf, TypefaceStyle.Bold);

            base64image = JsonConvert.DeserializeObject<string>(Intent.GetStringExtra("issueimage"));
            byte[] imageArray = Convert.FromBase64String(base64image);
            Android.Graphics.Bitmap bitmap = BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);
            issueImg.SetImageBitmap(bitmap);
        
            //runtime py profile change krna or name change krna 
            //start
            char[] arr = UserInfoHolder.User_name.ToCharArray();
            byte[] arra = Convert.FromBase64String(Control.UserInfoHolder.Profile_pic);
            Android.Graphics.Bitmap bitmapp = BitmapFactory.DecodeByteArray(arra, 0, arra.Length);
            circleimageview2.SetImageBitmap(bitmapp);
            tvusername.SetText(arr, 0, arr.Length);
            //end //runtime py profile change krna or name change krna 
            next_imga2 = (ImageView)FindViewById(Resource.Id.imganext);
            next_imga2.Click += Next_imga2_Click;
          

            infoprob = (TextView)FindViewById(Resource.Id.tvinfoproblem);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            infoprob.SetTypeface(tf, TypefaceStyle.Bold);

            Potholeimgbtn1 = (ImageView)FindViewById(Resource.Id.imgbtn1);
            Potholeimgbtn1.Click += Potholeimgbtn1_Click;
            Manholeimgbtn2 = (ImageView)FindViewById(Resource.Id.imgbtn2);
            Manholeimgbtn2.Click += Manholeimgbtn2_Click;
            Debrisimgbtn3 = (ImageView)FindViewById(Resource.Id.imgbtn3);
            Debrisimgbtn3.Click += Debrisimgbtn3_Click;
            Garbageimgbtn4 = (ImageView)FindViewById(Resource.Id.imgbtn4);
            Garbageimgbtn4.Click += Garbageimgbtn4_Click;
            BrokenWiresimgbtn5 = (ImageView)FindViewById(Resource.Id.imgbtn5);
            BrokenWiresimgbtn5.Click += BrokenWiresimgbtn5_Click;
            Plantingimgbtn6 = (ImageView)FindViewById(Resource.Id.imgbtn6);
            Plantingimgbtn6.Click += Plantingimgbtn6_Click;
            MissingVehicleimgbtn7 = (ImageView)FindViewById(Resource.Id.imgbtn7);
            MissingVehicleimgbtn7.Click += MissingVehicleimgbtn7_Click;
            Rainwaterimgbtn8 = (ImageView)FindViewById(Resource.Id.imgbtn8);
            Rainwaterimgbtn8.Click += Rainwaterimgbtn8_Click;
            imgviewissue = (ImageView)FindViewById(Resource.Id.imgbtn9);

            radiobtn1 = (RadioButton)FindViewById(Resource.Id.radioButton1);
            radiobtn1.Click += Radiobtn1_Click;
            radiobtn2 = (RadioButton)FindViewById(Resource.Id.radioButton2);
            radiobtn2.Click += Radiobtn2_Click;
            radiobtn3 = (RadioButton)FindViewById(Resource.Id.radioButton3);
            radiobtn3.Click += Radiobtn3_Click;
            radiobtn4 = (RadioButton)FindViewById(Resource.Id.radioButton4);
            radiobtn4.Click += Radiobtn4_Click;
            radiobtn5 = (RadioButton)FindViewById(Resource.Id.radioButton5);
            radiobtn5.Click += Radiobtn5_Click;
            radiobtn6 = (RadioButton)FindViewById(Resource.Id.radioButton6);
            radiobtn6.Click += Radiobtn6_Click;
            radiobtn7 = (RadioButton)FindViewById(Resource.Id.radioButton7);
            radiobtn7.Click += Radiobtn7_Click;
            radiobtn8 = (RadioButton)FindViewById(Resource.Id.radioButton8);
            radiobtn8.Click += Radiobtn8_Click;

        }

        private void Radiobtn8_Click(object sender, EventArgs e)
        {
            Rainwaterimgbtn8.PerformClick();
        }

        private void Radiobtn7_Click(object sender, EventArgs e)
        {
            MissingVehicleimgbtn7.PerformClick();
        }

        private void Radiobtn6_Click(object sender, EventArgs e)
        {
            Plantingimgbtn6.PerformClick();
        }

        private void Radiobtn5_Click(object sender, EventArgs e)
        {
            BrokenWiresimgbtn5.PerformClick();
        }

        private void Radiobtn4_Click(object sender, EventArgs e)
        {
            Garbageimgbtn4.PerformClick();
        }

        private void Radiobtn3_Click(object sender, EventArgs e)
        {
            Debrisimgbtn3.PerformClick();
        }

        private void Radiobtn2_Click(object sender, EventArgs e)
        {
            Manholeimgbtn2.PerformClick();
        }

        private void Radiobtn1_Click(object sender, EventArgs e)
        {
            Potholeimgbtn1.PerformClick();
        }
        private void Rainwaterimgbtn8_Click(object sender, EventArgs e)
        {
            selected = "rainwater";
            radiobtn8.Checked = true;
            radiobtn7.Checked = false;
            radiobtn6.Checked = false;
            radiobtn5.Checked = false;
            radiobtn4.Checked = false;
            radiobtn3.Checked = false;
            radiobtn2.Checked = false;
            radiobtn1.Checked = false;
        }

        private void MissingVehicleimgbtn7_Click(object sender, EventArgs e)
        {
            selected = "missingvehicle";
            radiobtn7.Checked = true;
            radiobtn8.Checked = false;
            radiobtn6.Checked = false;
            radiobtn5.Checked = false;
            radiobtn4.Checked = false;
            radiobtn3.Checked = false;
            radiobtn2.Checked = false;
            radiobtn1.Checked = false;
        }

        private void Plantingimgbtn6_Click(object sender, EventArgs e)
        {
            selected = "planting";
            radiobtn6.Checked = true;
            radiobtn7.Checked = false;
            radiobtn8.Checked = false;
            radiobtn5.Checked = false;
            radiobtn4.Checked = false;
            radiobtn3.Checked = false;
            radiobtn2.Checked = false;
            radiobtn1.Checked = false;
        }

        private void BrokenWiresimgbtn5_Click(object sender, EventArgs e)
        {
            selected = "brokenpole";
            radiobtn5.Checked = true;
            radiobtn6.Checked = false;
            radiobtn7.Checked = false;
            radiobtn8.Checked = false;

            radiobtn4.Checked = false;
            radiobtn3.Checked = false;
            radiobtn2.Checked = false;
            radiobtn1.Checked = false;


        }

        private void Garbageimgbtn4_Click(object sender, EventArgs e)
        {
            selected = "garbage";
            radiobtn4.Checked = true;
            radiobtn6.Checked = false;
            radiobtn7.Checked = false;
            radiobtn8.Checked = false;
            radiobtn5.Checked = false;

            radiobtn3.Checked = false;
            radiobtn2.Checked = false;
            radiobtn1.Checked = false;

        }

        private void Debrisimgbtn3_Click(object sender, EventArgs e)
        {
            selected = "debris";
            radiobtn3.Checked = true;
            radiobtn6.Checked = false;
            radiobtn7.Checked = false;
            radiobtn8.Checked = false;
            radiobtn5.Checked = false;
            radiobtn4.Checked = false;

            radiobtn2.Checked = false;
            radiobtn1.Checked = false;

        }

        private void Manholeimgbtn2_Click(object sender, EventArgs e)
        {
            selected = "manhole";
            radiobtn2.Checked = true;
            radiobtn6.Checked = false;
            radiobtn7.Checked = false;
            radiobtn8.Checked = false;
            radiobtn5.Checked = false;
            radiobtn4.Checked = false;
            radiobtn3.Checked = false;

            radiobtn1.Checked = false;

        }

        private void Potholeimgbtn1_Click(object sender, EventArgs e)
        {
            selected = "pothole";
                radiobtn1.Checked = true;
                radiobtn6.Checked = false;
                radiobtn7.Checked = false;
                radiobtn8.Checked = false;
                radiobtn5.Checked = false;
                radiobtn4.Checked = false;
                radiobtn3.Checked = false;
                radiobtn2.Checked = false;
            
        }

        private async void Next_imga2_Click(object sender, EventArgs e)
        {
            if(await new ImageModeration().IsValidImage(base64image))
            {
                IssueOper.ProceedToIssueActivity(selected, this, base64image);
            }
            else
            {
                Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                AlertDialog alert1 = dialog.Create();
                alert1.SetButton("OK", (c, ev) =>
                {
                    alert1.Dismiss();
                });
                alert1.SetTitle("Wrong Image");
                alert1.SetMessage("The image you are trying to post does not relate to this issue. Please upload correct picture");
                alert1.Show();
            }
            
        }

      

        private void IconHome_Click(object sender, EventArgs e)
        {
            var i = new Intent(this, typeof(HomeActivity));
            this.StartActivity(i);
        }

        private void IconFunds_Click(object sender, EventArgs e)
        {
            var i = new Intent(this, typeof(FundsActivity));
            this.StartActivity(i);
        }

        private void IconNotifications_Click(object sender, EventArgs e)
        {
            var i = new Intent(this, typeof(NotificationsActivity));
            this.StartActivity(i);
        }

        private void IconMap_Click(object sender, EventArgs e)
        {
            var i = new Intent(this, typeof(MapActivity));
            this.StartActivity(i);
        }

        private void IconSettngs_Click(object sender, EventArgs e)
        {
            var i = new Intent(this, typeof(SettingsActivity));
            this.StartActivity(i);
        }

    }
}