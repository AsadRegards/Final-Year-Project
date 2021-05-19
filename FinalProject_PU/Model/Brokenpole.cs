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
    class Brokenpole:Issue
    {
        public string roadSize
        {
            get;set;
        }

        public string traffic
        {
            get;set;
        }

        //road blockage is the road half blocked or full blocked
        public string roadCoverage { get; set; }

        public string isRoadBlock
        {
            get;set;
        }
        

    }
}