using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_PU
{
    internal class AdsData
    {
        public string Adsimage { get; set; }
        public string Adstitle { get; set; }
        public string Adstext { get; set; }
        public string websitelink { get; set; }
        public int budget { get; internal set; }

        public string Status { get; set; }
        public int User_id { get; set; }

        public DateTime Date { get; set; }
        public int Elapsed_Days { get; set; }

        public int Amount { get; set; }


        public static async Task<List<AdsData>>  GetAllAds()
        {
            HttpClient client = new HttpClient();
            var uri = Control.Account.BaseAddressUri + "/api/issue/fetchallads";
            var response = await client.GetStringAsync(uri);
            var list = JsonConvert.DeserializeObject<List<AdsData>>(response);
            return list;
        }

        public async Task<bool> StoreAd(AdsData data)
        {
            var uri = Control.Account.BaseAddressUri + "/api/issue/storead";
            HttpClient client = new HttpClient();
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(uri, content);

            if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                return true;
            }
            return false;
        }
    }
}