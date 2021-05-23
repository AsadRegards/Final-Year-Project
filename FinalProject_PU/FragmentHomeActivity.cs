using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using FinalProject_PU.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_PU
{
    [Activity(Label = "FragmentHomeActivity", Theme = V)]
    public class FragmentHomeActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        private const string V = "@style/Theme.AppCompat.Light.NoActionBar";

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        

        
        TextView textMessage;
        static List<AndroidX.Fragment.App.Fragment> list1;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.FragmentHome);

            list1 = new List<AndroidX.Fragment.App.Fragment>();

            list1.Add(new IssueFragment());
            list1.Add(new FundsFragment());
            list1.Add(new notificationfragment());
            list1.Add(new Settings());
            list1.Add(new DirectionFragment());

            SupportFragmentManager.BeginTransaction()
                       .Replace(Resource.Id.fragment_main, list1[0])
                       .Commit();


            BottomNavigationView Mnavigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            Mnavigation.SetOnNavigationItemSelectedListener(this);
            Mnavigation.ItemIconTintList = null;




        }


        public bool OnNavigationItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.navigation_home:
                    SupportFragmentManager.BeginTransaction()
                       .Replace(Resource.Id.fragment_main, list1[0])
                       .Commit();
                    return true;
                case Resource.Id.navigation_funds:
                    SupportFragmentManager.BeginTransaction()
                       .Replace(Resource.Id.fragment_main, list1[1])
                       .Commit();
                    return true;
                case Resource.Id.navigation_notifications:
                    SupportFragmentManager.BeginTransaction()
                      .Replace(Resource.Id.fragment_main, list1[2])
                      .Commit();
                    return true;
                case Resource.Id.navigation_map:
                    SupportFragmentManager.BeginTransaction()
                      .Replace(Resource.Id.fragment_main, list1[4])
                      .Commit();
                    return true;
                case Resource.Id.navigation_settings:
                    SupportFragmentManager.BeginTransaction()
                      .Replace(Resource.Id.fragment_main, list1[3])
                      .Commit();
                    return true;





            }
            return false;
        }

    }
}