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
    class Missingvehicle:Issue
    {
        public string isVehicleLostorFound
        {
            get;set;
        }

        public string plateNumber { get; set; }
        public DateTime missingDate { get; set; }
        public DateTime foundDate { get; set; }

        


    }
}