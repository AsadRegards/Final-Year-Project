using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_PU.Model
{
    class Notification
    {
       public string title { get; set; }
        public string message { get; set; }
        public string image { get; set; }

        public async Task<List<Notification>> getallnotification()
        {
            HttpClient client = new HttpClient();
            var uri = Control.Account.BaseAddressUri + "/api/pushnotification/getallnotification";
            var response = await client.GetStringAsync(uri);
            var list = JsonConvert.DeserializeObject<List<Notification>>(response);
            return list;
        }
    }
}