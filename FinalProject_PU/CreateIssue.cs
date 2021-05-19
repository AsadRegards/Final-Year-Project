using Android;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FinalProject_PU.Control;
using FinalProject_PU.Model;
using Newtonsoft.Json;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject_PU
{
    [Activity(Label = "CreateIssue")]
    public class CreateIssue : Activity
    {
        TextView username, textviewinfo, tev1;
        User u;
        //static string base64image;
        Typeface tf;
        ImageView uploadimage, captureimage, iconSettngs, iconMap, iconNotifications, iconFunds, iconHome;
                  
        private static string base64image;
        readonly string[] permissionGroup =
        {
             Manifest.Permission.ReadExternalStorage,
            Manifest.Permission.WriteExternalStorage,
            Manifest.Permission.Camera
        };

        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.CreateIssue);
            RequestPermissions(permissionGroup, 0);
            
            username = (TextView)FindViewById(Resource.Id.tvusername);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            username.SetTypeface(tf, TypefaceStyle.Bold);

            char[] arr = UserInfoHolder.User_name.ToCharArray();
            username.SetText(arr, 0, arr.Length);
            byte[] arra = Convert.FromBase64String(UserInfoHolder.Profile_pic);
            
            ImageView img2 = FindViewById<ImageView>(Resource.Id.circleImageView2);
            Android.Graphics.Bitmap bitmap = BitmapFactory.DecodeByteArray(arra, 0, arra.Length);
            img2.SetImageBitmap(bitmap);

            textviewinfo = (TextView)FindViewById(Resource.Id.textviewinfo);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            textviewinfo.SetTypeface(tf, TypefaceStyle.Bold);


            tev1 = (TextView)FindViewById(Resource.Id.tev1);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            tev1.SetTypeface(tf, TypefaceStyle.Bold);


            captureimage = (ImageView)FindViewById(Resource.Id.capture_image);
            uploadimage = (ImageView)FindViewById(Resource.Id.upload_img);
            uploadimage.Click += delegate {
                try
                {
                    UploadPhoto();
                }
                catch (Exception ex)
                {

                    Toast.MakeText(this, "Please select any image to represent the issue" + ex, ToastLength.Long).Show();
                }

            };

            captureimage.Click += delegate {
                try
                {
                    TakePhoto();
                }
                catch (Exception ex)
                {

                    Toast.MakeText(this, "Please select any image to represent the issue"+ex, ToastLength.Long).Show();
                }
            };

            iconSettngs = (ImageView)FindViewById(Resource.Id.iconSettings);
            iconSettngs.Click += IconSettngs_Click;
            iconMap = (ImageView)FindViewById(Resource.Id.iconMap);
            iconMap.Click += IconMap_Click;
            iconNotifications = (ImageView)FindViewById(Resource.Id.iconNotifications);
            iconNotifications.Click += IconNotifications_Click;
            iconFunds = (ImageView)FindViewById(Resource.Id.iconFunds);
            iconFunds.Click += IconFunds_Click;
            iconHome = (ImageView)FindViewById(Resource.Id.iconHome);
            iconHome.Click += IconHome_Click;

        }

        private void IconHome_Click(object sender, EventArgs e)
        {
            var i = new Intent(this, typeof(HomeActivity));
            this.StartActivity(i);
        }

        private void IconFunds_Click(object sender, EventArgs e)
        {
            var i = new Intent(this, typeof(FundsActivity));
            this.StartActivity(i);
        }

        private void IconNotifications_Click(object sender, EventArgs e)
        {
            var i = new Intent(this, typeof(NotificationsActivity));
            this.StartActivity(i);
        }

        private void IconMap_Click(object sender, EventArgs e)
        {
            var i = new Intent(this, typeof(MapActivity));
            this.StartActivity(i);
        }

        private void IconSettngs_Click(object sender, EventArgs e)
        {
            var i = new Intent(this, typeof(SettingsActivity));
            this.StartActivity(i);
        }

        async void TakePhoto()
        {
           
            try
            {
                await CrossMedia.Current.Initialize();
                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                    CompressionQuality = 100,
                    Name = "myimage.jpg",
                    Directory = "sample"
                });
                byte[] imageArray = System.IO.File.ReadAllBytes(file.Path);
                Bitmap bitmap = BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);
                base64image = Convert.ToBase64String(imageArray);
                if (file == null)
                {
                    return;
                }

                var i = new Intent(this, typeof(createissue2));
                i.PutExtra("issueimage", JsonConvert.SerializeObject(base64image));
                this.StartActivity(i);
            }
            catch(Exception ex)
            { Toast.MakeText(this, "Please select any image to represent the issue"+ex, ToastLength.Long).Show(); }
           
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
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


                var i = new Intent(this, typeof(createissue2));
                i.PutExtra("issueimage", JsonConvert.SerializeObject(base64image));
                this.StartActivity(i);

            }
            catch (Exception ex)
            { Toast.MakeText(this, "Please select any image to represent the issue" + ex, ToastLength.Long).Show(); }




        }
    }
}