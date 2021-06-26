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
    public class notificationfragment : AndroidX.Fragment.App.Fragment
    {


        private RecyclerView recycler;
        private Notifictaion_RecyclerViewAdapter adapter;
        private RecyclerView.LayoutManager layoutManager;
        private List<Model.Notification> lstData = new List<Model.Notification>();

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            
        }

        public async override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            lstData = await InitData();
            layoutManager = new LinearLayoutManager(Application.Context, LinearLayoutManager.Vertical, false);
            recycler.SetLayoutManager(layoutManager);
            lstData.Reverse();
            adapter = new Notifictaion_RecyclerViewAdapter(lstData);
            recycler.SetAdapter(adapter);

        }

        public override Android.Views.View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment

            var rootview = inflater.Inflate(Resource.Layout.notification, container, false);

            

            recycler = rootview.FindViewById<RecyclerView>(Resource.Id.recyclerView2);
            recycler.HasFixedSize = true;
            
            
            


            //isline ko uncomment krna hai
           //byte[] arr = Convert.FromBase64String(UserInfoHolder.Profile_pic);

           
            return rootview;




        }
        private async Task<List<Model.Notification>> InitData()
        {
            return await new Model.Notification().getallnotification();
        }

        }
}