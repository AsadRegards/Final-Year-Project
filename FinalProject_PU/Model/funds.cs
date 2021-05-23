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

namespace FinalProject_PU.Model
{
    class funds
    {
        public int issue_id { get; set; }
        public int user_id { get; set; }
        public int amount { get; set; }
        public DateTime fund_date { get; set; }

        public async void postnewfund(funds fund)
        {
            HttpClient client = new HttpClient();
            var uri = Control.Account.BaseAddressUri + "/api/userfunds/postnewfund";
            var json = JsonConvert.SerializeObject(fund);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(uri, content);
            if(response.StatusCode==System.Net.HttpStatusCode.Accepted)
            {
                return;
            }
        }
    }
}