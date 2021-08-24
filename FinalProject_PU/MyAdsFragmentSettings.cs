using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using FinalProject_PU.Control;
using Newtonsoft.Json;
using Plugin.Media;
using Refractored.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject_PU
{
    [Activity(Label = "MyAdsFragmentSettings",NoHistory =true)]
    public class MyAdsFragmentSettings : Activity
    {
        ImageView back, uploadimg, submit,uploaded;
        CircleImageView userimage;
        TextView Username;
        EditText edturl, budget;
        private string base64image;
        private bool IsImageUploaded = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MyAds);
            back = (ImageView)FindViewById(Resource.Id.imgbackgo);
            back.Click += Back_Click;
            userimage = (CircleImageView)FindViewById(Resource.Id.usericon);
            Username = (TextView)FindViewById(Resource.Id.username);
            uploadimg = (ImageView)FindViewById(Resource.Id.imguploadimg);
            uploadimg.Click += Uploadimg_Click;
            edturl = (EditText)FindViewById(Resource.Id.edtUrl);
            submit = (ImageView)FindViewById(Resource.Id.imgsubmitt);
            submit.Click += Submit_Click;
            budget = FindViewById<EditText>(Resource.Id.edtBudget);
            //runtime py name and user image change start
            char[] arr = UserInfoHolder.User_name.ToCharArray();
            byte[] arra = Convert.FromBase64String(Control.UserInfoHolder.Profile_pic);
            Android.Graphics.Bitmap bitmapp = BitmapFactory.DecodeByteArray(arra, 0, arra.Length);
            userimage.SetImageBitmap(bitmapp);
            Username.SetText(arr, 0, arr.Length);
            //end



            // Create your fragment here
        }


        private void Submit_Click(object sender, EventArgs e)
        {
            if(Control.InputValidation.validateUri(edturl.Text))
            {
                if(IsImageUploaded)
                {
                    int budgetAmount;
                   if(int.TryParse(budget.Text,out budgetAmount))
                    {
                        var intent = new Intent(this, typeof(AdTitleandTextActivity));
                        intent.PutExtra("baseimage", JsonConvert.SerializeObject(base64image));
                        intent.PutExtra("budget", JsonConvert.SerializeObject(budgetAmount));
                        intent.PutExtra("link", JsonConvert.SerializeObject(edturl.Text));
                        StartActivity(intent);
                    }
                   else
                    {
                        Toast.MakeText(this, "Please enter budget amount", ToastLength.Long).Show();
                    }
                    
                }
                else
                {
                    Toast.MakeText(this, "Please upload an image for your ad", ToastLength.Long).Show();
                }

            }
            else
            {
                Toast.MakeText(this, "Please enter correct website address", ToastLength.Long).Show();
            }
        }
        long lastPress;
        public override void OnBackPressed()
        {
            // source https://stackoverflow.com/a/27124904/3814729
            long currentTime = DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond;

            // source https://stackoverflow.com/a/14006485/3814729
            if (currentTime - lastPress > 5000)
            {
                Toast.MakeText(this, "Press back again to exit", ToastLength.Long).Show();
                lastPress = currentTime;
            }
            else
            {

                FinishAffinity();

            }
        }
        private void Uploadimg_Click(object sender, EventArgs e)
        {
            try
            { 
                UploadPhoto();
                //if (IsImageUploaded)
                //{
                //    uploaded.Visibility = Android.Views.ViewStates.Visible;
                //}
                
            }
            catch(NullReferenceException)
            {
                Toast.MakeText(this, "Please select any photo to upload", ToastLength.Long).Show();
            }
            catch(Exception)
            {
                Toast.MakeText(this, "Image couldn't be selected at this time", ToastLength.Long).Show();
            }
        }

        public async void UploadPhoto()
        {
            try
            {
                await CrossMedia.Current.Initialize();
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    Toast.MakeText(this, "upload not supported on this device", ToastLength.Short).Show();

                }

                var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small,
                    CompressionQuality = 90

                });

                //convert file to byte array , to bitmap
                byte[] imageArray = System.IO.File.ReadAllBytes(file.Path);
                base64image = Convert.ToBase64String(imageArray);
                IsImageUploaded = true;


            }
            catch (Exception ex)
            { Toast.MakeText(this, "Please select any image to represent the issue",ToastLength.Long).Show(); }




        }
        private void Back_Click(object sender, EventArgs e)
        {
            this.OnBackPressed();
        }
    }
}