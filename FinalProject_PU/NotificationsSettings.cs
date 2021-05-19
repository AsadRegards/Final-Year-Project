using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject_PU

{
    [Activity(Label = "NotificationsSettings")]
    public class NotificationsSettings : Activity
    {
        Switch switch1, switch2, switch3;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.NotificationSettings);
            // Create your application here
            switch1 = (Switch)FindViewById(Resource.Id.switch1);
            switch1.CheckedChange += Switch1_CheckedChange;
            switch2 = (Switch)FindViewById(Resource.Id.switch2);
            switch2.CheckedChange += Switch2_CheckedChange;
            switch3 = (Switch)FindViewById(Resource.Id.switch3);
            switch3.CheckedChange += Switch3_CheckedChange;
            
        }

        private void Switch3_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            //show all notification
            Toast.MakeText(this, ((e.IsChecked) ?"Changed Succesfully":"Unchanged"), ToastLength.Short);
        }

        private void Switch2_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            //show notification with my locations only
            Toast.MakeText(this, ((e.IsChecked) ? "Changed Succesfully" : "Unchanged"), ToastLength.Short);
        }

        private void Switch1_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            //show notification with my contribution only
            Toast.MakeText(this, ((e.IsChecked) ? "Changed Succesfully" : "Unchanged"), ToastLength.Short);
        }
    }
}