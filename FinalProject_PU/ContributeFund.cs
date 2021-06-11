using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_PU
{
    [Activity(Label = "ContributeFund")]
    public class ContributeFund : Activity
    {
        ImageView easypaisa, jazzcash, bankaccount, creditcard, back;
        Model.PaymentInfo paymentInfo;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Contribution);

            // Create your application here
            easypaisa = (ImageView)FindViewById(Resource.Id.imgEasyPaisa);
            jazzcash = (ImageView)FindViewById(Resource.Id.imgJazzCash);
            bankaccount = (ImageView)FindViewById(Resource.Id.imgBankTransfer);
            creditcard = (ImageView)FindViewById(Resource.Id.CreditCard);
            back = (ImageView)FindViewById(Resource.Id.close);
            easypaisa.Click += Easypaisa_Click;
            jazzcash.Click += Jazzcash_Click;
            bankaccount.Click += Bankaccount_Click;
            creditcard.Click += Creditcard_Click;
            back.Click += Back_Click;
            Task.Run(async () =>
            {
                paymentInfo = await new Model.PaymentInfo().getInfoFromDB();
            });
            
            


           
        }

        private void Back_Click(object sender, EventArgs e)
        {
            base.OnBackPressed();
        }

        private void Creditcard_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(PayByCard));
            this.StartActivity(intent);
        }

        private void Bankaccount_Click(object sender, EventArgs e)
        {
            try
            {
                Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                AlertDialog alert = dialog.Create();
                alert.SetTitle("Bank Account Information");
                alert.SetMessage("Please Transfer your Funds to the Given Bank Account\nAccount Number:" + paymentInfo.bankaccountnumber + "\n" + "Account Title:" + paymentInfo.bankaccounttitle + "\nPlease give this code Number in Refrence:" + Control.UserInfoHolder.User_id.ToString() + Control.UserInfoHolder.currentIssueContext.ToString());
                alert.SetButton("OK", (c, ev) =>
                {
                    alert.Dismiss();
                });
                alert.Show();
            }
            catch (Exception)
            {

                
            }

        }

        private void Jazzcash_Click(object sender, EventArgs e)
        {
            try
            {
                Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                AlertDialog alert = dialog.Create();
                alert.SetTitle("JazzCash Account Information");
                alert.SetMessage("Please Transfer your Funds to the JazzCash Account\nAccount Number:" + paymentInfo.jazzcashnumber + "\n" + "Account Title:" + paymentInfo.jazzcashtitle + "\nPlease give this code Number in Refrence:" + Control.UserInfoHolder.User_id.ToString() + Control.UserInfoHolder.currentIssueContext.ToString());
                alert.SetButton("OK", (c, ev) =>
                {
                    alert.Dismiss();
                });
                alert.Show();
            }
            catch (Exception)
            {

                
            }

        }

        private void Easypaisa_Click(object sender, EventArgs e)
        {
            try
            {
                Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                AlertDialog alert = dialog.Create();
                alert.SetTitle("Easypaisa Account Information");
                alert.SetMessage("Please Transfer your Funds to the Easypaisa Account\nAccount Number:" + paymentInfo.easypaisanumber + "\n" + "Account Title:" + paymentInfo.easypaisatitle + "\nPlease give this code Number in Refrence:" + Control.UserInfoHolder.User_id.ToString() + Control.UserInfoHolder.currentIssueContext.ToString());
                alert.SetButton("OK", (c, ev) =>
                {
                    alert.Dismiss();
                });
                alert.Show();
            }
            catch (Exception)
            {

                
            }
    
        }
    }
}