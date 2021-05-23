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
    [Activity(Label = "Garbage")]
    public class Garbage : Activity
    {
        static string selected;
        ImageView back_Garbage, next_Garbage, Garbage_FullRoad, Garbage_HalfRoad, Garbage_SideRoad,
               iconSettngs, iconMap, iconNotifications, iconFunds, iconHome;
        RadioButton Garbage_radiobtn1, Garbage_radiobtn2, Garbage_radiobtn3;
        
        CircleImageView circleImageView_Garbage;
        TextView Garbage_tvusername, Garbage_head, Garbage_info;
        Typeface tf;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Garbage);

            circleImageView_Garbage = (CircleImageView)FindViewById(Resource.Id.circleImageView_Garbage);

            Garbage_head = (TextView)FindViewById(Resource.Id.Garbage_tev1);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            Garbage_head.SetTypeface(tf, TypefaceStyle.Bold);

            Garbage_info = (TextView)FindViewById(Resource.Id.Garbage_tv);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            Garbage_info.SetTypeface(tf, TypefaceStyle.Bold);

            Garbage_tvusername = (TextView)FindViewById(Resource.Id.Garbage_tvusername);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            Garbage_tvusername.SetTypeface(tf, TypefaceStyle.Bold);

           
            next_Garbage = (ImageView)FindViewById(Resource.Id.next_Garbage);
            next_Garbage.Click += Next_Garbage_Click; 
            Garbage_FullRoad = (ImageView)FindViewById(Resource.Id.Garbage_FullRoad);
            Garbage_FullRoad.Click += Garbage_FullRoad_Click;
            Garbage_HalfRoad = (ImageView)FindViewById(Resource.Id.Garbage_HalfRoad);
            Garbage_HalfRoad.Click += Garbage_HalfRoad_Click;
            Garbage_SideRoad = (ImageView)FindViewById(Resource.Id.Garbage_SideRoad);
            Garbage_SideRoad.Click += Garbage_SideRoad_Click;
            Garbage_radiobtn1 = (RadioButton)FindViewById(Resource.Id.Garbage_Radiobtn1);
            Garbage_radiobtn1.Click += Garbage_radiobtn1_Click;
            Garbage_radiobtn2 = (RadioButton)FindViewById(Resource.Id.Garbage_Radiobtn2);
            Garbage_radiobtn2.Click += Garbage_radiobtn2_Click;
            Garbage_radiobtn3 = (RadioButton)FindViewById(Resource.Id.Garbage_Radiobtn3);
            Garbage_radiobtn3.Click += Garbage_radiobtn3_Click;
            //runtime py profile change krna or name change krna 
            //start

            char[] arr = Control.UserInfoHolder.User_name.ToCharArray();
            byte[] arra = Convert.FromBase64String(Control.UserInfoHolder.Profile_pic);
            Android.Graphics.Bitmap bitmapp = BitmapFactory.DecodeByteArray(arra, 0, arra.Length);
            circleImageView_Garbage.SetImageBitmap(bitmapp);
            Garbage_tvusername.SetText(arr, 0, arr.Length);
            //end //runtime py profile change krna or name change krna 
        }

        private void Garbage_radiobtn3_Click(object sender, EventArgs e)
        {
            Garbage_SideRoad.PerformClick();
        }

        private void Garbage_radiobtn2_Click(object sender, EventArgs e)
        {
            Garbage_HalfRoad.PerformClick();
        }

        private void Garbage_radiobtn1_Click(object sender, EventArgs e)
        {
            Garbage_FullRoad.PerformClick();
        }

        private void Garbage_SideRoad_Click(object sender, EventArgs e)
        {
            selected = "smallarea";
            Garbage_radiobtn3.Checked = true;
            Garbage_radiobtn2.Checked = false;
            Garbage_radiobtn1.Checked = false;
        }

        private void Garbage_HalfRoad_Click(object sender, EventArgs e)
        {
            selected = "halfroad";
            Garbage_radiobtn3.Checked = false;
            Garbage_radiobtn2.Checked = true;
            Garbage_radiobtn1.Checked = false;
        }

        private void Garbage_FullRoad_Click(object sender, EventArgs e)
        {
            selected = "fullroad";
            Garbage_radiobtn3.Checked = false;
            Garbage_radiobtn2.Checked = false;
            Garbage_radiobtn1.Checked = true;
        }

        private async void Next_Garbage_Click(object sender, EventArgs e)
        {
            if (selected != "")
            {

                await Task.Run(() =>
                {
                    var gar = new Model.Garbage();
                    gar.roadCoverage = selected;
                    gar.IssueImage = JsonConvert.DeserializeObject<string>(Intent.GetStringExtra("objtopass"));
                    FinalProject_PU.Control.DataOper.PutData<Garbage2>(this, gar);


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