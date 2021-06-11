using System;
using System.Collections.Generic;

namespace FinalProject_PU.Control
{

    public class Prediction
    {
        public string tagId { get; set; }
        public string tagName { get; set; }
        public double probability { get; set; }
    }

    public class Root
    {
        public string id { get; set; }
        public string project { get; set; }
        public string iteration { get; set; }
        public DateTime created { get; set; }
        public List<Prediction> predictions { get; set; }
    }


}