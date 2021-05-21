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

namespace FinalProject_PU
{
    [Activity(Label = "MissingVehicle2")]
    public class MissingVehicle2 : Activity
    {

        ImageView back_MissingVehicle2, next_MissingVehicle2, 
              iconSettngs, iconMap, iconNotifications, iconFunds, iconHome;
        TextView head, info;
        CircleImageView circleImageView_MissingVehicle2;
        TextView MissingVehicle2_tvusername, MissingVehicle2_tv, MissingVehicle2_tev1, MissingVehicle2_Plateno, MissingVehicle2_MissingDate;
        Typeface tf;
        EditText MissingVehicle2_edtPlateno, MissingVehicle2_edtDate;
        User u;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.MissingVehicle2);
            circleImageView_MissingVehicle2 = (CircleImageView)FindViewById(Resource.Id.circleImageView_MissingVehicle2);
            MissingVehicle2_tvusername = (TextView)FindViewById(Resource.Id.MissingVehicle2_tvusername);
            Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            MissingVehicle2_tvusername.SetTypeface(tf, TypefaceStyle.Bold);
            MissingVehicle2_edtPlateno = FindViewById < EditText >( Resource.Id.MissingVehicle2_edtPlateno);
            Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            MissingVehicle2_edtPlateno.SetTypeface(tf, TypefaceStyle.Bold);
            MissingVehicle2_edtDate = FindViewById<EditText>(Resource.Id.MissingVehicle2_edtDate);
            Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            MissingVehicle2_edtDate.SetTypeface(tf, TypefaceStyle.Bold);

            //runtime py profile change krna or name change krna 
            //start
            // circleImageView5 = (CircleImageView)FindViewById(Resource.Id.circleImageView5);
          char[] arr = Control.UserInfoHolder.User_name.ToCharArray();
            MissingVehicle2_tvusername.SetText(arr, 0, arr.Length);
            byte[] arra = Convert.FromBase64String(Control.UserInfoHolder.Profile_pic);

            ImageView img2 = FindViewById<ImageView>(Resource.Id.circleImageView_MissingVehicle2);
            Android.Graphics.Bitmap bitmapp = BitmapFactory.DecodeByteArray(arra, 0, arra.Length);
            img2.SetImageBitmap(bitmapp);
            //end //runtime py profile change krna or name change krna 


            MissingVehicle2_tv = (TextView)FindViewById(Resource.Id.MissingVehicle2_tvinfo);
            Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            MissingVehicle2_tv.SetTypeface(tf, TypefaceStyle.Bold);

            MissingVehicle2_tev1 = (TextView)FindViewById(Resource.Id.tev1);
            Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            MissingVehicle2_tev1.SetTypeface(tf, TypefaceStyle.Bold);

            MissingVehicle2_Plateno = (TextView)FindViewById(Resource.Id.MissingVehicle2_Plateno);
            Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            MissingVehicle2_Plateno.SetTypeface(tf, TypefaceStyle.Bold);

            MissingVehicle2_MissingDate = (TextView)FindViewById(Resource.Id.MissingVehicle2_Date);
            Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            MissingVehicle2_MissingDate.SetTypeface(tf, TypefaceStyle.Bold);



        
            next_MissingVehicle2 = (ImageView)FindViewById(Resource.Id.next_MissingVehicle2);
            next_MissingVehicle2.Click += Next_MissingVehicle2_Click;

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
        private void Next_MissingVehicle2_Click(object sender, EventArgs e)
        {
            Model.Missingvehicle m = JsonConvert.DeserializeObject<Model.Missingvehicle>(Intent.GetStringExtra("objtopass"));
            m.plateNumber = MissingVehicle2_edtPlateno.Text;
            try
            {
                //m.foundDate = Convert.ToDateTime(MissingVehicle2_MissingDate.Text);
                //isko sahi krna hey 21 may 2021

            }

            catch
            {

            }

            FinalProject_PU.Control.DataOper.PutData<MissingVehicle3>(this, m);
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