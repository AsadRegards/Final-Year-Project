using Android.App;
using Android.Content;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Views;
using FinalProject_PU.Model;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject_PU.Control
{
    static class UserInfoHolder
    {
        public static int User_id { get; set; }
        public static string User_name { get; set; }
        public static string Profile_pic { get; set; }

        public static string email { get; set; }
        public static  string homelocatlati { get; set; }
        public static  string homelocatlongi { get; set; }
        public static  string joblocatlati { get; set; }

        public static  string joblocatlongi { get; set; }
        public static int UserRating { get; set; }
        public static int currentIssueContext { get; set; }

        public static void FetchUserInfo(User user)
        {

            User_id = user.user_id;
            User_name = user.name;
            Profile_pic = user.profile_pic;
            email = user.email_address;
            homelocatlati = user.homelocatlati;
            homelocatlongi = user.homelocatlongi;
            joblocatlati = user.joblocatlati;
            joblocatlongi = user.joblocatlongi;
            UserRating = user.userrating;

        }

        public static void DisposeUserinfo()
        {
            User_id = 0;
            User_name = null;
            Profile_pic = null;
            email = null;
            homelocatlongi = null;
            homelocatlati = null;
            joblocatlongi = null;
            joblocatlati = null;
            
            UserRating = 0;


        }

      
    }
}