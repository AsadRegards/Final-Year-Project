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
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_PU.Control
{
    class ImageModeration
    {
        public async Task<string> ModerateImage(string base64Image)
        {
            HttpClient client = new HttpClient();
            var uri = "https://centralindia.api.cognitive.microsoft.com/customvision/v3.0/Prediction/f32e1748-fa6f-447a-a554-6abaa22c5a1b/classify/iterations/Issue%20Images%20Model/image";
            client.DefaultRequestHeaders.Add("Prediction-Key", "93289c7ac6654fa7b0290435308174fd");
            var byteArray = Convert.FromBase64String(base64Image);
            var content = new ByteArrayContent(byteArray);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            var response = await client.PostAsync(uri, content);
            var result = await response.Content.ReadAsStringAsync();
            var root = JsonConvert.DeserializeObject<Root>(result);
            var finalPrediction = root.predictions[0].tagName;
            return finalPrediction;
        }

        public async Task<bool> IsValidImage(string base64Image)
        {
            var result = await ModerateImage(base64Image);
            //if(result=="broken wires" || result=="garbage" || result=="debris" || result=="manhole filled" || result=="manhole notfilled" || result=="missing vehicle" || result=="pothole 1 fill" || result=="pothole 1 notfill" || result=="pothole 2 fill" || result=="pothole 2 notfill" || result == "pothole 3 fill" || result == "pothole 3 notfill" || result == "pothole 4 fill" || result == "pothole 4 notfill" || result == "pothole 5 fill" || result == "pothole 5 notfill" || result == "rainwater")
            //{

            //}
            if(result=="unsupported")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}