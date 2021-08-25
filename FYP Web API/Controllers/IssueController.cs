using FYP_Web_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity.Validation;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FYP_Web_API.Controllers
{
    public class IssueController : ApiController
    {
        problemupdatedbEntities dbe = new problemupdatedbEntities();


        //Posting New Issue
        [HttpPost]
        [ActionName("postnewissue")]
        public async Task<HttpResponseMessage> postnewissue(issue_table issue)
        {
            try
            {
                // Your code...
                // Could also be before try if you know the exception occurs in SaveChanges
                dbe.issue_table.Add(issue);
                
                dbe.SaveChanges();
                var originalSynchronizationContext = SynchronizationContext.Current;
                try
                {
                    SynchronizationContext.SetSynchronizationContext(null);
                    new NearbyUserController().findnearbyusers(issue.issue_id);
                    
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

               
                
                return Request.CreateResponse(HttpStatusCode.Accepted, "Issue Posted Sucessfully");



            }
            catch(DbEntityValidationException ex)
            {
                StreamWriter sw = new StreamWriter("D:\\WORK\\abcerror.txt");
                foreach (var eve in ex.EntityValidationErrors)
                {
                    sw.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        sw.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                sw.Close();
            }

            return Request.CreateResponse(HttpStatusCode.NotAcceptable, "Issue posting error");

        }


        [HttpGet]
        [ActionName("addfundtoissue")]
        public HttpResponseMessage addfundtoissue(int issueid, int amount, int userid)
        {
            funds_table funds = new funds_table { amount = amount, issue_id = issueid, fund_date = DateTime.Now, user_id = userid };
            dbe.funds_table.Add(funds);
            dbe.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.Accepted, "Saved");
        }

        [HttpGet]
        [ActionName("estimateallcost")]
        public async Task<HttpResponseMessage> estimateallcost()
        {
            var list = dbe.issue_table.Where(x => x.estimated_cost == 0).ToList();
            foreach(var item in list)
            {
                item.estimated_cost= await new CostEstimationController().EstimateCost(item.IssueImage);
                dbe.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.Accepted, "");
        }


        [HttpGet]
        [ActionName("deleteanissue")]
        public HttpResponseMessage deleteanissue(int issueid)
        {
            var issueRecord = dbe.issue_table.Where(x => x.issue_id == issueid).FirstOrDefault();
            if(issueRecord!=null)
            {
                dbe.issue_table.Remove(issueRecord);
                var result = dbe.SaveChanges();
                if(result==1)
                {
                    return Request.CreateResponse(HttpStatusCode.Accepted, "");
                }
                return Request.CreateResponse(HttpStatusCode.OK, "");
                
            }
            return Request.CreateResponse(HttpStatusCode.OK, "");
        }
        [HttpGet]
        [ActionName("fetchallissues")]
        public HttpResponseMessage fetchallissues()
        {
            using (var context = new problemupdatedbEntities())
            {
                var data = context.user_table.
                    Join
                    (
                        context.issue_table,
                        user_table => user_table.user_id,
                        issue_table => issue_table.user_table.user_id,
                        (user_table, issue_table) => new
                        {
                            IssueImage = issue_table.IssueImage,
                            UserImage = user_table.profile_pic,
                            UserName = user_table.name,
                            IssueDate = issue_table.IssueDate,
                            IssueStatement=issue_table.issueStatement,
                            IssueLatitude=issue_table.locationLatitude,
                            IssueLongitude=issue_table.locationLongitude,
                            Issueflag=issue_table.issueFlag,
                            IssueType=issue_table.issueType,
                            estimatedCost=issue_table.estimated_cost,
                            amountCollected=issue_table.amount_collected,
                            isworkingstarted=issue_table.isWorkingStarted,
                            isResolved=issue_table.isresolved,
                            IssueId=issue_table.issue_id,
                            location_name=issue_table.location_name



                        }).ToList();

                return Request.CreateResponse(HttpStatusCode.Accepted, data);
            }
        }

        [HttpPost]
        [ActionName("reportanissue")]
        public HttpResponseMessage reportanissue(report_table report)
        {
            dbe.report_table.Add(report);
            int result=dbe.SaveChanges();
            
            return Request.CreateResponse(HttpStatusCode.Accepted, "accepted");
        }

        [HttpGet]
        [ActionName("getissuebyid")]
        public HttpResponseMessage getissuebyid(int issueid)
        {
            var issue = dbe.issue_table.Where(x => x.issue_id == issueid).FirstOrDefault();
            if(issue!=null)
            {
                return Request.CreateResponse(HttpStatusCode.Accepted, issue);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "issue not found");
        }

        [HttpGet]
        [ActionName("fetchallads")]
        public HttpResponseMessage fetchallads()
        {
            var list = dbe.ad_table.Where(x => x.Status == "approved").ToList();
            if(list!=null)
            {
                list.Reverse();
            }
            return Request.CreateResponse(HttpStatusCode.Accepted, list);
        }

        [HttpPost]
        [ActionName("storead")]
        public HttpResponseMessage storead(ad_table ad)
        {
            dbe.ad_table.Add(ad);
            try
            {
                var result = dbe.SaveChanges();
                if (result != 0)
                {
                    return Request.CreateResponse(HttpStatusCode.Accepted, "success");

                }
            }
            catch (DbEntityValidationException ex)
            {
                StreamWriter sw = new StreamWriter("D:\\WORK\\abcerror.txt");
                foreach (var eve in ex.EntityValidationErrors)
                {
                    sw.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        sw.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                sw.Close();
            }



            return Request.CreateResponse(HttpStatusCode.NotAcceptable, "failed to save ad");

        }


        [HttpGet]
        [ActionName("GetIssuePoints")]
        public HttpResponseMessage GetIssuePoints()
        {
            List<LocationPoints> points = new List<LocationPoints>();
            var redIssueList = dbe.issue_table.Where(x => x.issueFlag == "red").ToList();
            if(redIssueList!=null)
            {
                foreach (var item in redIssueList)
                {
                    points.Add(new LocationPoints { Latitude = item.locationLatitude, Longitude = item.locationLongitude });
                }
                return Request.CreateResponse(HttpStatusCode.Accepted, points);
            }
           
            return Request.CreateResponse(HttpStatusCode.Accepted, points);
            
            

        }

    }
    public class LocationPoints
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}



/*
 *using (var context = new BookStore())
{
    var data = context.Authors
        .Join(
            context.Books,
            author => author.AuthorId,
            book => book.Author.AuthorId,
            (author, book) => new
            {
                BookId = book.BookId,
                AuthorName = author.Name,
                BookTitle = book.Title
            }
        ).ToList();
	
    foreach(var book in data)
    {
        Console.WriteLine("Book Title: {0} \n\t Written by {1}", book.BookTitle, book.AuthorName);
    }
}
 */

