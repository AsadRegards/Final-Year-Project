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
            
            
            return rootview;
        }

        

        private void Myads_Click(object sender, EventArgs e)
        {
            Activity.SupportFragmentManager.BeginTransaction()
                                            .Replace(Resource.Id.fragment_main, new AdsFragmentSettings())
                                            .Commit();
        }

        private void Logout_Click(object sender, EventArgs e)
        {
            //Logout work will be done here
            //app will be directed to login screen
            //shared preferences will be cleared
           //userinfoholder will be cleared
        }

        private void Notifications_Click(object sender, EventArgs e)
        {
            Activity.SupportFragmentManager.BeginTransaction()
                                            .Replace(Resource.Id.fragment_main, new NotificationFragmentSettings())
                                            .Commit();
        }

        private void Accounts_Click(object sender, EventArgs e)
        {
            //click here to call accountsfragmentsettings
            Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.fragment_main, new AccountFragmentSettings()).AddToBackStack("settings").Commit();

        }
    }
}