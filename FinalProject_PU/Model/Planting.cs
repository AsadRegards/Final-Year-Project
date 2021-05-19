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
    class Planting:Issue
    {
        public string Schedule
        {
            get;set;
        }

        public DateTime time { get; set; }
       

    }
}