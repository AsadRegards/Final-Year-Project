﻿using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
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

namespace FinalProject_PU
{
    [Activity(Label = "FoundVehicle",NoHistory =true)]
    public class FoundVehicle : Activity, Com.Wdullaer.Materialdatetimepicker.Date.DatePickerDialog.IOnDateSetListener
    {
        static string selected;
        ImageView back_FoundVehicle, next_FoundVehicle;
        TextView head, info;
        CircleImageView circleImageView_FoundVehicle;
        TextView FoundVehicle_tvusername, FoundVehicle_tv, FoundVehicle_tev1, FoundVehicle_Plateno, FoundVehicle_Date;
        Typeface tf;
        User u;
        EditText FoundVehicle_edtPlateno, FoundVehicle_edtDate;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.FoundVehicle);


            circleImageView_FoundVehicle = (CircleImageView)FindViewById(Resource.Id.circleImageView_FoundVehicle);
            FoundVehicle_tvusername = (TextView)FindViewById(Resource.Id.FoundVehicle_tvusername);
            Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            FoundVehicle_tvusername.SetTypeface(tf, TypefaceStyle.Bold);
            FoundVehicle_edtPlateno = FindViewById<EditText>(Resource.Id.FoundVehicle_edtPlateno);
            Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            FoundVehicle_edtPlateno.SetTypeface(tf, TypefaceStyle.Bold);
            FoundVehicle_edtDate = FindViewById<EditText>(Resource.Id.FoundVehicle_edtDate);
            Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            FoundVehicle_edtDate.SetTypeface(tf, TypefaceStyle.Bold);
            FoundVehicle_edtDate.Click += DateSelect_OnClick;
            //runtime py profile change krna or name change krna 
            //start
            // circleImageView5 = (CircleImageView)FindViewById(Resource.Id.circleImageView5);
            char[] arr = Control.UserInfoHolder.User_name.ToCharArray();
            FoundVehicle_tvusername.SetText(arr, 0, arr.Length);
            byte[] arra = Convert.FromBase64String(Control.UserInfoHolder.Profile_pic);

            ImageView img2 = FindViewById<ImageView>(Resource.Id.circleImageView_FoundVehicle);
            Android.Graphics.Bitmap bitmapp = BitmapFactory.DecodeByteArray(arra, 0, arra.Length);
            img2.SetImageBitmap(bitmapp);
            //end //runtime py profile change krna or name change krna 


            FoundVehicle_tv = (TextView)FindViewById(Resource.Id.FoundVehicle_tv);
            Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            FoundVehicle_tv.SetTypeface(tf, TypefaceStyle.Bold);

            FoundVehicle_tev1 = (TextView)FindViewById(Resource.Id.tev1);
            Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            FoundVehicle_tev1.SetTypeface(tf, TypefaceStyle.Bold);

            FoundVehicle_Plateno = (TextView)FindViewById(Resource.Id.FoundVehicle_Plateno);
            Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            FoundVehicle_Plateno.SetTypeface(tf, TypefaceStyle.Bold);

            FoundVehicle_Date = (TextView)FindViewById(Resource.Id.FoundVehicle_Date_tv);
            Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            FoundVehicle_Date.SetTypeface(tf, TypefaceStyle.Bold);



            next_FoundVehicle = (ImageView)FindViewById(Resource.Id.next_FoundVehicle);
            next_FoundVehicle.Click += Next_FoundVehicle_Click;

          
        }
        private void Next_FoundVehicle_Click(object sender, EventArgs e)
        {
            if (FoundVehicle_edtPlateno.Text != "" && FoundVehicle_Date.Text!="")
            {


                Model.Missingvehicle m = JsonConvert.DeserializeObject<Model.Missingvehicle>(Intent.GetStringExtra("objtopass"));

                m.plateNumber = FoundVehicle_edtPlateno.Text;

                try
                {
                    //m.foundDate = Convert.ToDateTime(FoundVehicle_Date.Text);
                    //get m.founddate from date time picker
                }

                catch
                {

                }

                FinalProject_PU.Control.DataOper.PutData<FoundVehicle2>(this, m);
            }
            else
            {
                Toast.MakeText(this, "Please fill out the data!", ToastLength.Long).Show();
            }
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
        void DateSelect_OnClick(object sender, EventArgs eventArgs)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                FoundVehicle_edtDate.Text = time.ToLongDateString();
                FoundVehicle_edtDate.Enabled = false;
            });
            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }
        public void OnDateSet(Com.Wdullaer.Materialdatetimepicker.Date.DatePickerDialog p0, int year, int dayOfMonth, int monthOfYear)
        {
            Toast.MakeText(this, $"Your selected :{monthOfYear}/{dayOfMonth}/{year}", ToastLength.Long).Show();
        }

        public class DatePickerFragment : DialogFragment, DatePickerDialog.IOnDateSetListener
        {
            // TAG can be any string of your choice.
            public static readonly string TAG = "X:" + typeof(DatePickerFragment).Name.ToUpper();

            // Initialize this value to prevent NullReferenceExceptions.
            Action<DateTime> _dateSelectedHandler = delegate { };

            public static DatePickerFragment NewInstance(Action<DateTime> onDateSelected)
            {
                DatePickerFragment frag = new DatePickerFragment();
                frag._dateSelectedHandler = onDateSelected;
                return frag;
            }

            public override Dialog OnCreateDialog(Bundle savedInstanceState)
            {
                DateTime currently = DateTime.Now;
                DatePickerDialog dialog = new DatePickerDialog(Activity,
                                                               this,
                                                               currently.Year,
                                                               currently.Month - 1,
                                                               currently.Day);
                return dialog;
            }

            public void OnDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth)
            {
                // Note: monthOfYear is a value between 0 and 11, not 1 and 12!
                DateTime selectedDate = new DateTime(year, monthOfYear + 1, dayOfMonth);
                Log.Debug(TAG, selectedDate.ToLongDateString());
                _dateSelectedHandler(selectedDate);
            }
        }



    }
}