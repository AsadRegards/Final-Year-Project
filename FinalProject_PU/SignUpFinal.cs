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
    [Activity(Label = "SignUpFinal")]
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
    }
}