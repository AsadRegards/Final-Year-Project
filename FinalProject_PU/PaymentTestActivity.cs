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
using FinalProject_PU.Control;

namespace FinalProject_PU
{
    [Activity(Label = "PaymentTestActivity")]
    public class PaymentTestActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LoginActivity);
            Intent i = new Intent(this, typeof(PayByCard));
            this.StartActivity(i);
            // Create your application here

        }
    }
}