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
using Stripe;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading;

namespace FinalProject_PU.Control
{
    class Payment_Integration
    {
        string CardNumber;
        string ExpMonth;
        string ExpYear;
        string Amount;
        string Cvc;

        public Payment_Integration(string cardNumber, string cvc, string expmonth, string expyear, string amount)
        {
            CardNumber = cardNumber;
            Cvc = cvc;
            ExpMonth = expmonth;
            ExpYear = expyear;
            Amount = amount;
            


        }


        public bool StripePay(Android.Content.Context acc)
        {
            try
            {
                StripeConfiguration.ApiKey = "sk_test_51If20qKme4ylLpJqCqLr2nw0chcXSm6zi0vCffHptgB8xa1wSUH3qbxCvsSFIHvP6ZLSi1tuXqhFzNApyB4XIEC6001UUZghYc";
                //Step 1: Create Card Options
                TokenCardOptions stripeOptions = new TokenCardOptions();
                stripeOptions.Number = CardNumber;
                stripeOptions.Cvc = Cvc;
                stripeOptions.ExpMonth = int.Parse(ExpMonth);
                stripeOptions.ExpYear = int.Parse(ExpYear);

                //Step 2: Assign Card to a Token
                TokenCreateOptions stripeCard = new TokenCreateOptions();
                stripeCard.Card = stripeOptions;

                TokenService service = new TokenService();
                Token newToken = service.Create(stripeCard);

                //Step 3: Assign Token to the source
                var Options = new SourceCreateOptions
                {
                    Type = SourceType.Card,
                    Currency = "pkr",
                    Token = newToken.Id
                };

                var sourceService = new SourceService();
                var source = sourceService.Create(Options);

                //Step 4: Create Customer Options
                CustomerCreateOptions customerOptions = new CustomerCreateOptions
                {
                    Name = "",
                    Email = "",
                    Address = new AddressOptions { Country = "Pakistan" }
                };

                var customerService = new CustomerService();
                var Customer = customerService.Create(customerOptions);


                //Step 5: Charge Options
                var ChargeOptions = new ChargeCreateOptions
                {
                    Amount = long.Parse(Amount),
                    Currency = "PKR",
                    ReceiptEmail = "asadregards@gmail.com", //user Email will be provided
                    Customer = Customer.Id,
                    Source = source.Id


                };

                //Step 6: Charge the Customer
                var chargeService = new ChargeService();
                Charge charge = chargeService.Create(ChargeOptions);
                if (charge.Status == "succeeded")
                {
                    return true;
                    Toast.MakeText(acc, "Payment successfull", ToastLength.Long).Show();
                }
                else
                {
                    //Payment declined
                    return true;
                    Toast.MakeText(acc, "Payment declined", ToastLength.Long).Show();
                }
            }
            catch (Exception)
            {
                return false;
                Toast.MakeText(acc, "Payment Declined", ToastLength.Long).Show();
            }
   

        }
    }
}