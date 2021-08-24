using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using FinalProject_PU.Control;
using FinalProject_PU.Model;
using MohammedAlaa.GifLoading;
using Newtonsoft.Json;
using System;
using Xamarin.Essentials;
using System.Threading.Tasks;

namespace FinalProject_PU
{
    [Activity(Label = "SignUpFinal",NoHistory =true)]
    public class SignUpFinal : Activity
    {
        ImageView imgGo;
        LoadingView loadingView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.SignupFinal);

            imgGo = (ImageView)FindViewById(Resource.Id.imgGO);
            imgGo.Click += ImgGo_Click;
            loadingView = FindViewById<LoadingView>(Resource.Id.loading_view);

        }

        private  async void ImgGo_Click(object sender, EventArgs e)
        {
            loadingView.Visibility = Android.Views.ViewStates.Visible;
            imgGo.Enabled = false;
            var User = JsonConvert.DeserializeObject<User>(Intent.GetStringExtra("userObj"));
            User.Status = "active";
            await Task.Run(() => 
            {
                try
                {
                    Account.UserSignup(User, this);
                }
                catch (Exception)
                {

                }
                finally
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        loadingView.Visibility = Android.Views.ViewStates.Gone;
                        imgGo.Enabled = true;
                    });
                   
                }
            });
           


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
    }
}