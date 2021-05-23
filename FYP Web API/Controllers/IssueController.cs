using FYP_Web_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity.Validation;
using System.IO;

namespace FYP_Web_API.Controllers
{
    public class IssueController : ApiController
    {
        problemupdatedbEntities dbe = new problemupdatedbEntities();


        //Posting New Issue
        [HttpPost]
        [ActionName("postnewissue")]
        public HttpResponseMessage postnewissue(issue_table issue)
        {
            try
            {
                // Your code...
                // Could also be before try if you know the exception occurs in SaveChanges
                dbe.issue_table.Add(issue);
                dbe.SaveChanges();
                

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

            return Request.CreateResponse(HttpStatusCode.Accepted, "Issue Posted Sucessfully");

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

