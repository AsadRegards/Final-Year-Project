using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FinalProject_PU.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace FinalProject_PU.Control
{
    class IssueController
    {
        static string baseuri = Account.BaseAddressUri;
        public static async Task<bool> PostIssue<T>(object obj,Android.Content.Context acc) where T:Model.Issue
        {
            if(UserInfoHolder.Status=="blocked")
            {
                Toast.MakeText(acc, "You have been blocked from problem update and cannot post any issue", ToastLength.Long).Show();
                Intent i = new Intent(acc, typeof(FragmentHomeActivity));
                acc.StartActivity(i);
                return false;
            }
            var issueObj = (T)obj;
            issueObj.isresolved = 0;
            
            HttpClient client = new HttpClient();
            var uri = baseuri + "/api/issue/postnewissue";
            var json = JsonConvert.SerializeObject(obj);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(uri, content);

            if(response.StatusCode==System.Net.HttpStatusCode.Accepted)
            {
                Intent i = new Intent(acc, typeof(FragmentHomeActivity));
              
                
                DataOper.SendNotification(new Model.Notification() { notification_title="Issue Alert", notification_text=issueObj.issueStatement, notification_image=issueObj.IssueImage});
                acc.StartActivity(i);
                
                return true;

            }
            return false;
        }


        public static async Task<List<Helper.Data>> FetchPostList()
        {
         
                HttpClient client = new HttpClient();
                var uri = baseuri + "/api/issue/fetchallissues";
                var response = await client.GetStringAsync(uri);
                var list = JsonConvert.DeserializeObject<List<Helper.Data>>(response);
                return list; 
        }

        public async Task<bool> reportanissue(Report report)
        {
            HttpClient client = new HttpClient();
            var uri = baseuri + "/api/issue/reportanissue";
            var json = JsonConvert.SerializeObject(report);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(uri, content);
            if(response.StatusCode==System.Net.HttpStatusCode.Accepted)
            {
                return true;
            }
            return false;



        }


    }
}