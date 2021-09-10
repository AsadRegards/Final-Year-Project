using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FinalProject_PU.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinalProject_PU.Control;
using Lsjwzh.Widget.MaterialLoadingProgressBar;
using System.ComponentModel;
using Xamarin.Essentials;
using MohammedAlaa.GifLoading;
using Android.Util;
using Android.Icu.Util;

namespace FinalProject_PU
{
    [Activity(Label = "Register2Activity")]
    public class Register2Activity : Activity, Com.Wdullaer.Materialdatetimepicker.Date.DatePickerDialog.IOnDateSetListener
    {

        EditText phone;
        ImageView login, back, go;
        Typeface tf;
        TextView RegisterHeading, tvDOB, tvContact;
        User u;
        TextView dob;
        LoadingView loader, button_loader;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Register2);

            loader = FindViewById<LoadingView>(Resource.Id.loading_view);
            button_loader = FindViewById<LoadingView>(Resource.Id.loading_view_button);

            login = (ImageView)FindViewById(Resource.Id.imageView9);
            login.Click += Login_Click;

            RegisterHeading = (TextView)FindViewById(Resource.Id.textView1);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            RegisterHeading.SetTypeface(tf, TypefaceStyle.Bold);

            go = (ImageView)FindViewById(Resource.Id.imglogin);
            go.Click += Go_Click;

            phone = (EditText)FindViewById(Resource.Id.edtPhone);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            phone.SetTypeface(tf, TypefaceStyle.Normal);

            dob = (TextView)FindViewById(Resource.Id.edtDOB);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            dob.SetTypeface(tf, TypefaceStyle.Normal);
            dob.Click+=DateSelect_OnClick;

            tvDOB = (TextView)FindViewById(Resource.Id.textView2);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            tvDOB.SetTypeface(tf, TypefaceStyle.Normal);

            tvContact = (TextView)FindViewById(Resource.Id.textView3);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            tvContact.SetTypeface(tf, TypefaceStyle.Normal);

     




        }

        void DateSelect_OnClick(object sender, EventArgs eventArgs)
        {
        DatePickerFragment frag = DatePickerFragment.NewInstance(delegate(DateTime time)
         {
            dob.Text = time.ToLongDateString();
             dob.Enabled = false;
         });
        frag.Show(FragmentManager, DatePickerFragment.TAG);
        }




        private void Login_Click(object sender, EventArgs e)
        {
            var i = new Intent(this, typeof(Login));
            this.StartActivity(i);
        }

        private void Go_Click(object sender, EventArgs e)
        {
            var worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerAsync();

        }



        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (InputValidation.ValidateContact(phone.Text, this))
            {

                u = JsonConvert.DeserializeObject<User>(Intent.GetStringExtra("user"));
                u.contact_no = phone.Text;
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    loader.Visibility = Android.Views.ViewStates.Visible;
                    button_loader.Visibility = Android.Views.ViewStates.Visible;
                    login.Enabled = false;
                    dob.Enabled = false;
                    phone.Enabled = false;

                    var i = new Intent(this, typeof(Register3Activity));
                    i.PutExtra("usera", JsonConvert.SerializeObject(u));
                    this.StartActivity(i);
                    loader.Visibility = Android.Views.ViewStates.Gone;
                    button_loader.Visibility = Android.Views.ViewStates.Gone;
                    login.Enabled = true;
                    dob.Enabled = true;
                    phone.Enabled = true;
                });

            }
            else
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Toast.MakeText(this, "Please enter valid Contact Number", ToastLength.Long).Show();
                });
            }
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