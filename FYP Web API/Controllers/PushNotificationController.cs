using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using FYP_Web_API.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PushSharp.Core;
using PushSharp.Google;

namespace FYP_Web_API.Controllers
{
    public class PushNotificationController : ApiController
    {
        problemupdatedbEntities dbe = new problemupdatedbEntities();

        [HttpPost]
        [ActionName("savetoken")]
        public HttpResponseMessage SaveToken([FromUri] string Token, [FromUri] int userId)
        {
            var oldToken = dbe.FCM_TOKEN.Where(x => x.UserID == userId).FirstOrDefault();
            //Deleting old Token
            if(oldToken!=null)
            {
                dbe.FCM_TOKEN.Remove(oldToken);
                dbe.SaveChanges();
            }
            //Adding new token
            FCM_TOKEN token = new FCM_TOKEN();
            token.Token = Token;
            token.UserID = userId;
            dbe.FCM_TOKEN.Add(token);
            dbe.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Accepted, "token saved");
        }


        private List<string> GetAllTokens()
        {
            var TokenListEntity = dbe.FCM_TOKEN.ToList();
            List<string> TokenList = new List<string>();
            foreach (var item in TokenListEntity)
            {
                TokenList.Add(item.Token);

            }
            return TokenList;
        }

        

        [HttpPost]
        [ActionName("sendpushnotification")]
        public IHttpActionResult SendPushNotification([FromBody]notification noti )
        {
            
            var DeviceTokenList = GetAllTokens();

            pushnotification notification = new pushnotification();
            notification.title = noti.notification_title;
            notification.body = noti.notification_text;
            NotificationTable table = new NotificationTable();
            table.notification_image = noti.notification_image;
            table.notification_text = noti.notification_text;
            table.notification_title = noti.notification_title;
            table.notification_date = DateTime.Now;
            dbe.NotificationTable.Add(table);
            dbe.SaveChanges();
            var Json = JsonConvert.SerializeObject(notification);
            SendNotification(DeviceTokenList, Json);
            return Ok();

        }

        [HttpGet]
        [ActionName("getallnotification")]
        public HttpResponseMessage getallNotification()
        {
            var list = dbe.NotificationTable.ToList();
            return Request.CreateResponse(HttpStatusCode.Accepted, list);
        }



        private void SendNotification(List<string> IDs, string Jsondata)
        {
            try
            {
                
                var API_KEY = "AAAA-GB4D7o:APA91bEuSp9nrKHbtF9MMc26yQ86Jyq7736Aoo6-WxEjVx0xngutgjhfJU1gCwkfiMHgObmFlZ2woKnVe57xFT3N3eDKaZsj95X9S2Vntz5d7NWJk97O63gJw_qngjayb_1-mIqDPGZ9"; //Obtained from google developer console 
                var SENDER_ID = "1066770370490"; //Obtained from firebase console

                //configuration
                var config = new GcmConfiguration(SENDER_ID, API_KEY, "com.companyname.finalproject_pu");
                config.GcmUrl = "https://fcm.googleapis.com/fcm/send";
                //create new broker 
                var gcmbroker = new GcmServiceBroker(config);

                //write up events
                gcmbroker.OnNotificationFailed += (notification, aggregatEx) =>
                {
                    aggregatEx.Handle(ex =>
                    {
                        //see what kind of exception it was further diagnose
                        if (ex is GcmNotificationException)
                        {
                            var notificationException = (GcmNotificationException)ex;
                            //deal with failed notification
                            var gcmNotification = notificationException.Notification;
                            var description = notificationException.Description;
                            string desc = $"GCM Notification Failed: ID={gcmNotification.MessageId}, Desc={description}";
                            //lblStatus.Text = desc;

                        }
                        else if (ex is GcmMulticastResultException)
                        {
                            var multicastException = (GcmMulticastResultException)ex;
                            foreach (var succeededNotification in multicastException.Succeeded)
                            {
                                Console.WriteLine($"GCM Notification succeeded ID={succeededNotification.MessageId}");
                            }
                            foreach (var failedKvp in multicastException.Failed)
                            {
                                var n = failedKvp.Key;
                                var e = failedKvp.Value;
                                string desc = $"GCM Notification Failed: ID={n.MessageId}";

                            }
                        }
                        else if (ex is DeviceSubscriptionExpiredException)
                        {
                            var expiredException = (DeviceSubscriptionExpiredException)ex;
                            var oldID = expiredException.OldSubscriptionId;
                            var newID = expiredException.NewSubscriptionId;
                            string desc = $"Device Registration ID Expired:{oldID}";
                            Console.WriteLine(desc);
                            if (!string.IsNullOrWhiteSpace(newID))
                            {
                                string desc2 = $"Device Registration ID changed to:{newID}";
                                // if this value is not null our subscription changed and we should update our database
                                //lblStatus.Text = desc2;
                            }
                        }
                        else if (ex is RetryAfterException)
                        {
                            var retryException = (RetryAfterException)ex;
                            //if you get rate limited you should stop sending messages until after the RetryAfterUtc date
                            string desc = $"GCM rate limited, Dont send more until after {retryException.RetryAfterUtc}";
                           // lblStatus.Text = desc;

                        }
                        else
                        {
                            string desc = "$GCM Notification failed for some unknown reason";
                            //lblStatus.Text = desc;

                        }
                        //mark it as handled
                        return true;
                    });
                };
                gcmbroker.OnNotificationSucceeded += (notification) => { };
                //start the broker 
                gcmbroker.Start();

             
                //queue a notification to send
                gcmbroker.QueueNotification(new GcmNotification
                {
                    RegistrationIds = IDs,
                    Data = JObject.Parse(Jsondata)
                });
                /*
                 * stop the broker wait for it to finish
                 * this is noyt done after every message but you are done with the borker
                 */

                gcmbroker.Stop();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

    public class notification
    {
        public int notification_id { get; set; }
        public string notification_title { get; set; }
        public string notification_text { get; set; }
        public string notification_image { get; set; }
        public DateTime notification_date { get; set; }
    }
    
}




//string server_api_key = ConfigurationManager.AppSettings[API_KEY];
//string sender_id = ConfigurationManager.AppSettings[SENDER_ID];

//WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
//tRequest.Method = "post";
//tRequest.ContentType = "application/json";
//tRequest.Headers.Add($"Authorization: key={server_api_key}");
//tRequest.Headers.Add($"Sender: id={sender_id}");

//tRequest.ContentLength = arr.Length;
//Stream datastream = tRequest.GetRequestStream();
//datastream.Write(arr, 0, arr.Length);
//datastream.Close();

//WebResponse tResponse = tRequest.GetResponse();
//datastream = tResponse.GetResponseStream();
//StreamReader tReader = new StreamReader(datastream);

//String sResponseFromServer = tReader.ReadToEnd();

//tReader.Close();
//datastream.Close();