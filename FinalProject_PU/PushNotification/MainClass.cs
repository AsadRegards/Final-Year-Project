using Android.App;
using Android.Content;
using Android.Gms.Common;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Firebase.Iid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject_PU.PushNotification
{
    class MainClass
    {

        public bool isPlayServiceAvailable(Android.Content.Context acc, TextView tv)
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(acc);
            if (resultCode!= ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                {
                    tv.Text = GoogleApiAvailability.Instance.GetErrorString(resultCode);
                }
                else
                {
                    tv.Text = "This device is not supported";
                }
                return false;
            }
            else
            {
                tv.Text = "Google play service is available";
                return true;
                
            }
        }
        public void onButtonClick()
        {
            //Show token on logcat
            Log.Debug("TOKEN", "Instance ID Token: " + FirebaseInstanceId.Instance.Token);
        }
    }

    
}