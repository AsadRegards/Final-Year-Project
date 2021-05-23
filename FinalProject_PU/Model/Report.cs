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

namespace FinalProject_PU.Model
{
    class Report
    {
        public int report_id { get; set; }
        public int user_id { get; set; }
        public int issue_id { get; set; }
        public string report_text { get; set; }
        public string status { get; set; }
        public string admin_reply { get; set; }
    }
}