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

namespace FinalProject_PU.Control
{
    class IssueFlagDetector:IDisposable
    {
        public string DetectRainwaterFlag(Model.Rainwater issue)
        {
            
            if(issue.Vehiclepassing== "novehiclecanpass")
            {
                return "red";
            }
            if(issue.Vehiclepassing== "highvehiclecanpass")
            {
                return "yellow";
            }
            if(issue.Vehiclepassing== "allvehiclecanpass")
            {
                return "green";
            }

            return null;
        }

        public string DetectManholeFlag(Model.Manhole issue)
        {
            if(issue.Iswateroverflow==true)
            {
                return "red";
            }
            if(issue.Iswateroverflow==false)
            {
                return "yellow";
            }
            return null;
        }

        public string DetectPotholeFlag(Model.Pothole issue)
        {
            if(issue.issuePositionwrtRoad== "rightfasttrack" && issue.traffic== "heavytraffic")
            {
                return "red";
            }
            if (issue.issuePositionwrtRoad == "leftslowtrack" && issue.traffic == "moderatetraffic" || issue.traffic == "lighttraffic")
            {
                return "yellow";
            }
            if(issue.issuePositionwrtRoad=="middle" && issue.traffic=="lighttraffic" && issue.roadSize=="threelane" || issue.roadSize=="fourlane")
            {
                return "green";
            }
            if(issue.roadSize=="twolane")
            {
                if(issue.traffic=="moderatetraffic" || issue.traffic=="heavytraffic")
                {
                    return "red";
                }
                return "yellow";
            }

            return "yellow";
        }

        public string DetectDebrisFlag(Model.Debris issue)
        {
            if(issue.roadCoverage=="fullblocked")
            {
                return "red";
            }
            if(issue.roadCoverage=="halfblocked")
            {
                if(issue.roadSize=="twolane")
                {
                    return "red";
                }
                return "yellow";
            }
            if(issue.roadCoverage=="smallarea")
            {
                return "green";
            }

            return "yellow";
        }

        public string DetectGarbageFlag(Model.Garbage issue)
        {
            if (issue.roadCoverage == "fullblocked")
            {
                return "red";
            }
            if (issue.roadCoverage == "halfblocked")
            {
                if (issue.roadSize == "twolane")
                {
                    return "red";
                }
                return "yellow";
            }
            if (issue.roadCoverage == "smallarea")
            {
                return "green";
            }

            return "yellow";
        }

        public void Dispose()
        {
            Dispose();
        }
    }
}