using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FinalProject_PU.Control;
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
    [Activity(Label = "BrokenWires",NoHistory =true)]
    public class BrokenWires : Activity
    {
        static string selected;
        ImageView back_BrokenWires, next_BrokenWires, FullRoadElectricPol,HalfRoadElectricPol, 
                iconSettngs, iconMap, iconNotifications, iconFunds, iconHome;
        RadioButton BrokenWires_radiobtn1, BrokenWires_radiobtn2;
        
        CircleImageView circleImageView_BrokenWires;
        TextView BrokenWires_tvusername, BrokenWires_tv, BrokenWires_tev1;
        Typeface tf;
        User u;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.BrokenWires);
            //runtime py change hoga yhan sy 
           // circleImageView_BrokenWires = (CircleImageView)FindViewById(Resource.Id.circleImageView_BrokenWires);
            BrokenWires_tvusername = (TextView)FindViewById(Resource.Id.BrokenWires_tvusername);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            BrokenWires_tvusername.SetTypeface(tf, TypefaceStyle.Bold);

            //runtime py profile change krna or name change krna 
            //start
            circleImageView_BrokenWires = (CircleImageView)FindViewById(Resource.Id.cimgviewbw);
            char[] arr = UserInfoHolder.User_name.ToCharArray();
            byte[] arra = Convert.FromBase64String(Control.UserInfoHolder.Profile_pic);
            Android.Graphics.Bitmap bitmapp = BitmapFactory.DecodeByteArray(arra, 0, arra.Length);
            circleImageView_BrokenWires.SetImageBitmap(bitmapp);
            BrokenWires_tvusername.SetText(arr, 0, arr.Length);
            //end //runtime py profile change krna or name change krna 

            




            BrokenWires_tv = (TextView)FindViewById(Resource.Id.BrokenWires_tv);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            BrokenWires_tv.SetTypeface(tf, TypefaceStyle.Bold);
            
            BrokenWires_tev1 = (TextView)FindViewById(Resource.Id.tev1);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            BrokenWires_tev1.SetTypeface(tf, TypefaceStyle.Bold);

            
            next_BrokenWires = (ImageView)FindViewById(Resource.Id.next_BrokenWires);
            next_BrokenWires.Click += Next_BrokenWires_Click;
            FullRoadElectricPol = (ImageView)FindViewById(Resource.Id.FullRoadElectricPole);
            FullRoadElectricPol.Click += FullRoadElectricPol_Click;
            HalfRoadElectricPol = (ImageView)FindViewById(Resource.Id.HalfRoadElectricPole);
            HalfRoadElectricPol.Click += HalfRoadElectricPol_Click;
            BrokenWires_radiobtn1 = (RadioButton)FindViewById(Resource.Id.BrokenWire_Radiobtn1);
            BrokenWires_radiobtn1.Click += BrokenWires_radiobtn1_Click;
            BrokenWires_radiobtn2 = (RadioButton)FindViewById(Resource.Id.BrokenWire_Radiobtn2);
            BrokenWires_radiobtn2.Click += BrokenWires_radiobtn2_Click;


          

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

        private void BrokenWires_radiobtn2_Click(object sender, EventArgs e)
        {
            HalfRoadElectricPol.PerformClick();
        }

        private void BrokenWires_radiobtn1_Click(object sender, EventArgs e)
        {
            FullRoadElectricPol.PerformClick();
        }

        private void HalfRoadElectricPol_Click(object sender, EventArgs e)
        {
            selected = "halfblock";
            BrokenWires_radiobtn1.Checked = false;
            BrokenWires_radiobtn2.Checked = true;
        }

        private void FullRoadElectricPol_Click(object sender, EventArgs e)
        {
            selected = "fullblock";
            BrokenWires_radiobtn1.Checked = true;
            BrokenWires_radiobtn2.Checked = false;
        }

        private async void Next_BrokenWires_Click(object sender, EventArgs e)
        {
            if(selected!=null)
                await Task.Run(() => {
                var gar = new Model.Brokenpole();
                gar.roadCoverage = selected;
                gar.IssueImage = JsonConvert.DeserializeObject<string>(Intent.GetStringExtra("objtopass"));
                FinalProject_PU.Control.DataOper.PutData<BrokenWires2>(this, gar);


            });
            else
            {
                Xamarin.Essentials.MainThread.BeginInvokeOnMainThread(() => 
                {
                    Toast.MakeText(this, "Please select any option", ToastLength.Long).Show();
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
           
        }
    }
}