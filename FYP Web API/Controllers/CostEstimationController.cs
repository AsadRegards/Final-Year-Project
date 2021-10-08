using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace FYP_Web_API.Controllers
{
    public class CostEstimationController : ApiController
    {
        public async Task<int> EstimateCost(string base64string)
        {
            var uri = "https://us-central1-river-interface-323808.cloudfunctions.net/function-1";
            HttpClient client = new HttpClient();
            var request = new request() { base64image = base64string };
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(uri, content);
            if(response.Content!=null)
            {
                var result = JsonConvert.DeserializeObject<int>(response.Content.ToString());
                var cost = GetCost(result);
                return cost;
            }
            return 0;
            
        }

        public int GetCost(int index)
        {
            var CostIndex = new int[] { 2000,1500,3000,2000,4000,2500,5000,3500,6000,4000  };
            if(index>=CostIndex.Length)
            {
                return 0;
            }
            return CostIndex[index];
        }
    }

    public class request
    {
       public string base64image { get; set; }
    }
}