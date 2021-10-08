using FYP_Web_API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace FYP_Web_API.Controllers
{
    public class AdminController : ApiController
    {
        problemupdatedbEntities dbe = new problemupdatedbEntities();


        [HttpGet]
        [ActionName("updateuserstatus")]
        public HttpResponseMessage updateuserstatus(int userid)
        {
            var user = dbe.user_table.Where(x => x.user_id == userid).FirstOrDefault();
            if(user!=null)
            {
                if(user.Status=="active")
                {
                    user.Status = "blocked";
                    dbe.SaveChanges();
                }
                else if(user.Status=="blocked")
                {
                    user.Status = "active";
                    dbe.SaveChanges();
                }

                return Request.CreateResponse(HttpStatusCode.Accepted, "");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "");
        }


        [HttpGet]
        [ActionName("adminlogin")]

        public async Task<HttpResponseMessage> adminlogin(string name, string password)
        {
            var originalSynchronizationContext = SynchronizationContext.Current;
            try 
            {
                SynchronizationContext.SetSynchronizationContext(null);
                await new NearbyUserController().verifybynearbyusers();
            } 
            catch(Exception ex)
            {
                StreamWriter sw = new StreamWriter("D:\\WORK\\abcerror.txt");
                sw.WriteLine("EXCEPTION::" + ex.ToString());
                sw.Close();
            }
            finally
            {
                SynchronizationContext.SetSynchronizationContext(originalSynchronizationContext);
            }
            
         
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
        [ActionName("updateworkingstatus")]
        public HttpResponseMessage updateworkingstatus(int issueid, string status)
        {
            var issue = dbe.issue_table.Where(x => x.issue_id == issueid).FirstOrDefault();
            if(issue!=null)
            {
                issue.WorkingStatus = status;
                if(status=="Working")
                {
                    issue.isWorkingStarted = 1;
                    issue.isresolved = 0;
                }
                if(status=="Resolved")
                {
                    issue.isresolved = 1;
                }
                if(status=="Not Working")
                {
                    issue.isWorkingStarted = 0;
                    issue.isresolved = 0;
                }
                if(status=="Conflict")
                {
                    issue.isresolved = 0;
                }
                dbe.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.Accepted, "");
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
        [ActionName("updateadstatus")]
        public HttpResponseMessage updateadstatus(string status, int ad_id, int daystorun)
        {
            var ad = dbe.ad_table.Where(x => x.advertisment_id == ad_id).FirstOrDefault();
            if(ad!=null)
            {
                ad.Status = status;
                ad.Elapsed_Days = daystorun;
                dbe.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Accepted, "Status Updated");

            }
            return Request.CreateResponse(HttpStatusCode.OK, "Ad record not found");


        }

        [HttpGet]
        [ActionName("sendmessagetoadvertiser")]
        public HttpResponseMessage sendmessagetoadvertiser(int userid, string message)
        {
            var user = dbe.user_table.Where(x => x.user_id == userid).FirstOrDefault();
            if(user!=null)
            {
                SmtpClient client = new SmtpClient();
                MailMessage msg = new MailMessage();
                string email_id = user.email_address;
                msg.From = new MailAddress("problemupdatepu@gmail.com");
                msg.To.Add(email_id);
                msg.Subject = "Reply to your recent Advertisment posting";
                msg.Body = string.Format(message);
                msg.IsBodyHtml = true;
                client.Credentials = new NetworkCredential("problemupdatepu@gmail.com", "6302762985");
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                client.Send(msg);
                return Request.CreateResponse(HttpStatusCode.Accepted, "");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "");
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
            var data = dbe.report_table.Where(x => x.issue_id == issueid).ToList();
            return Request.CreateResponse(HttpStatusCode.Accepted, data);
        }

        [HttpPost]
        [ActionName("sendfeedback")]
        public HttpResponseMessage sendfeedback([FromBody] Feedback feedback)
        {
            dbe.Feedback.Add(feedback);
            dbe.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Accepted, "Saved!");
        }

        [HttpGet]
        [ActionName("getallfeedback")]
        public HttpResponseMessage getallfeedback()
        {
            var FeedbackList = dbe.Feedback.ToList();

            return Request.CreateResponse(HttpStatusCode.Accepted, FeedbackList);
        }
    }
    }
