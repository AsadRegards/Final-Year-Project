﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
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
        ImageView Accounts, Notifications, logout,myads,payments,help;
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
            help = (ImageView)rootview.FindViewById(Resource.Id.imghelp);
            help.Click += Help_Click;
            
            return rootview;
        }

        private void Help_Click(object sender, EventArgs e)
        {
            var intent = new Intent(Application.Context, typeof(HelpActivity));
            intent.AddFlags(ActivityFlags.NewTask);
            Application.Context.StartActivity(intent);
        }

        private void Payments_Click(object sender, EventArgs e)
        {
            var intent = new Intent(Application.Context, typeof(ContributeFund));
            intent.AddFlags(ActivityFlags.NewTask);
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
            intent.AddFlags(ActivityFlags.NewTask);
            ISharedPreferences prefs = Application.Context.GetSharedPreferences("tokenfile", FileCreationMode.Private);
            ISharedPreferencesEditor editor = prefs.Edit();
            
            editor.Clear();
            editor.Commit();
            ISharedPreferences prefs0 = Application.Context.GetSharedPreferences("loginfile", FileCreationMode.Private);
            ISharedPreferencesEditor editor0 = prefs0.Edit();
            editor0.Clear();
            editor0.Commit();
            Activity.StartActivity(intent);



        }

        private void Notifications_Click(object sender, EventArgs e)
        {
            var intent = new Intent(Application.Context, typeof(notificationfragment));
            intent.AddFlags(ActivityFlags.NewTask);
            Application.Context.StartActivity(intent);
        }

        private void Accounts_Click(object sender, EventArgs e)
        {
            var intent = new Intent(Application.Context, typeof(AccountFragmentSettings));
            intent.AddFlags(ActivityFlags.NewTask);
            Application.Context.StartActivity(intent);
            

        }
    }
}