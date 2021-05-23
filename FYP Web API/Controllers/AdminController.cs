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
    public class AdminController : ApiController
    {
        problemupdatedbEntities dbe = new problemupdatedbEntities();

        [HttpGet]
        [ActionName("adminlogin")]

        public HttpResponseMessage adminlogin(string name, string password)
        {
            //  var user = db.Logins.Where(x => x.Username == username && x.Password == password).FirstOrDefault();
            admin_table admin = dbe.admin_table.Where(x => x.name == name && x.password == password).FirstOrDefault();
            if (admin == null)
            {

                return Request.CreateResponse(HttpStatusCode.OK, admin);

            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Accepted, admin);
            }
        }

        [HttpGet]
        [ActionName("fetchallusers")]
        public HttpResponseMessage fetchallusers()
        {
            var data = dbe.user_table.ToList();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [HttpGet]
        [ActionName("fetchallissues")]
        public HttpResponseMessage fetchallissues()
        {
            var data = dbe.issue_table.ToList();
            return Request.CreateResponse(HttpStatusCode.Accepted, data);
        }
        [HttpGet]
        [ActionName("fetchallvolunteers")]
        public HttpResponseMessage fetchallvolunteers()
        {
            var data = dbe.Volunteer_table.ToList();
            return Request.CreateResponse(HttpStatusCode.Accepted, data);
        }
        [HttpGet]
        [ActionName("fetchallreports")]
        public HttpResponseMessage fetchallreports()
        {
            var data = dbe.report_table.ToList();
            return Request.CreateResponse(HttpStatusCode.Accepted, data);
        }
        [HttpGet]
        [ActionName("fetchalladvertisments")]
        public HttpResponseMessage fetchalladvertisments()
        {
            var data = dbe.ad_table.ToList();
            return Request.CreateResponse(HttpStatusCode.Accepted, data);
        }
        [HttpGet]
        [ActionName("fetchallfunds")]
        public HttpResponseMessage fetchallfunds()
        {
            using (var context = new problemupdatedbEntities())
            {
                var data = context.issue_table.
                    Join
                    (
                        context.funds_table,
                        issue_table => issue_table.issue_id,
                        funds_table => funds_table.issue_table.issue_id,
                        (issue_table, funds_table) => new
                        {
                            user_id = funds_table.user_id,
                            issue_id = funds_table.issue_id,
                            funds_id = funds_table.funds_id,
                            amount = funds_table.amount,
                            issueStatement = issue_table.issueStatement,
                            Location_name = issue_table.location_name,
                            fund_date = funds_table.fund_date,
                            estimated_cost = issue_table.estimated_cost,
                            //balance_payment = issue_table.balance_payment

                        }).ToList();


                return Request.CreateResponse(HttpStatusCode.Accepted, data);
            }
        }
        [HttpGet]
        [ActionName("fetchallresolvedissues")]
        public HttpResponseMessage fetchallresolvedissues()
        {
            var data = dbe.issue_table.ToList();
            return Request.CreateResponse(HttpStatusCode.Accepted, data);
        }
    //http:192.168.10.13:8044/api/admin/fetchallusers

        [HttpPost]
        [ActionName("addvolunteer")]
        public HttpResponseMessage addvolunteer(Volunteer_table Vti)
        {
            try
            {
                // Your code...
                // Could also be before try if you know the exception occurs in SaveChanges
                dbe.Volunteer_table.Add(Vti);
                dbe.SaveChanges();


            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
                throw;
            }

            return Request.CreateResponse(HttpStatusCode.Accepted, "Volunteer Added Sucessfully");

        }


        [HttpGet]
        [ActionName("getfundbyid")]
        public HttpResponseMessage getfundbyid(int id)
        {
            using (var context = new problemupdatedbEntities())
            {
                var data = context.issue_table.
                    Join
                    (
                        context.funds_table,
                        issue_table => issue_table.issue_id,
                        funds_table => funds_table.issue_table.issue_id,
                        (issue_table, funds_table) => new
                        {
                            user_id = funds_table.user_id,
                            issue_id = funds_table.issue_id,
                            funds_id = funds_table.funds_id,
                            amount = funds_table.amount,
                            issueStatement = issue_table.issueStatement,
                            Location_name = issue_table.location_name,
                            fund_date = funds_table.fund_date,
                            estimated_cost = issue_table.estimated_cost,
                            //balance_payment = issue_table.balance_payment

                        }).Where(x => x.funds_id == id).FirstOrDefault();


                return Request.CreateResponse(HttpStatusCode.Accepted, data);
            }
        }

        [HttpGet]
        [ActionName("getreportsbyid")]
        public HttpResponseMessage getreportsbyid(int issueid)
        {
            var data = dbe.report_table.Where(x => x.issue_id == issueid).FirstOrDefault();
            return Request.CreateResponse(HttpStatusCode.Accepted, data);
        }
    }
    }
