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
    public class NotificationFragmentSettings : AndroidX.Fragment.App.Fragment
    {
        Switch switch1, switch2, switch3;
        ImageView back;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override Android.Views.View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            var rootview = inflater.Inflate(Resource.Layout.NotificationSettings, container, false);
            switch1 = (Switch)rootview.FindViewById(Resource.Id.switch1);
            switch1.CheckedChange += Switch1_CheckedChange;
            switch2 = (Switch)rootview.FindViewById(Resource.Id.switch2);
            switch2.CheckedChange += Switch2_CheckedChange;
            switch3 = (Switch)rootview.FindViewById(Resource.Id.switch3);
            switch3.CheckedChange += Switch3_CheckedChange;
            back = (ImageView)rootview.FindViewById(Resource.Id.imgbackgo);
            back.Click += Back_Click;


            return rootview;
        }
        private void Switch3_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            //show all notification
            Toast.MakeText(Application.Context, ((e.IsChecked) ? "Yes" : "No"), ToastLength.Short).Show();
        }

        private void Switch2_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            //show notification with my locations only
            Toast.MakeText(Application.Context, ((e.IsChecked) ? "Yes" : "No"), ToastLength.Short).Show();
        }

        private void Switch1_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            //show notification with my contribution only
            Toast.MakeText(Application.Context, ((e.IsChecked) ? "Yes" : "No"), ToastLength.Short).Show();
        }
        private void Back_Click(object sender, EventArgs e)
        {
           
        }
    }
}