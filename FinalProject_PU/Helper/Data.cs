using Android.App;
using Android.Content;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject_PU.Helper
{
    public class Data
    {
        public string IssueImage { get; set; }
        public string UserImage { get; set; }
        public string UserName { get; set; }
        public DateTime IssueDate { get; set; }
        public string IssueStatement { get; set; }
        public string IssueLatitude { get; set; }
        public string IssueLongitude { get; set; }
        public string Issueflag { get; set; }
        public string IssueType { get; set; }

        public LatLng GetLocation()
        {
            return new LatLng(Convert.ToDouble(IssueLatitude), Convert.ToDouble(IssueLongitude));
        }

        public string ElevatedDays { get; set; }


        public int GetElevatedDates()
        {
            var ElevatedDays = (DateTime.Now.Date - IssueDate.Date).Days;
            return ElevatedDays;
        }

        public Data()
        {
            
            if (GetElevatedDates() == 0)
            {
                ElevatedDays = "Today";
            }
            else
            {
                ElevatedDays = "Today";
            }
        }
    }
}