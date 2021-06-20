using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject_PU
{
    public class Settings : AndroidX.Fragment.App.Fragment
    {
        ImageView Accounts, Notifications, logout,myads,payments;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
            
            
        }

        public override Android.Views.View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            var rootview = inflater.Inflate(Resource.Layout.settings, container, false);
            Accounts = (ImageView)rootview.FindViewById(Resource.Id.imgviewaccounts);
            Accounts.Click += Accounts_Click;
            logout = (ImageView)rootview.FindViewById(Resource.Id.imgaccounticon);
            logout.Click += Logout_Click;
            myads = (ImageView)rootview.FindViewById(Resource.Id.imgviewmyads);
            myads.Click += Myads_Click;
            payments = (ImageView)rootview.FindViewById(Resource.Id.imgpayments);
            payments.Click += Payments_Click;
            
            return rootview;
        }

        private void Payments_Click(object sender, EventArgs e)
        {
            var intent = new Intent(Application.Context, typeof(ContributeFund));
            Application.Context.StartActivity(intent);

        }

        private void Myads_Click(object sender, EventArgs e)
        {
            var intent = new Intent(Application.Context, typeof(AdsFragmentSettings));
            intent.SetFlags(ActivityFlags.NewTask);
            Application.Context.StartActivity(intent);

        }

        private void Logout_Click(object sender, EventArgs e)
        {
            Control.UserInfoHolder.DisposeUserinfo();
            var intent = new Intent(Application.Context, typeof(Login));
            Activity.StartActivity(intent);
            Application.Context.DeleteSharedPreferences("tokenfile");
            this.Activity.Finish();


        }

        private void Notifications_Click(object sender, EventArgs e)
        {
            var intent = new Intent(Application.Context, typeof(notificationfragment));
            Application.Context.StartActivity(intent);
        }

        private void Accounts_Click(object sender, EventArgs e)
        {
            var intent = new Intent(Application.Context, typeof(AccountFragmentSettings));
            Application.Context.StartActivity(intent);
            

        }
    }
}