﻿using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using FinalProject_PU.Helper;
using Refractored.Controls;
using System.Collections.Generic;
using Android.App;
using FinalProject_PU.Control;
using System;
using System.Threading.Tasks;
using MohammedAlaa.GifLoading;

namespace FinalProject_PU
{
    public class IssueFragment : AndroidX.Fragment.App.Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
            
        }

        ImageView imgwriteIssue;
        LoadingView loader;


        private RecyclerView recycler;
        private RecyclerViewAdapter adapter;
        private RecyclerView.LayoutManager layoutManager;
        private static List<Data> lstData = new List<Data>();
        private static List<AdsData> lstAdData = new List<AdsData>();


       

        public  override Android.Views.View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment

            var RootView= inflater.Inflate(Resource.Layout.Home, container, false);




            loader = RootView.FindViewById<LoadingView>(Resource.Id.loading_view_button);
            loader.Visibility = ViewStates.Visible;
            recycler = RootView.FindViewById<RecyclerView>(Resource.Id.recyclerView1);
            recycler.HasFixedSize = true;
            //  layoutManager = new LinearLayoutManager(this);
            layoutManager = new GridLayoutManager(Application.Context, 1, GridLayoutManager.Horizontal, false);
            recycler.SetLayoutManager(layoutManager);
            

            
            

            byte[] arr = Convert.FromBase64String(UserInfoHolder.Profile_pic);

            imgwriteIssue = (ImageView)RootView.FindViewById(Resource.Id.imgwriteIssue);
            imgwriteIssue.Click += delegate
            {
                var i = new Intent(Application.Context, typeof(CreateIssue));
                this.StartActivity(i);
            };
            //CircleImageView img = RootView.FindViewById<CircleImageView>(Resource.Id.circleImageView1);
            //Android.Graphics.Bitmap bitmap = BitmapFactory.DecodeByteArray(arr, 0, arr.Length);
            //img.SetImageBitmap(bitmap);

            return RootView;

           
        }

        public override async void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            try
            {
                lstData = await InitData();
                lstAdData = await InitAds();
                lstData.Reverse();
                adapter = new RecyclerViewAdapter(lstData, lstAdData);
                recycler.SetAdapter(adapter);
            }
            finally
            {
                loader.Visibility = ViewStates.Gone;
            }
           
            


        }


        private async Task<List<Helper.Data>> InitData()
        {
            return await IssueController.FetchPostList();
        }

        private async Task<List<AdsData>> InitAds()
        {
            return await AdsData.GetAllAds();
        }
    }
}