using FYP_Web_API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FYP_Web_API.Controllers
{
    public class VolunteerController:ApiController
    {
        problemupdatedbEntities dbe = new problemupdatedbEntities();
        [HttpGet]
        [ActionName("volunteerlogin")]

        public HttpResponseMessage volunteerlogin(string name, string password)
        {
            //  var user = db.Logins.Where(x => x.Username == username && x.Password == password).FirstOrDefault();
            Volunteer_table volunteer = dbe.Volunteer_table.Where(x => x.name == name && x.password == password).FirstOrDefault();
            if (volunteer == null)
            {

                return Request.CreateResponse(HttpStatusCode.OK, "notfound");

            }
            else
            {
                try
                {
                    return Request.CreateResponse(HttpStatusCode.Accepted, volunteer);
                }
                catch(Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                    return null;
                }

                
            }
        }
        [HttpGet]
        [ActionName("fetchallissues")]
        public HttpResponseMessage fetchallissues()
        {
            var data = dbe.issue_table.ToList();
            return Request.CreateResponse(HttpStatusCode.Accepted, data);
        }
        [HttpGet]
        [ActionName("fetchallassignedissues")]
        public HttpResponseMessage fetchallassignedissues()
        {
            problemupdatedbEntities dbContext = new problemupdatedbEntities();
            var entryPoint = (from assignedtable in dbContext.AssignedByAdmin
                              join volunteertable in dbContext.Volunteer_table on assignedtable.volunteer_id equals volunteertable.volunteer_id
                              join issuetable in dbContext.issue_table on assignedtable.issue_id equals issuetable.issue_id
                              select new
                              {
                                 volunteer_id=assignedtable.volunteer_id,
                                  user_id= issuetable.user_id,
                                  issue_id=assignedtable.issue_id,
                                  issueStatement=issuetable.issueStatement,
                                  location_name=issuetable.location_name
                              }).ToList();

            return Request.CreateResponse(HttpStatusCode.Accepted, entryPoint);
        }

        [HttpPut]
        [ActionName("changeusername")]
        public HttpResponseMessage changeusername(string name,string oldname, string password)
        {
            var record=dbe.Volunteer_table.Where(x => x.name == oldname && x.password == password).FirstOrDefault();
            if(record!=null)
            {
                record.name = name;
                dbe.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Accepted);
            }
            return Request.CreateResponse(HttpStatusCode.NotAcceptable);
        }

        [HttpPut]
        [ActionName("changepassword")]
        public HttpResponseMessage changepassword(string name, string newpassword, string password)
        {
            var record = dbe.Volunteer_table.Where(x => x.name == name && x.password == password).FirstOrDefault();
            if (record != null)
            {
                record.password = newpassword;
                dbe.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Accepted);
            }
            return Request.CreateResponse(HttpStatusCode.NotAcceptable);
        }

        [HttpGet]
        [ActionName("GetstatusofIssue")]
        public HttpResponseMessage GetstatusofIssue(int id)
        {
            var issue = dbe.issue_table.Where(x => x.issue_id == id).FirstOrDefault();
            if(issue!=null)
            {
                return Request.CreateResponse(HttpStatusCode.Accepted, issue.Status);
            }
            return Request.CreateResponse(HttpStatusCode.OK, "");
        }

        [HttpGet]
        [ActionName("updatestatusofissue1")]
        public HttpResponseMessage updatestatusofissue1(int id)
        {
            var issue = dbe.issue_table.Where(x => x.issue_id == id).FirstOrDefault();
            if (issue != null)
            {
                issue.Status = "verified";
                dbe.SaveChanges();

                Request.CreateResponse(HttpStatusCode.Accepted, "");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "");

        }


        [HttpGet]
        [ActionName("updatestatusofissue0")]
        public HttpResponseMessage updatestatusofissue0(int id)
        {
            var issue = dbe.issue_table.Where(x => x.issue_id == id).FirstOrDefault();
            if (issue != null)
            {
                issue.Status = "unverified";
                dbe.SaveChanges();

                Request.CreateResponse(HttpStatusCode.Accepted, "");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "");

        }

        [HttpPost]
        [ActionName("writereport")]
        public HttpResponseMessage writereport([FromBody]Volunteer_Report Vr)
        {
            dbe.Volunteer_Report.Add(Vr);
            dbe.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.Accepted, "");
        }

        [HttpPost]
        [ActionName("sendmessage")]
        public HttpResponseMessage sendmessage(string id_name,string message)
        {
            VolunteerAdminMessages m = new VolunteerAdminMessages() { date = DateTime.Now, user_name_id = id_name, message = message };
            dbe.VolunteerAdminMessages.Add(m);
            dbe.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.Accepted, "Message sent successfully");
        }
    }
}