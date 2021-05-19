using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject_PU.Control
{
    class IssueOper
    {
        public static void ProceedToIssueActivity(string selected, Android.Content.Context acc, object obj)
        {
            switch (selected)
            {
                case "pothole":
                    DataOper.PutData<createissue3>(acc, obj);
                    break;

                case "manhole":
                    DataOper.PutData<Manhole1>(acc, obj);
                    break;

                case "debris":
                    DataOper.PutData<Debris>(acc, obj);
                    break;

                case "garbage":
                    DataOper.PutData<Garbage>(acc, obj);
                    break;

                case "planting":
                    DataOper.PutData<PlantingCampaign1>(acc, obj);
                    break;

                case "missingvehicle":
                    DataOper.PutData<MissingVehicle>(acc, obj);
                    break;

                case "rainwater":
                    DataOper.PutData<Rainwater>(acc, obj);
                    break;
                case "brokenpole":
                    DataOper.PutData<BrokenWires>(acc, obj);
                    break;

            }

        }

      

    }
}