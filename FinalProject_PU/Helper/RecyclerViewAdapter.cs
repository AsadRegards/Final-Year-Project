using Android.Graphics;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Refractored.Controls;
using System.Collections.Generic;
using System;
using Android.Content;
using AndroidX.Core.Graphics;
using Android.App;
using Newtonsoft.Json;

namespace FinalProject_PU.Helper
{
    class RecyclerViewHolder : RecyclerView.ViewHolder
    {
        public ImageView imageview { get; set; }
        public CircleImageView UserImage { get; set; }
        public TextView UserName { get; set; }
        public TextView IssueDate { get; set; }
        public TextView IssueStatement { get; set; }
        public ImageView ViewStatus { get; set; }
        public ImageView GoLast { get; set; }
        public ImageView Report { get; set; }
        public ImageView Contribute { get; set; }
        public ImageView ViewOnMap { get; set; }


        Android.Graphics.Typeface tf;
        //
        // public TextView Description { get; set; }
        public RecyclerViewHolder(Android.Views.View itemView) : base(itemView)
        {
            imageview = itemView.FindViewById<ImageView>(Resource.Id.imageView1);
            UserImage = itemView.FindViewById<CircleImageView>(Resource.Id.imgProfile);
            UserName = itemView.FindViewById<TextView>(Resource.Id.tvname);
            IssueDate = itemView.FindViewById<TextView>(Resource.Id.tvtime);
            IssueStatement = itemView.FindViewById<TextView>(Resource.Id.tvinfo);
            ViewStatus = itemView.FindViewById<ImageView>(Resource.Id.ViewStatus);
            ViewOnMap = itemView.FindViewById<ImageView>(Resource.Id.ViewOnMap);
            Contribute = itemView.FindViewById<ImageView>(Resource.Id.ContributeHome);
            Report = itemView.FindViewById<ImageView>(Resource.Id.Report);
            GoLast = itemView.FindViewById<ImageView>(Resource.Id.GoLast);
            
            

            //beauttification

            /*
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            IssueStatement.SetTypeface(tf, TypefaceStyle.Bold);

           
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            UserName.SetTypeface(tf, TypefaceStyle.Bold);

           
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            IssueDate.SetTypeface(tf, TypefaceStyle.Bold);
            */
        }
    }
    class RecyclerViewAdapter : RecyclerView.Adapter
    {
        private List<Data> lstData = new List<Data>();

        public RecyclerViewAdapter(List<Data> lstData)
        {
            this.lstData = lstData;
        }

        
        public override int ItemCount
        {
            get
            {
                return lstData.Count;
            }
        }

        RecyclerView mrecyclerView;
        public override void OnAttachedToRecyclerView(RecyclerView recyclerView)
        {
            base.OnAttachedToRecyclerView(recyclerView);
            mrecyclerView = recyclerView;
            
            
        }
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            RecyclerViewHolder viewHolder = holder as RecyclerViewHolder;
            byte[] arr0 = Convert.FromBase64String(lstData[position].IssueImage); //IssueImage
            Bitmap b0 = BitmapFactory.DecodeByteArray(arr0, 0, arr0.Length);
            viewHolder.imageview.SetImageBitmap(b0);
           byte[] arr1 = Convert.FromBase64String(lstData[position].UserImage); //UserImage
            Bitmap b1 = BitmapFactory.DecodeByteArray(arr1, 0, arr1.Length);
            viewHolder.UserImage.SetImageBitmap(b1); //
            viewHolder.UserName.Text = lstData[position].UserName;
            viewHolder.IssueDate.Text = lstData[position].ElevatedDays;
            viewHolder.IssueStatement.Text = lstData[position].IssueStatement;
            viewHolder.IssueDate.Text = lstData[position].GetElevatedDates();
            
            viewHolder.ViewStatus.Click += (sender, EventArgs) => 
            {
                ViewStatus_Click(sender, EventArgs, position);
            };
            viewHolder.ViewOnMap.Click += (sender, EventArgs) =>
            {
                ViewOnMap_Click(sender, EventArgs, position);
            };
            viewHolder.Contribute.Click += (sender, EventArgs) =>
            {
                Contribute_Click(sender, EventArgs, position);
            };
            viewHolder.Report.Click += (sender, EventArgs) => 
            {
                Report_Click(sender, EventArgs, position);
            };
            viewHolder.GoLast.Click += (sender, EventArgs) => 
            {
                GoLast_Click(sender, EventArgs, position);
                
            };
        }

     
        private void GoLast_Click(object sender, EventArgs e, int position)
        {
            mrecyclerView.SmoothScrollToPosition(0);
        }

        private void Report_Click(object sender, EventArgs e, int position)
        {
            Intent i = new Intent(Application.Context, typeof(ReportIssueHome));
            i.PutExtra("issueobj", JsonConvert.SerializeObject(lstData[position]));
            Application.Context.StartActivity(i);
        }

        private void Contribute_Click(object sender, EventArgs e, int position)
        {
            Intent i = new Intent(Application.Context, typeof(ContributeFund));
            Control.UserInfoHolder.currentIssueContext = lstData[position].IssueId;
            Application.Context.StartActivity(i);
        }

        private void ViewOnMap_Click(object sender, EventArgs e, int position)
        {
            var issuePosition = lstData[position].GetLocation();
            string lat = issuePosition.Latitude.ToString();
            string lon = issuePosition.Longitude.ToString();
            // Control.DataOper.PutData<ViewIssueOnMap>(Application.Context, issuePosition);
            Intent i = new Intent(Application.Context, typeof(ViewIssueOnMap));
            i.PutExtra("lattopass", JsonConvert.SerializeObject(lat));
            i.PutExtra("longtopass", JsonConvert.SerializeObject(lon));
            Application.Context.StartActivity(i);

        }

        private void ViewStatus_Click(object sender, EventArgs e,int position)
        {
            Intent i = new Intent(Application.Context, typeof(ViewStatusHome));
            i.PutExtra("issueobj", JsonConvert.SerializeObject(lstData[position]));
            Application.Context.StartActivity(i);
            
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            Android.Views.View itemView = inflater.Inflate(Resource.Layout.items, parent, false);
            return new RecyclerViewHolder(itemView);
        }
    }
}