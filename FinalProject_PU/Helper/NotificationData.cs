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

namespace FinalProject_PU.Helper
{
    class NotificationData
    {
        
        public string UserImage { get; set; }
        public string UserName { get; set; }
        public DateTime IssueDate { get; set; }
        public string IssueStatement { get; set; }

        public string ElevatedDays { get; set; }


        public int GetElevatedDates()
        {
            var ElevatedDays = (DateTime.Now.Date - IssueDate.Date).Days;
            return ElevatedDays;
        }

        public NotificationData(DateTime date)
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