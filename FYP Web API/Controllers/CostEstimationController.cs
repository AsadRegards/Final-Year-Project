using Keras.Applications;
using Keras.Models;
using Keras.PreProcessing.Image;
using Numpy;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Web.Http;

namespace FYP_Web_API.Controllers
{
    public class CostEstimationController : ApiController
    {
        [HttpGet]
        [ActionName("estimateCost")]
        
        public HttpResponseMessage estimateCost([FromBody] string base64image)
       {
            using (var client = new WebClient())
            {
                client.DownloadFile("https://drive.google.com/uc?export=download&id=1ATIf7wAEOP0tOkxAO5x48pgl2j3H8wkB", "Model.h5");
            }
            var Model = Sequential.LoadModel("Model.h5");


            string filePath = "MyImage.jpg";
            File.WriteAllBytes(filePath, Convert.FromBase64String(base64image));
            var model = new Xception();
            string img_path = "MyImage.jpg";
            var img = ImageUtil.LoadImg(img_path, target_size: (128, 128));
            var x = ImageUtil.ImageToArray(img);
            x = np.expand_dims(x, axis: 0);
            x = model.PreprocessInput(x);
            var preds = Model.Predict(x);
            var output_index = Numpy.np.argmax(Numpy.np.around(preds));
            CostEstimationController co = new CostEstimationController();
            int Cost = co.GetCost(output_index);
            HttpRequestMessage Request = new HttpRequestMessage();
            return Request.CreateResponse(HttpStatusCode.Accepted, Cost);

        }

        public int GetCost(int index)
        {
            var CostIndex = new int[] { 2000,1500,3000,2000,4000,2500,5000,3500,6000,4000  };
            return CostIndex[index];
        }
    }
}