using FYP_Web_API.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FYP_Web_API.Controllers
{
    public class AccountController : ApiController
    {

        problemupdatedbEntities dbe = new problemupdatedbEntities();
        


       [HttpPost]
       [ActionName("registernewuser")]
       public HttpResponseMessage registernewuser(user_table u)
        {
            dbe.user_table.Add(u);
            dbe.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Accepted, "Signup Sucessfull");

        }

        [HttpGet]
        [ActionName("login")]
        // http://*:8044/api/test/login/email=asadregards@gmail.com&password_hash=123456
        public HttpResponseMessage login(string Email_address, int Password_hash)
        {
            //  var user = db.Logins.Where(x => x.Username == username && x.Password == password).FirstOrDefault();
            user_table user = dbe.user_table.Where(x => x.email_address == Email_address && x.password_hash == Password_hash).FirstOrDefault();
            if (user == null)
            {

                return Request.CreateResponse(HttpStatusCode.OK, user);
                
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Accepted, user);
            }
        }

        [HttpGet]
        [ActionName("validateemail")]
        public HttpResponseMessage validateemail(string email)
        {
            user_table user = dbe.user_table.Where(x => x.email_address == email).FirstOrDefault();
            if (user != null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
        }


        [HttpPut]
        [ActionName("updatepassword")]
        public HttpResponseMessage updatepassword(int password_hash,string email)
        {
            var response = 0;
            user_table user = dbe.user_table.Where(x => x.email_address == email).FirstOrDefault();
            user.password_hash = password_hash;
            dbe.Entry(user).State = EntityState.Modified;
            response=dbe.SaveChanges();
            string finalresponse = "password updated successfully - " + response;
            return Request.CreateResponse(HttpStatusCode.OK, finalresponse );
        }

        [HttpPut]
        [ActionName("setnewpassword")]
        public HttpResponseMessage setnewpassword(int old_password_hash, int new_password_hash,string email)
        {
            var response = 0;
            user_table user = dbe.user_table.Where(x => x.email_address == email && x.password_hash == old_password_hash).FirstOrDefault();
            if(user!=null)
            {
                user.password_hash = new_password_hash;
                dbe.Entry(user).State = EntityState.Modified;
                response = dbe.SaveChanges();
                return Request.CreateErrorResponse(HttpStatusCode.OK, response.ToString());
            }

            //returns -1 if not found user record.
            return Request.CreateResponse(HttpStatusCode.NotAcceptable, -1); 
        }


        [HttpPut]
        [ActionName("setnewusername")]
        public HttpResponseMessage setnewusername(int old_password_hash, string New_Username, string email)
        {
            var response = 0;
            user_table user = dbe.user_table.Where(x => x.email_address == email && x.password_hash == old_password_hash).FirstOrDefault();
            if (user != null)
            {
                user.name = New_Username; 
                dbe.Entry(user).State = EntityState.Modified;
                response = dbe.SaveChanges();
                return Request.CreateErrorResponse(HttpStatusCode.OK, response.ToString());
            }

            //returns -1 if not found user record.
            return Request.CreateResponse(HttpStatusCode.NotAcceptable, -1);
        }

        [HttpPut]
        [ActionName("setnewphonenumber")]
        public HttpResponseMessage setnewphonenumber(int old_password_hash, string contact_no_new, string email)
        {
            var response = 0;
            user_table user = dbe.user_table.Where(x => x.email_address == email && x.password_hash == old_password_hash).FirstOrDefault();
            if (user != null)
            {
                user.contact_no = contact_no_new;
                dbe.Entry(user).State = EntityState.Modified;
                response = dbe.SaveChanges();
                return Request.CreateErrorResponse(HttpStatusCode.OK, response.ToString());
            }

            //returns -1 if not found user record.
            return Request.CreateResponse(HttpStatusCode.NotAcceptable, -1);
        }

    }
}
