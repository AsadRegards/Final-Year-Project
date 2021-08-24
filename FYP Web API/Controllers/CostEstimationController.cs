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
        //[HttpGet]
        //[ActionName("estimateCost")]
        
        //public HttpResponseMessage estimateCost([FromBody] string base64image)
        //{
           

        //}

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
}