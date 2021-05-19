using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using FinalProject_PU.Model;
using MohammedAlaa.GifLoading;
using Newtonsoft.Json;
using Plugin.Media;
using System;
using System.ComponentModel;
using Xamarin.Essentials;

namespace FinalProject_PU
{
    [Activity(Label = "PhotoUploading")]
    public class PhotoUploading : Activity
    {

        ImageView gallery, newimage, back, go;
        TextView upload;
        Typeface tf;
        LoadingView loader, button_loader;
     
        private static string base64image;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.photoupload);
            loader = FindViewById<LoadingView>(Resource.Id.loading_view);
            button_loader = FindViewById<LoadingView>(Resource.Id.loading_view_button);

    



            upload = (TextView)FindViewById(Resource.Id.tvupload);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            upload.SetTypeface(tf, TypefaceStyle.Bold);

            gallery = (ImageView)FindViewById(Resource.Id.imggallery);
            newimage = (ImageView)FindViewById(Resource.Id.imgaccount);
           
            gallery.Click += Gallery_Click;

            go = (ImageView)FindViewById(Resource.Id.imglogin);
            go.Click += Go_Click;
        }

        

        private void Gallery_Click(object sender, EventArgs e)
        {
            UploadPhoto();

        }
        private void Go_Click(object sender, EventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerAsync();
        }
        
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            var u = JsonConvert.DeserializeObject<User>(Intent.GetStringExtra("userb"));
            u.profile_pic = base64image;
            MainThread.BeginInvokeOnMainThread(() => 
            {
                loader.Visibility = Android.Views.ViewStates.Visible;
                button_loader.Visibility = Android.Views.ViewStates.Visible;
                gallery.Enabled = false;
                go.Enabled = false;
                var i = new Intent(this, typeof(EmailVerifyActivity));
                i.PutExtra("usertoemail", JsonConvert.SerializeObject(u));
                this.StartActivity(i);
            });
            
        }

        public async void UploadPhoto()
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                Toast.MakeText(this, "upload not supported on this device", ToastLength.Short).Show();

            }

            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small,
                CompressionQuality = 40

            });

            //convert file to byte array , to bitmap
            byte[] imageArray = System.IO.File.ReadAllBytes(file.Path);
            base64image = Convert.ToBase64String(imageArray);

            //display photo
            Android.Graphics.Bitmap bitmap = BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);
            newimage.SetImageBitmap(bitmap);




        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}