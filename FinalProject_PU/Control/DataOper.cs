using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Xamarin.Essentials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;



namespace FinalProject_PU.Control
{
    class DataOper
    {
        public static void PutData<T>(Android.Content.Context acc , object obj)
        {
            Intent intent = new Intent(acc, typeof(T));
            intent.PutExtra("objtopass", JsonConvert.SerializeObject(obj));
            MainThread.BeginInvokeOnMainThread(() => 
            {
                acc.StartActivity(intent);

            });
            
        }


        public async  static void SendNotification(Model.Notification notif)
        {
            HttpClient client = new HttpClient();
            var uri = Account.BaseAddressUri + "/api/pushnotification/sendpushnotification";
            var json = JsonConvert.SerializeObject(notif);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(uri, null);

        }

        public static void ModerateImage(Android.Content.Context acc, string selected, string predictionResult,string base64image)
        {

            if(selected==predictionResult)
            {
                IssueOper.ProceedToIssueActivity(selected, acc, base64image);
            }   
            else
            {
                Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(acc);
                AlertDialog alert = dialog.Create();
                alert.SetTitle("Please select correct image");
                alert.SetMessage($"You have selected image of type {predictionResult}, please select the appropiate type from the list");
                alert.SetButton("OK", (c, ev) =>
                {
                    alert.Dismiss();
                });
                alert.Show();
            }    
        }
       
        
    }
}