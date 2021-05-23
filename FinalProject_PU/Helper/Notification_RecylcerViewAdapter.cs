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
       
        public CircleImageView UserImage { get; set; }
        public TextView UserName { get; set; }
        public TextView IssueDate { get; set; }
        public MultiAutoCompleteTextView IssueStatement { get; set; }

        
        public Notification_RecyclerViewHolder(Android.Views.View itemView) : base(itemView)
        {
           
            UserImage = itemView.FindViewById<CircleImageView>(Resource.Id.userimage);
            UserName = itemView.FindViewById<TextView>(Resource.Id.username);
            IssueDate = itemView.FindViewById<TextView>(Resource.Id.date);
            IssueStatement = itemView.FindViewById<MultiAutoCompleteTextView>(Resource.Id.description );

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

            byte[] arr1 = Convert.FromBase64String(lstData[position].image); //UserImage
            Android.Graphics.Bitmap b1 = BitmapFactory.DecodeByteArray(arr1, 0, arr1.Length);
            viewHolder.UserImage.SetImageBitmap(b1); //

            viewHolder.IssueStatement.Text = lstData[position].message;

        }



        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            Android.Views.View itemView = inflater.Inflate(Resource.Layout.Notification_items, parent, false);
            return new Notification_RecyclerViewHolder(itemView);
        }
    }


}