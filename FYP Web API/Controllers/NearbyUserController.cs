using FYP_Web_API.Models;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace FYP_Web_API.Controllers
{
    public class NearbyUserController : ApiController
    {
        problemupdatedbEntities dbe = new problemupdatedbEntities();
        public async void CalculateNearbyUser(string latitude, string longitude, int issueId)
       {
            double latitude_ = Convert.ToDouble(latitude);
            double longitude_ = Convert.ToDouble(longitude);
            var ICoardinates = new GeoCoordinate(latitude_, longitude_);
            //111 meters=0.001 degrees( of Latitude and Longitude)
            await Task.Run(() =>
            {
                foreach (var item in dbe.user_table.ToList())
                {
                    var UCoardinates = new GeoCoordinate(Convert.ToDouble(item.homelocatlati), Convert.ToDouble(item.homelocatlongi));
                    var distance = ICoardinates.GetDistanceTo(UCoardinates);
                    if (distance < 100)
                    {
                        dbe.nearby_user_table.Add(new nearby_user_table { issue_id = issueId, user_id = item.user_id, Isverified = 0 });
                        dbe.SaveChanges();
                    }

                }

            });
            PushNotificationController controller = new PushNotificationController() 
            {
                Request = new System.Net.Http.HttpRequestMessage() ,
                Configuration=new HttpConfiguration()
            };
            controller.sendverificationnotification(issueId);
           
       }

        [HttpGet]
        [ActionName("verifybysingleuser")]
        public HttpResponseMessage verifybysingleuser(int userid, int issueid)
        {
            var record = dbe.nearby_user_table.Where(x => x.user_id == userid && x.issue_id == issueid).FirstOrDefault();
            if(record!=null)
            {
                record.Isverified = 1;
                dbe.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.Accepted, "");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "");
        }
       
        public void findnearbyusers(int issueid)
        {
            var Issue = dbe.issue_table.Where(x => x.issue_id == issueid).FirstOrDefault();
            CalculateNearbyUser(Issue.locationLatitude, Issue.locationLongitude, issueid);
        }

        [HttpGet]
        [ActionName("getnearbyusersbyid")]
        public HttpResponseMessage getnearbyusersbyid(int IssueId)
        {
            var list = dbe.nearby_user_table.Where(x => x.issue_id == IssueId).ToList();
            if (list.Count() != 0)
            {
                return Request.CreateResponse(HttpStatusCode.Accepted, list);
            }

            return Request.CreateResponse(HttpStatusCode.NotFound, "No Users");
            
        }

      
        public async Task verifybynearbyusers()
        {
            await Task.Run(() =>
            {
                var list = dbe.issue_table.ToList();
                foreach (var issue in list)
                {
                    var nearbyUsersList = dbe.nearby_user_table.Where(x => x.issue_id == issue.issue_id && x.Isverified == 1).ToList();
                    if (nearbyUsersList.Count >= 5)
                    {
                        issue.Status = "verified";
                        dbe.SaveChanges();
                    }

                }
            });

        }

    }
    
}