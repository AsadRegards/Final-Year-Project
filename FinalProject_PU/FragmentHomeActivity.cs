using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.ViewPager.Widget;
using AndroidX.ViewPager2.Widget;
using FinalProject_PU.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Android.Views.View;

namespace FinalProject_PU
{
    [Activity(Label = "FragmentHomeActivity", Theme = V)]
    public class FragmentHomeActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener,IOnTouchListener
    {
        private const string V = "@style/Theme.AppCompat.Light.NoActionBar";

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        ViewPager viewPager;
        BottomNavigationView Mnavigation;
        //creating fragment instance
        IssueFragment Home = new IssueFragment();
        FundsFragment funds = new FundsFragment();
        notificationfragment notification = new notificationfragment();
        DirectionFragment direction = new DirectionFragment();
        Settings settings = new Settings();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.FragmentHome);
            ConnectViews();

 
        }

        public void ConnectViews()
        {
            viewPager = FindViewById<ViewPager>(Resource.Id.viewPager1);
            Mnavigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            Mnavigation.SetOnNavigationItemSelectedListener(this);
            viewPager.OffscreenPageLimit = 5;
            SetupViewpager();
            
        }

        private void SetupViewpager()
        {
            ViewPagerAdapter adapter = new ViewPagerAdapter(SupportFragmentManager);
            adapter.AddFragment(Home, "Home");
            adapter.AddFragment(funds, "Funds");
            adapter.AddFragment(notification, "Notification");
            adapter.AddFragment(direction, "Direction");
            adapter.AddFragment(settings, "Settings");
            viewPager.Adapter = adapter;

        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.navigation_home:
                    viewPager.SetCurrentItem(0, true);
                    return true;
                case Resource.Id.navigation_funds:
                    viewPager.SetCurrentItem(1, true);
                    return true;
                case Resource.Id.navigation_notifications:
                    viewPager.SetCurrentItem(2, true);
                    return true;
                case Resource.Id.navigation_map:
                    viewPager.SetCurrentItem(3, true);
                    return true;
                case Resource.Id.navigation_settings:
                    viewPager.SetCurrentItem(4, true);
                    return true;

            }
            return false;
        }

        public bool OnTouch(Android.Views.View v, MotionEvent e)
        {
            return false;
        }

        
    }
}