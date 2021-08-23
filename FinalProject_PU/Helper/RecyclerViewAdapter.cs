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

        //Ads Items
        public ImageView Ad_image { get; set; }
        public TextView Ad_Title { get; set; }
        public TextView Ad_text { get; set; }
        public ImageView Ad_button { get; set; }


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

            //items from AdItems.xml
            Ad_image = itemView.FindViewById<ImageView>(Resource.Id.imgIssue);
            Ad_Title = itemView.FindViewById<TextView>(Resource.Id.adtitle);
            Ad_text = itemView.FindViewById<TextView>(Resource.Id.adtext);
            Ad_button = itemView.FindViewById<ImageView>(Resource.Id.adgoButton);




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
        private List<AdsData> lstAdsData = new List<AdsData>();

        public RecyclerViewAdapter(List<Data> lstData, List<AdsData> lstAdsData)
        {
            this.lstData = lstData;
            this.lstAdsData = lstAdsData;
            
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
            
            if(GetItemViewType(position)!=0)
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
            else
            {
                //try
                //{
                    int AdPosition = position / 6 -1;
                    int checknumber = position;
                    if(AdPosition < lstAdsData.Count)
                    {
                        RecyclerViewHolder viewHolder = holder as RecyclerViewHolder;
                        byte[] arr = Convert.FromBase64String(lstAdsData[AdPosition].Adsimage);
                        Bitmap bitmap = BitmapFactory.DecodeByteArray(arr, 0, arr.Length);
                        viewHolder.Ad_image.SetImageBitmap(bitmap);
                        char[] title = lstAdsData[AdPosition].Adstitle.ToCharArray();
                        viewHolder.Ad_Title.SetText(title, 0, title.Length);
                        char[] text = lstAdsData[AdPosition].Adstext.ToCharArray();
                        viewHolder.Ad_text.SetText(text, 0, text.Length);
                        var index = AdPosition;
                        viewHolder.Ad_button.Click += (sender, EventArgs) =>
                        {
                            Adbutton_Click(sender, EventArgs, index);
                        };
                         AdPosition += 1;//
                        
                    }

                //}
                //catch (Exception ex)
                //{
                //    System.Diagnostics.Debug.WriteLine("EXCEPTION::" + ex.ToString());
                //    throw;
                //}
                //finally
                //{
                //    AdPosition += 1;
                //}


            }

        }

        private void Adbutton_Click(object sender, EventArgs eventArgs, int adPosition)
        {
            string uri= lstAdsData[adPosition].websitelink;
            if (!uri.Contains("http://") || !uri.Contains("https://"))
            {
                uri=uri.Insert(0, "http://");
            }
            var f_uri = Android.Net.Uri.Parse(uri);
            var intent = new Intent(Intent.ActionView, f_uri);
            Application.Context.StartActivity(intent);
        }

        public override int GetItemViewType(int position)
        {
            
            if(position % 6 == 0 && position!=0)
            {
                int AdPosition = position / 6 -1;
                if(AdPosition<lstAdsData.Count)
                {
                    return 0;
                }
                //if(AdPosition<lstAdsData.Count)
                //{
                //    return 0; //return AD_TYPE
                //}
                //return 1;
                
                
            }
            return 1; //return VIEW_TYPE
        }


        private void GoLast_Click(object sender, EventArgs e, int position)
        {
            mrecyclerView.SmoothScrollToPosition(0);
        }

        private void Report_Click(object sender, EventArgs e, int position)
        {
            Intent i = new Intent(Application.Context, typeof(ReportIssueHome));
            i.PutExtra("issueobj", JsonConvert.SerializeObject(lstData[position]));
            i.AddFlags(ActivityFlags.NewTask);
            Application.Context.StartActivity(i);
        }

        private void Contribute_Click(object sender, EventArgs e, int position)
        {
            Intent i = new Intent(Application.Context, typeof(ContributeFund));
            i.AddFlags(ActivityFlags.NewTask);
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
            i.AddFlags(ActivityFlags.NewTask);
            Application.Context.StartActivity(i);

        }

        int ItemTYPE;
        private void ViewStatus_Click(object sender, EventArgs e,int position)
        {
            Intent i = new Intent(Application.Context, typeof(ViewStatusHome));
            i.PutExtra("issueobj", JsonConvert.SerializeObject(lstData[position]));
            i.AddFlags(ActivityFlags.NewTask);
            Application.Context.StartActivity(i);
            
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            if(viewType==0 )
            {
                LayoutInflater inflater = LayoutInflater.From(parent.Context);
                //TO CHANGE WITH LAYOUT FOR ADS SHOWING

                Android.Views.View itemView = inflater.Inflate(Resource.Layout.AdItems, parent, false);
                ItemTYPE = 0;
                return new RecyclerViewHolder(itemView);
            }
            else
            {
                
                LayoutInflater inflater = LayoutInflater.From(parent.Context);
                Android.Views.View itemView = inflater.Inflate(Resource.Layout.items, parent, false);
                ItemTYPE=1;
                return new RecyclerViewHolder(itemView);
            }
           
        }
    }
}