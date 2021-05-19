using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinalProject_PU.Helper;
using Refractored.Controls;
using System.Threading.Tasks;

namespace FinalProject_PU
{
    public class FundsFragment : AndroidX.Fragment.App.Fragment
    {

        private RecyclerView recycler;
        private Funds_RecyclerViewAdapter adapter;
        private RecyclerView.LayoutManager layoutManager;
        private List<Model.OpenForFundsIssues> lstData = new List<Model.OpenForFundsIssues>();
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override Android.Views.View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);


            var rootview = inflater.Inflate(Resource.Layout.FundsFragment, container, false);

            recycler = rootview.FindViewById<RecyclerView>(Resource.Id.recyclerView3);
            recycler.HasFixedSize = true;

            
            return rootview;

        }

        public async override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            lstData=await InitData();
            //layoutManager = new GridLayoutManager(Application.Context, 4,LinearLayoutManager.Vertical, false);
            layoutManager = new LinearLayoutManager(Application.Context, LinearLayoutManager.Vertical, false);
            recycler.SetLayoutManager(layoutManager);
            adapter = new Funds_RecyclerViewAdapter(lstData);
            recycler.SetAdapter(adapter);


        }

        public async Task<List<Model.OpenForFundsIssues>> InitData()
        {
            Control.Funds funds = new Control.Funds();
            return await funds.GetOpenFundingIssues();
        }
       
    }
}