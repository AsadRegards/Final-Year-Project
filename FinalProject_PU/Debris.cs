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
using System.Threading.Tasks;

namespace FinalProject_PU
{
    [Activity(Label = "Debris")]
    public class Debris : Activity
    {
        ImageView back_Debris, next_Debris, FullRoadDebris, HalfRoadDebris, SideRoadDebris,
                iconSettngs, iconMap, iconNotifications, iconFunds, iconHome;
        RadioButton Debris_radiobtn1, Debris_radiobtn2, Debris_radiobtn3;
        CircleImageView circleImageView_Debris;
        TextView Debris_tvusername, Debris_info, Debris_head;
        Typeface tf;
        User u;
        static string selected;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Debris);

            Debris_head = (TextView)FindViewById(Resource.Id.tev1);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            Debris_head.SetTypeface(tf, TypefaceStyle.Bold);

            Debris_info = (TextView)FindViewById(Resource.Id.Debris_tv);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            Debris_info.SetTypeface(tf, TypefaceStyle.Bold);

            Debris_tvusername = (TextView)FindViewById(Resource.Id.Debris_tvusername);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            Debris_tvusername.SetTypeface(tf, TypefaceStyle.Bold);

            circleImageView_Debris = (CircleImageView)FindViewById(Resource.Id.circleImageView_Debris);
            //runtime py change 
            //start
            char[] arr = Control.UserInfoHolder.User_name.ToCharArray();
        
            Debris_tvusername.SetText(arr, 0, arr.Length);
            byte[] arra = Convert.FromBase64String(Control.UserInfoHolder.Profile_pic);

            ImageView img2 = FindViewById<ImageView>(Resource.Id.circleImageView_Debris);
            Android.Graphics.Bitmap bitmapp = BitmapFactory.DecodeByteArray(arra, 0, arra.Length);
            img2.SetImageBitmap(bitmapp);
            //end

            next_Debris = (ImageView)FindViewById(Resource.Id.next_Debris);
            next_Debris.Click += Next_Debris_Click;
            FullRoadDebris = (ImageView)FindViewById(Resource.Id.Debris_FullRoad);
            FullRoadDebris.Click += FullRoadDebris_Click;
            HalfRoadDebris = (ImageView)FindViewById(Resource.Id.Debris_HalfRoad);
            HalfRoadDebris.Click += HalfRoadDebris_Click;
            SideRoadDebris = (ImageView)FindViewById(Resource.Id.Debris_SideRoad);
            SideRoadDebris.Click += SideRoadDebris_Click;
            Debris_radiobtn1 = (RadioButton)FindViewById(Resource.Id.Debris_Radiobtn1);
            Debris_radiobtn1.Click += Debris_radiobtn1_Click;
            Debris_radiobtn2 = (RadioButton)FindViewById(Resource.Id.Debris_Radiobtn2);
            Debris_radiobtn2.Click += Debris_radiobtn2_Click;
            Debris_radiobtn3 = (RadioButton)FindViewById(Resource.Id.Debris_Radiobtn3);
            Debris_radiobtn3.Click += Debris_radiobtn3_Click;
            iconSettngs = (ImageView)FindViewById(Resource.Id.iconSettings);
            iconSettngs.Click += IconSettngs_Click;
            iconMap = (ImageView)FindViewById(Resource.Id.iconMap);
            iconMap.Click += IconMap_Click;
            iconNotifications = (ImageView)FindViewById(Resource.Id.iconNotifications);
            iconNotifications.Click += IconNotifications_Click;
            iconFunds = (ImageView)FindViewById(Resource.Id.iconFunds);
            iconFunds.Click += IconFunds_Click;
            iconHome = (ImageView)FindViewById(Resource.Id.iconHome);
            iconHome.Click += IconHome_Click;

        }

        private void Debris_radiobtn3_Click(object sender, EventArgs e)
        {
            SideRoadDebris.PerformClick();
        }

        private void Debris_radiobtn2_Click(object sender, EventArgs e)
        {
            HalfRoadDebris.PerformClick();
        }

        private void Debris_radiobtn1_Click(object sender, EventArgs e)
        {
            FullRoadDebris.PerformClick();
        }

        private void SideRoadDebris_Click(object sender, EventArgs e)
        {
            selected = "smallarea";
            Debris_radiobtn1.Checked = false;
            Debris_radiobtn2.Checked = false;
            Debris_radiobtn3.Checked = true;
        }

        private void HalfRoadDebris_Click(object sender, EventArgs e)
        {
            selected = "halfblocked";
            Debris_radiobtn1.Checked = false;
            Debris_radiobtn2.Checked = true;
            Debris_radiobtn3.Checked = false;
        }

        private void FullRoadDebris_Click(object sender, EventArgs e)
        {
            selected = "fullblocked";
            Debris_radiobtn1.Checked = true;
            Debris_radiobtn2.Checked = false;
            Debris_radiobtn3.Checked = false;
        }

        private async void Next_Debris_Click(object sender, EventArgs e)
        {
            if (selected!="")
            {
                await Task.Run(() =>
                {
                    FinalProject_PU.Model.Debris deb = new Model.Debris();
                    deb.roadCoverage = selected;
                    FinalProject_PU.Control.DataOper.PutData<Debris2>(this, deb);
                });
            }
            else
            {
                Toast.MakeText(this, "Please select any one from them", ToastLength.Long).Show();
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