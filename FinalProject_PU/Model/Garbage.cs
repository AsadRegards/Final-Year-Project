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
    class Garbage:Issue
    {
        public string roadSize
        {
            get;set;
        }
        //road blockage is road half blocked and full blocked
        public string roadCoverage { get; set; }
        public string issuePositionwrtRoad
        {
            get;set;
        }

        public string traffic
        {
            get;set;
        }

        public string isRoadBlock
        {
            get;set;
        }

          

    }
}