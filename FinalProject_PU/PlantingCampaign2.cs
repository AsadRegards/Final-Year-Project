﻿using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Text.Format;
using Android.Util;
using Android.Views;
using Android.Widget;
using FinalProject_PU.Model;
using Newtonsoft.Json;
using Refractored.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;

namespace FinalProject_PU
{
    [Activity(Label = "PlantingCampaign2",NoHistory =true)]
    public class PlantingCampaign2 : Activity
    {
        static string schedule;
        static DateTime timeToWater;
        TextView timeDisplay;
        ImageView issueImg,  iconback, iconnext, close;
        TextView tvusername, tvinfoproblem, tev1;
        CircleImageView circleImageViewplanting;
        Spinner spinner_PlantingCampign2;
        Typeface tf;
        User u;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.PlantingCampaign2);

           
            iconnext = (ImageView)FindViewById(Resource.Id.imga2);
            iconnext.Click += Iconnext_Click;
            iconback = (ImageView)FindViewById(Resource.Id.backbtn6);
            iconback.Click += Iconback_Click;
            close = (ImageView)FindViewById(Resource.Id.close);
            close.Click += Close_Click;
            tvusername = (TextView)FindViewById(Resource.Id.tvusername);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            tvusername.SetTypeface(tf, TypefaceStyle.Bold);

            //runtime py profile change krna or name change krna 
            //start
            circleImageViewplanting = (CircleImageView)FindViewById(Resource.Id.circleImageView2);
          char[] arr = Control.UserInfoHolder.User_name.ToCharArray();
            tvusername.SetText(arr, 0, arr.Length);
            byte[] arra = Convert.FromBase64String(Control.UserInfoHolder.Profile_pic);

          
            Android.Graphics.Bitmap bitmapp = BitmapFactory.DecodeByteArray(arra, 0, arra.Length);
            circleImageViewplanting.SetImageBitmap(bitmapp);
            //end

            tev1 = (TextView)FindViewById(Resource.Id.tev1);
            Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            tev1.SetTypeface(tf, TypefaceStyle.Bold);

            tvinfoproblem = (TextView)FindViewById(Resource.Id.tvinfoproblem);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            tvinfoproblem.SetTypeface(tf, TypefaceStyle.Bold);

            spinner_PlantingCampign2 = FindViewById<Spinner>(Resource.Id.spinner_PlantingCampign2);
            spinner_PlantingCampign2.Prompt = "Choose Days:";
            spinner_PlantingCampign2.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.spinner1_PlantingCampign2, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner_PlantingCampign2.Adapter = adapter;

            timeDisplay = FindViewById<TextView>(Resource.Id.time_display);
            timeDisplay.Click += TimeSelectOnClick;
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.OnBackPressed();
        }

        private void Iconback_Click(object sender, EventArgs e)
        {
            base.OnBackPressed();
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
        private async void Iconnext_Click(object sender, EventArgs e)
        {
            
            var p = JsonConvert.DeserializeObject<Planting>(Intent.GetStringExtra("objtopass"));
            p.Schedule = schedule;
            p.time = timeToWater;
            Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
            AlertDialog alert = dialog.Create();

            alert.SetMessage("Please wait while your issue is being posted ...");
            alert.Show();
            if(!await Control.IssueController.PostIssue<Planting>(p, this))
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Toast.MakeText(this, "Unfortunately! Your Issue cannot be posted at this time", ToastLength.Long).Show();
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
        void TimeSelectOnClick(object sender, EventArgs eventArgs)
        {
            TimePickerFragment frag = TimePickerFragment.NewInstance(
                delegate (DateTime time)
                {
                    timeDisplay.Text = time.ToShortTimeString();
                    timeToWater = time;
                });

            frag.Show(FragmentManager, TimePickerFragment.TAG);
            
        }
        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var spinner = sender as Spinner;
            Toast.MakeText(this, "You will water :" + spinner.GetItemAtPosition(e.Position), ToastLength.Long).Show();
            schedule = spinner.GetItemAtPosition(e.Position).ToString();
        }
    }
    public class TimePickerFragment : DialogFragment, TimePickerDialog.IOnTimeSetListener
    {
        public static readonly string TAG = "MyTimePickerFragment";
        Action<DateTime> timeSelectedHandler = delegate { };

        public static TimePickerFragment NewInstance(Action<DateTime> onTimeSelected)
        {
            TimePickerFragment frag = new TimePickerFragment();
            frag.timeSelectedHandler = onTimeSelected;
            return frag;
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            DateTime currentTime = DateTime.Now;
            bool is24HourFormat = DateFormat.Is24HourFormat(Activity);
            TimePickerDialog dialog = new TimePickerDialog
                (Activity, this, currentTime.Hour, currentTime.Minute, is24HourFormat);
            return dialog;
        }

        public void OnTimeSet(TimePicker view, int hourOfDay, int minute)
        {
            DateTime currentTime = DateTime.Now;
            DateTime selectedTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, hourOfDay, minute, 0);
            Log.Debug(TAG, selectedTime.ToLongTimeString());
            timeSelectedHandler(selectedTime);
        }
    }
}