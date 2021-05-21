using System;
using System.Collections.Generic;

namespace FinalProject_PU.Model
{
    class Issue
    {
        public List<User> NearbyUsers;
        public int issue_id { get; set; }

        public int user_id = Control.UserInfoHolder.User_id;
        public string locationLongitude { get; set; }
        public string locationLatitude { get; set; }
        public string Status { get; set; }
        public DateTime issueDate = DateTime.Now;
        public string location_name { get; set; }
        public string IssueImage { get; set; }
        public int balance_payment { get; set; }

        public int isresolved { get; set; }
        public int amount_collected { get; set; }

        public int isWorkingStarted { get; set; }

        public string issueFlag { get; set; }

        public string issueType { get; set; }
        public string issueStatement
        {
            get; set;
        }

        public DateTime resolveddate { get; set; }





        protected void getIssue()
       {
            //implment web api to get issue from database
       }
    }
}