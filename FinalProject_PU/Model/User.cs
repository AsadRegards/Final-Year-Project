 using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Essentials;

namespace FinalProject_PU.Model
{
    public class User
    {
        public int user_id { get; set; }
        
        public string name { get; set; }
        public string email_address { get; set; }
        public string contact_no { get; set; }
        public string profile_pic { get; set; }

        public string  homelocatlati { get; set; }
        public string homelocatlongi { get; set; }
        public string joblocatlati { get; set; }

        public string joblocatlongi { get; set; }

        public int userrating { get; set; }

        public int password_hash { get; set; }

     
        
       





    }
}