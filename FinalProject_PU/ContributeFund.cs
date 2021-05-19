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

namespace FinalProject_PU
{
    [Activity(Label = "ContributeFund")]
    public class ContributeFund : Activity
    {
        ImageView easypaisa, jazzcash, bankaccount, creditcard, back;
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
           
        }
    }
}