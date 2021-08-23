using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.Fragment.App;
using AndroidX.RecyclerView.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject_PU
{
    class ViewPagerAdapter : FragmentPagerAdapter
    {
        public List<AndroidX.Fragment.App.Fragment> fragment { get; set; }
        public List<string> fragmentNames { get; set; }

        public ViewPagerAdapter(AndroidX.Fragment.App.FragmentManager fragmentmanager): base(fragmentmanager)
        {
            fragment = new List<AndroidX.Fragment.App.Fragment>();
            fragmentNames = new List<string>();
        }

        public override int Count
        {
            get
            {
                return fragment.Count();
            }
        }

        public void AddFragment(AndroidX.Fragment.App.Fragment fragMent, string fragment_title)
        {
            fragment.Add(fragMent);
            fragmentNames.Add(fragment_title);
        }
        public override AndroidX.Fragment.App.Fragment GetItem(int position)
        {
            return fragment[position];
        }

        public static implicit operator RecyclerView.Adapter(ViewPagerAdapter v)
        {
            throw new NotImplementedException();
        }
    }
}