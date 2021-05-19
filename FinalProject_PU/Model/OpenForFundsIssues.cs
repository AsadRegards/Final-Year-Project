using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject_PU.Model
{
    public class OpenForFundsIssues
    {

        public DateTime issueDate { get; set; }
        public string userName { get; set; }
        public string issueStatement { get; set; }
        public int estimated_cost { get; set; }
        public string issue_type { get; set; }
        public int amount_collected { get; set; }
        public string status { get; set; }
        public string userImage { get; set; }
        public string IssueImage { get; set; }
        public int issue_id { get; set; }
        public int isResolved { get; set; }
        public int isworkingstarted { get; set; }

        
        
    }
}