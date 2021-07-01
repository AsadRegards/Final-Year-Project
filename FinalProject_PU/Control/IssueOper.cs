using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FinalProject_PU.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_PU.Control
{
    class IssueOper
    {
        public static void ProceedToIssueActivity(string selected, Android.Content.Context acc, object obj)
        {
            switch (selected)
            {
                case "pothole":
                    DataOper.PutData<createissue3>(acc, obj);
                    break;

                case "manhole":
                    DataOper.PutData<Manhole1>(acc, obj);
                    break;

                case "debris":
                    DataOper.PutData<Debris>(acc, obj);
                    break;

                case "garbage":
                    DataOper.PutData<Garbage>(acc, obj);
                    break;

                case "planting":
                    DataOper.PutData<PlantingCampaign1>(acc, obj);
                    break;

                case "missingvehicle":
                    DataOper.PutData<MissingVehicle>(acc, obj);
                    break;

                case "rainwater":
                    DataOper.PutData<Rainwater>(acc, obj);
                    break;
                case "brokenpole":
                    DataOper.PutData<BrokenWires>(acc, obj);
                    break;

            }

        }

        public async Task<User> GetUserById(int UserId)
        {
            HttpClient client = new HttpClient();
            var uri = Control.Account.BaseAddressUri + "/api/account/getuserimagebyid/?userid=" + UserId;
            var response = await client.GetStringAsync(uri);
            var user = JsonConvert.DeserializeObject<Model.User>(response);
            return user;

        }

        public async Task<Issue> GetIssueById(int IssueId)
        {
            HttpClient client = new HttpClient();
            var uri = Control.Account.BaseAddressUri + "/api/issue/getissuebyid/?issueid=" + IssueId;
            var response = await client.GetStringAsync(uri);
            var ISSUE = JsonConvert.DeserializeObject<Model.Issue>(response);
            return ISSUE;
        }

    }
}