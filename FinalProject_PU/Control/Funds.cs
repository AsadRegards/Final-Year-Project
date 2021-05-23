using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_PU.Control
{
    class Funds
    {
        HttpClient client = new HttpClient();
        public async Task<List<Model.OpenForFundsIssues>> GetOpenFundingIssues()
        {
            string uri = Account.BaseAddressUri + "/api/userfunds/getfundedissues";
            var response = await client.GetStringAsync(uri);
            var IssueList = JsonConvert.DeserializeObject<List<Model.OpenForFundsIssues>>(response);
            return IssueList;

        }

        public  async Task<Model.User> getTopContributer(int IssueId)
        {
            var uri = Account.BaseAddressUri + "/api/userfunds/gettopcontributer/?issueid=" + IssueId;
            var response = await client.GetStringAsync(uri);
            try 
            {
                var User = JsonConvert.DeserializeObject<Model.User>(response);
                return User;
            }
            catch(Exception)
            {
                return null;
            }
            
            
        }
    }
}