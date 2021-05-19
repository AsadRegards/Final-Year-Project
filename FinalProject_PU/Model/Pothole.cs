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
    class Pothole : Issue
    {
        public string waterLevel { get; set; }
        

        public string roadSize { get; set; }
       

        public string issuePositionwrtRoad { get; set;}

        public string traffic
        {
            get; set;
        }

        public string isRoadBlock
        {
            get; set;
        }

        
    }
}