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
    [Activity(Label = "testActivity",MainLauncher =true)]
    public class testActivity : Activity
    {
        Button btn1;
        TextView tv1;
        PushNotification.MainClass m = new PushNotification.MainClass();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.testlayout);
            // Create your application here
            btn1 = FindViewById<Button>(Resource.Id.button1);
            btn1.Click += Btn1_Click;
            tv1 = FindViewById<TextView>(Resource.Id.textView1);

            m.isPlayServiceAvailable(this, tv1);
        }

        private void Btn1_Click(object sender, EventArgs e)
        {
            m.onButtonClick();

        }
    }
}