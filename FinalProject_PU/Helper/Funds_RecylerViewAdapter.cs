using Android.App;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FinalProject_PU.Helper
{
    class Funds_RecyclerViewHolder : RecyclerView.ViewHolder
    {

        static int Position_;
        //public TextView Username { get; set; }
        public TextView FundDate { get; set; }
        public ImageView ViewIssue { get; set; }
        public ImageView Contribute { get; set; }
        public ImageView ViewStatus { get; set; }
        public TextView FundStatement { get; set; }

        public Funds_RecyclerViewHolder(Android.Views.View itemsView) : base(itemsView)
        {
            //Username = itemsView.FindViewById<TextView>(Resource.Id.username);
            FundDate = itemsView.FindViewById<TextView>(Resource.Id.date);
            FundStatement = itemsView.FindViewById<TextView>(Resource.Id.description);
            ViewIssue = itemsView.FindViewById<ImageView>(Resource.Id.btnView);
            Contribute = itemsView.FindViewById<ImageView>(Resource.Id.btnContribute);
            ViewStatus = itemsView.FindViewById<ImageView>(Resource.Id.btnStatus);

        }

    }
    class Funds_RecyclerViewAdapter : RecyclerView.Adapter
    {
        private List<Model.OpenForFundsIssues> lstData = new List<Model.OpenForFundsIssues>();

        public Funds_RecyclerViewAdapter(List<Model.OpenForFundsIssues> lstData)
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

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            
            Funds_RecyclerViewHolder viewHolder = holder as Funds_RecyclerViewHolder;



          //  viewHolder.Username.Text = lstData[position].userName;
            viewHolder.FundDate.Text = lstData[position].issueDate.ToShortDateString();
            viewHolder.FundStatement.Text = lstData[position].issueStatement;
            viewHolder.ViewStatus.Click += (sender, EventArgs) =>
              {
                  ViewStatus_Click(sender, EventArgs, position);
              };
            viewHolder.ViewIssue.Click += (sender, EventArgs) =>
              {
                  ViewIssue_Click(sender, EventArgs, position);
              };
            viewHolder.Contribute.Click += (sender, EventArgs) =>
              {
                  Contribute_Click(sender, EventArgs, position);
              };

            
            
           

        }

        private void Contribute_Click(object sender, EventArgs e, int Position)
        {
            Intent i = new Intent(Application.Context, typeof(UserFund));
            i.PutExtra("issueObj", JsonConvert.SerializeObject(lstData[Position]));
            Application.Context.StartActivity(i);
        }

        private void ViewIssue_Click(object sender, EventArgs e, int Position)
        {
            Intent i = new Intent(Application.Context, typeof(ViewIssueFund));
            i.PutExtra("issueObj", JsonConvert.SerializeObject(lstData[Position]));
            Application.Context.StartActivity(i);
        }

        private void ViewStatus_Click(object sender, EventArgs e, int Position)
        {
            Intent i = new Intent(Application.Context, typeof(ViewStatusFund));
            i.PutExtra("issueObj", JsonConvert.SerializeObject(lstData[Position]));
            Application.Context.StartActivity(i);
        }

        
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            Android.Views.View itemView = inflater.Inflate(Resource.Layout.Funds_Items, parent, false);
            return new Funds_RecyclerViewHolder(itemView);
        }
    }
}