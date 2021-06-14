﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FinalProject_PU
{
    internal class AdsData
    {
        public string Adsimage { get; set; }
        public string Adstitle { get; set; }
        public string Adstext { get; set; }
        public string websitelink { get; set; }

        public static async Task<List<AdsData>>  GetAllAds()
        {
            HttpClient client = new HttpClient();
            var uri = Control.Account.BaseAddressUri + "/api/issue/fetchallads";
            var response = await client.GetStringAsync(uri);
            var list = JsonConvert.DeserializeObject<List<AdsData>>(response);
            return list;
        }
    }
}