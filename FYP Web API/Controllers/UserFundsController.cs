using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FYP_Web_API.Models;

namespace FYP_Web_API.Controllers
{
    public class UserFundsController : ApiController
    {

        problemupdatedbEntities dbe = new problemupdatedbEntities();

        [HttpGet]
        [ActionName("getfundedissues")]
        public HttpResponseMessage getfundedissues()
        {
            using (dbe)
            {
                var data = dbe.user_table.Join
                (
                dbe.issue_table,
                user_table => user_table.user_id,
                issue_table => issue_table.user_table.user_id,
                (user_table, issue_table) => new
                {
                    status = issue_table.Status,
                    issueStatement = issue_table.issueStatement,
                    estimated_cost = issue_table.estimated_cost,
                    issue_type = issue_table.issueType,
                    amount_collected = issue_table.amount_collected,
                    IssueImage = issue_table.IssueImage,
                    userName = user_table.name,
                    userImage = user_table.profile_pic,
                    issue_id = issue_table.issue_id,
                    issueDate = issue_table.IssueDate,
                    isResolved = issue_table.isresolved,
                    isworkingstarted = issue_table.isWorkingStarted,
                    issueLatitude=issue_table.locationLatitude,
                    issueLongitude=issue_table.locationLongitude


                }).Where(x => x.estimated_cost != 0 && x.status == "verified").ToList();
                if (data != null)
                {
                    return Request.CreateResponse(HttpStatusCode.Accepted, data);
                }
                return Request.CreateResponse(HttpStatusCode.NotFound, "No Issues have been opened for funding");
            }

        }
        
        [HttpGet]
        [ActionName("gettopcontributer")]
        public HttpResponseMessage gettopcontributer(int issueid)
        {
            var UserId = dbe.funds_table.Where(x => x.issue_id == issueid).OrderByDescending(x => x.amount).FirstOrDefault();
            if (UserId != null)
            {
                var TopContributerObject = dbe.user_table.Where(x => x.user_id == UserId.user_id);
                return Request.CreateResponse(HttpStatusCode.Found, TopContributerObject);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "No Contributer Yet");
        }


        [HttpGet]
        [ActionName("getissuebyid")]
        public HttpResponseMessage getissuebyid(int id)
        {
            var Issue = dbe.issue_table.Where(x => x.issue_id == id);
            if(Issue!=null)
            {
                return Request.CreateResponse(HttpStatusCode.Accepted, Issue);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Record not found");
        }

        [HttpGet]
        [ActionName("getuserbyid")]
        public HttpResponseMessage getuserbyid(int id)
        {
            var user = dbe.user_table.Where(x => x.user_id == id);
            if(user!=null)
            {
                return Request.CreateResponse(HttpStatusCode.Accepted, user);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "User not found");
        }



        //To do work in guidance with funds working.txt ( On Desktop)
        //view button api
        //status api
        //contribute work
        //easypaisa ko mail krna payment integration k liye
        //to checkout ki api check krni hey



    }
}