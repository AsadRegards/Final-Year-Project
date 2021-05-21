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
    [Activity(Label = "Manhole1")]
    public class Manhole1 : Activity
    {
        static bool selected;
        
        ImageView Manhole1_back, Manhole1_next, Manhole1_radio1,
                   Manhole1_radio2,  iconSettngs, iconMap, iconNotifications, iconFunds, iconHome;
        RadioButton Manhole1_radiobtn1, Manhole1_radiobtn2;
        
        TextView Manhole1_tvusername, Manhole1_tv, tev1;
        Typeface tf;
        CircleImageView circleImageView_Manhole1;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Manhole1);

            Manhole1_radiobtn1 = (RadioButton)FindViewById(Resource.Id.Manhole1_radiobtn1);
            Manhole1_radiobtn1.Click += Manhole1_radiobtn1_Click;
            Manhole1_radiobtn2 = (RadioButton)FindViewById(Resource.Id.Manhole1_radiobtn2);
            Manhole1_radiobtn2.Click += Manhole1_radiobtn2_Click;
            Manhole1_radio1 = (ImageView)FindViewById(Resource.Id.iconIssue1);
            Manhole1_radio1.Click += Manhole1_radio1_Click;
            Manhole1_radio2 = (ImageView)FindViewById(Resource.Id.iconIssue2);
            Manhole1_radio2.Click += Manhole1_radio2_Click;

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
            circleImageView_Manhole1 = (CircleImageView)FindViewById(Resource.Id.circleImageView_Manhole1);
            //runtime py profile change krna or name change krna 
            //start
           
            char[] arr = Control.UserInfoHolder.User_name.ToCharArray();
            Manhole1_tvusername.SetText(arr, 0, arr.Length);
            byte[] arra = Convert.FromBase64String(Control.UserInfoHolder.Profile_pic);

            ImageView img2 = FindViewById<ImageView>(Resource.Id.circleImageView_Manhole1);
            Android.Graphics.Bitmap bitmapp = BitmapFactory.DecodeByteArray(arra, 0, arra.Length);
            img2.SetImageBitmap(bitmapp);
            //end

            Manhole1_next = (ImageView)FindViewById(Resource.Id.Manhole1_btnnext);
            Manhole1_next.Click += Manhole1_next_Click;

            Manhole1_tvusername = (TextView)FindViewById(Resource.Id.Manhole1_tvusername);
            Manhole1_tv = (TextView)FindViewById(Resource.Id.Manhole1_tv);
            tev1 = (TextView)FindViewById(Resource.Id.tev1);
        }

        private void Manhole1_radiobtn2_Click(object sender, EventArgs e)
        {
            Manhole1_radio2.PerformClick();
        }

        private void Manhole1_radiobtn1_Click(object sender, EventArgs e)
        {
            Manhole1_radio1.PerformClick();
        }

        private void Manhole1_radio2_Click(object sender, EventArgs e)
        {
            selected = false;
            Manhole1_radiobtn2.Checked = true;
            Manhole1_radiobtn1.Checked = false;
        }

        private void Manhole1_radio1_Click(object sender, EventArgs e)
        {
            selected = true;
            Manhole1_radiobtn2.Checked = false;
            Manhole1_radiobtn1.Checked = true;
        }

        private async void Manhole1_next_Click(object sender, EventArgs e)
        {
            if (Manhole1_radiobtn1.Checked || Manhole1_radiobtn2.Checked)
            {
                await Task.Run(() =>
                {
                    Model.Manhole man = new Model.Manhole();
                    man.Iswateroverflow = selected;
                    man.IssueImage = JsonConvert.DeserializeObject<string>(Intent.GetStringExtra("objtopass"));
                    FinalProject_PU.Control.DataOper.PutData<Manhole2>(this, man);
                });
            }
            else
            {
                Xamarin.Essentials.MainThread.BeginInvokeOnMainThread(() => 
                {
                    Toast.MakeText(this, "Please select any one from them", ToastLength.Long).Show();
                });
                
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