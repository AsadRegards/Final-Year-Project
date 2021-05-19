using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using FinalProject_PU.Control;
using FinalProject_PU.Model;
using MohammedAlaa.GifLoading;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using Xamarin.Essentials;

namespace FinalProject_PU
{
    [Activity(Label = "Register1Activity")]
    public class Register1Activity : Activity
    {

        User u;    
        EditText name, email;
        ImageView login;
        TextView RegisterHeading, name_tv, email_tv;
        Typeface tf;
        BackgroundWorker worker;
        LoadingView loader, button_loader;
        
          

        protected override void OnCreate(Bundle savedInstanceState)
        {
            
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Register1);
            ImageView go = (ImageView)FindViewById(Resource.Id.imglogin);
            go.Click += Go_Click;

            loader = FindViewById<LoadingView>(Resource.Id.loading_view);
            button_loader = FindViewById<LoadingView>(Resource.Id.loading_view_button);

            RegisterHeading = (TextView)FindViewById(Resource.Id.textView1);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            RegisterHeading.SetTypeface(tf, TypefaceStyle.Bold);

            login = (ImageView)FindViewById(Resource.Id.imageView9);
            login.Click += Login_Click;

            name = (EditText)FindViewById(Resource.Id.edtName);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            name.SetTypeface(tf, TypefaceStyle.Normal);

            email = (EditText)FindViewById(Resource.Id.edtEmail);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            email.SetTypeface(tf, TypefaceStyle.Normal);

            name_tv = (TextView)FindViewById(Resource.Id.textView2);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            name_tv.SetTypeface(tf, TypefaceStyle.Normal);

            email_tv = (TextView)FindViewById(Resource.Id.textView3);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            email_tv.SetTypeface(tf, TypefaceStyle.Normal);


        }

        private async void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
         
            if (Android.Util.Patterns.EmailAddress.Matcher(email.Text).Matches() == true)
            {
                MainThread.BeginInvokeOnMainThread(() => 
                {
                    loader.Visibility = Android.Views.ViewStates.Visible;
                    button_loader.Visibility = Android.Views.ViewStates.Visible;
                    name.Enabled = false;
                    email.Enabled = false;
                    login.Enabled = false;
                });
                if (await Account.EmailValidation(email.Text))
                {
                    if (InputValidation.ValidateUsername(name.Text, this))
                    {
                        
                        var i = new Intent(this, typeof(Register2Activity));
                        u = new FinalProject_PU.Model.User();
                        u.name = name.Text;
                        u.email_address = email.Text;
                        MainThread.BeginInvokeOnMainThread(() => 
                        {
                            i.PutExtra("user", JsonConvert.SerializeObject(u));
                            
                            loader.Visibility = Android.Views.ViewStates.Gone;
                            button_loader.Visibility = Android.Views.ViewStates.Gone;
                            name.Enabled = true;
                            email.Enabled = true;
                            login.Enabled = true;
                            this.StartActivity(i);
                        });
                       
                    }
                }
                else
                {
                    MainThread.BeginInvokeOnMainThread(() => 
                    {
                        Toast.MakeText(this, "Email address already exist, please enter another email", ToastLength.Long).Show();
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            loader.Visibility = Android.Views.ViewStates.Gone;
                            button_loader.Visibility = Android.Views.ViewStates.Gone;
                            name.Enabled = true;
                            email.Enabled = true;
                            login.Enabled = true;
                        });
                    });
                    
                }



            }
            else
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Toast.MakeText(this, "Invalid Email address entered", ToastLength.Long).Show();
                });
                
            }

        
        }

        private void Go_Click(object sender, EventArgs e)
        {
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerAsync();
        }

        private void Login_Click(object sender, EventArgs e)
        {
            var i = new Intent(this, typeof(Login));
            this.StartActivity(i);
        }
    }
}
