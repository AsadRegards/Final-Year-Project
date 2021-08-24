using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_PU
{
    [Activity(Label = "PayByCard",NoHistory =true)]
    public class PayByCard : Activity,ITextWatcher
    {
        EditText CardNumber, Cvc, ExpDateMonth, ExpDateYear, Amount;
        TextView CardNo, cvv, expDate, amount;
        ImageView PaymentButton;

        bool AllSet;

        public void AfterTextChanged(IEditable s)
        {
           
        }

        public void BeforeTextChanged(ICharSequence s, int start, int count, int after)
        {
           
        }
        long lastPress;
        public override void OnBackPressed()
        {
            // source https://stackoverflow.com/a/27124904/3814729
            long currentTime = DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond;

            // source https://stackoverflow.com/a/14006485/3814729
            if (currentTime - lastPress > 5000)
            {
                Toast.MakeText(this, "Press back again to exit", ToastLength.Long).Show();
                lastPress = currentTime;
            }
            else
            {

                FinishAffinity();

            }
        }
        public void OnTextChanged(ICharSequence s, int start, int before, int count)
        {
            if(CardNumber.Text.Length>16)
            {
                CardNo.SetTextColor(Color.DarkRed);
                AllSet = false;
            }
            else
            {
                CardNo.SetTextColor(Color.Black);
                AllSet = true;
            }

            if(Cvc.Text.Length>3)
            {
                cvv.SetTextColor(Color.DarkRed);
                AllSet = false;
            }
            else
            {
                cvv.SetTextColor(Color.Black);
                AllSet = true;
            }

            if(ExpDateMonth.Text.Count()!=0)
            {
                if(int.Parse(ExpDateMonth.Text)<1 || int.Parse(ExpDateMonth.Text)>12)
                {
                    expDate.SetTextColor(Color.DarkRed);
                    AllSet = false;
                }
                else
                {
                    expDate.SetTextColor(Color.Black);
                    AllSet = true;
                }
            }

            if (ExpDateYear.Text.Count() != 0)
            {
                if (int.Parse(ExpDateYear.Text) < 1 || int.Parse(ExpDateYear.Text) > 12)
                {
                    expDate.SetTextColor(Color.DarkRed);
                    AllSet = false;
                }
                else
                {
                    expDate.SetTextColor(Color.Black);
                    AllSet = true;
                }
            }

        }

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.CardPayment);
            PaymentButton = FindViewById<ImageView>(Resource.Id.paynow);
            PaymentButton.Click += PaymentButton_Click;
            CardNumber = FindViewById<EditText>(Resource.Id.edtCardNo);
            Cvc = FindViewById<EditText>(Resource.Id.edtCvc);
            ExpDateYear = FindViewById<EditText>(Resource.Id.edtExpiryDateYear);
            ExpDateMonth = FindViewById<EditText>(Resource.Id.edtExpiryDateMonth);
            CardNo = FindViewById<TextView>(Resource.Id.txtCardNo);
            cvv = FindViewById<TextView>(Resource.Id.txtCvc);
            expDate = FindViewById<TextView>(Resource.Id.txtExpiryDate);
            Amount = FindViewById<EditText>(Resource.Id.edtAmount);
            amount = FindViewById<TextView>(Resource.Id.txtAmount);
            CardNumber.AddTextChangedListener(this);
            Cvc.AddTextChangedListener(this);
            ExpDateYear.AddTextChangedListener(this);
            ExpDateMonth.AddTextChangedListener(this);
            


           

            

          

        }

        private void PaymentButton_Click(object sender, EventArgs e)
        {
            if(AllSet)
            {
                Control.Payment_Integration payment = new Control.Payment_Integration(CardNo.Text, Cvc.Text, ExpDateYear.Text, ExpDateMonth.Text, Amount.Text);
                Android.App.AlertDialog.Builder dialogue = new AlertDialog.Builder(this);
                AlertDialog alert = dialogue.Create();
                alert.SetTitle("Transaction in process");
                alert.SetMessage("Please wait while your transaction is being processed...");
                alert.Show();
                if (payment.StripePay(this))
                {
                    Model.funds funds = new Model.funds();
                    funds.amount = int.Parse(Amount.Text);
                    funds.user_id = Control.UserInfoHolder.User_id;
                    funds.issue_id = Control.UserInfoHolder.currentIssueContext;
                    funds.fund_date = DateTime.Now;
                    funds.postnewfund(funds);
                    Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                    AlertDialog alert1 = dialog.Create();
                    alert.Dismiss();
                    alert1.SetTitle("Payment Information");
                    alert1.SetMessage("Payment Succesfull");
                    alert1.SetButton("OK", (c, ev) =>
                    {
                        alert1.Dismiss();
                    });
                    alert1.Show();
                }
                else
                {
            
                    Toast.MakeText(this, "Payment Declined", ToastLength.Long).Show();
                    alert.Dismiss();
                }
            }
            }
            
        }
    }


    

