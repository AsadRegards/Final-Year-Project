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
using Lsjwzh.Widget.MaterialLoadingProgressBar;

namespace FinalProject_PU.View
{
    static class ViewFunctions
    {
        static public void HideCircleProgessbar(CircleProgressBar cpb)
        {
            cpb.Visibility = Android.Views.ViewStates.Invisible;
        }
    }
}