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
using System.Text.RegularExpressions;
using Xamarin.Essentials;

namespace FinalProject_PU.Control
{
   
    static class InputValidation
    {
        static public bool ValidateUsername(string username, Android.Content.Context acc)
        {
            if (username == "")
            {
                Toast.MakeText(acc, "Please Enter a Username", ToastLength.Long).Show();
                return false;


            }
            Regex rg = new Regex(@"^[a-zA-Z\s]{4,15}$");
            Match mat = rg.Match(username);
            if(!mat.Success)
            {
                Toast.MakeText(acc, "Username must not contain any number.", ToastLength.Long).Show();
                return false;
            }

            return true;
        }


        static public bool ValidateContact(string contact,Android.Content.Context acc)
        {
            Regex reg = new Regex(@"(^[0][3][0-4][0-9]\d{7}$)");
            Match match = reg.Match(contact);
            if (!match.Success  || contact=="")
            {
                MainThread.BeginInvokeOnMainThread(() => 
                {
                    Toast.MakeText(acc, "Please enter valid contact number", ToastLength.Long).Show();
                });
                
                return false;
            }
            return true;
        }

        static public bool ValidatePassword(string password,string confirmpassword, Android.Content.Context acc)
        {
            Regex reg = new Regex(@"^[a-zA-Z0-9]{8,20}$");
            Match mat = reg.Match(password);
            if(!mat.Success || password=="")
            {
                MainThread.BeginInvokeOnMainThread(() => {
                    Toast.MakeText(acc, "Please Enter Valid Password, Password must contain lowercase alphabet, Upppercase alphabet, number and special character and must consist of 8 characters!!!", ToastLength.Long).Show();
                });
               
                return false;
            }
            if (password != confirmpassword)
            {

                MainThread.BeginInvokeOnMainThread(() => {
                    Toast.MakeText(acc, "Password don't match!", ToastLength.Long).Show();
                });

                return false;
            }
            return true;
        }


    }
}