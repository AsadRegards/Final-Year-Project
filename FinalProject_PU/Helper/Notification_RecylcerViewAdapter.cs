using Android.Graphics;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Refractored.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace FinalProject_PU.Helper
{
   
    class Notification_RecyclerViewHolder: RecyclerView.ViewHolder
    {
       
        public CircleImageView IssueImage { get; set; }
        public TextView UserName { get; set; }
        public TextView IssueDate { get; set; }
        public  TextView IssueStatement { get; set; }

        
        public Notification_RecyclerViewHolder(Android.Views.View itemView) : base(itemView)
        {
           
            
            IssueImage = itemView.FindViewById<CircleImageView>(Resource.Id.userimage); //This imageview is for Issue Image not userImage
            UserName = itemView.FindViewById<TextView>(Resource.Id.username);
            IssueDate = itemView.FindViewById<TextView>(Resource.Id.date);
            IssueStatement = itemView.FindViewById<TextView>(Resource.Id.description );

        }

    }
    class Notifictaion_RecyclerViewAdapter : RecyclerView.Adapter
    {
        private List<Model.Notification> lstData = new List<Model.Notification>();

        public Notifictaion_RecyclerViewAdapter(List<Model.Notification> lstData)
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
            Notification_RecyclerViewHolder viewHolder = holder as Notification_RecyclerViewHolder;

            byte[] arr1 = Convert.FromBase64String(lstData[position].notification_image); //IssueImage
            Android.Graphics.Bitmap b1 = BitmapFactory.DecodeByteArray(arr1, 0, arr1.Length); 
            //Java.Lang.OutOfMemoryError: 'Failed to allocate a 62436 byte allocation with 21600 free bytes and 21KB until OOM'

            viewHolder.IssueImage.SetImageBitmap(b1); //
            viewHolder.IssueDate.Text = lstData[position].notification_date.ToShortDateString();
            viewHolder.IssueStatement.Text = lstData[position].notification_text;

        }



        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            Android.Views.View itemView = inflater.Inflate(Resource.Layout.Notification_items, parent, false);
            return new Notification_RecyclerViewHolder(itemView);
        }
    }


}