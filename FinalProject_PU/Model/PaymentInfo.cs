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
    class PaymentInfo
    {
        public string easypaisanumber { get; set; }
        public string easypaisatitle { get; set; }
        public string jazzcashnumber { get; set; }
        public string jazzcashtitle { get; set; }
        public string bankaccountnumber { get; set; }
        public string bankaccounttitle { get; set; }

        public async Task<PaymentInfo> getInfoFromDB()
        {
            HttpClient client = new HttpClient();
            var uri = Control.Account.BaseAddressUri + "/api/userfunds/getpaymentinfo";
            var response = await client.GetStringAsync(uri);
            var PaymentInfo = JsonConvert.DeserializeObject<PaymentInfo>(response);
            return PaymentInfo;
        }
        
    }
}