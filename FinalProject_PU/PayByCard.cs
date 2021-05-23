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
    [Activity(Label = "PayByCard")]
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
                if (payment.StripePay(this))
                {
                    Model.funds funds = new Model.funds();
                    funds.amount = int.Parse(Amount.Text);
                    funds.user_id = Control.UserInfoHolder.User_id;
                    funds.issue_id = Control.UserInfoHolder.currentIssueContext;
                    funds.fund_date = DateTime.Now;
                    funds.postnewfund(funds);
                    Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                    AlertDialog alert = dialog.Create();
                    alert.SetTitle("Payment Information");
                    alert.SetMessage("Payment Succesfull");
                    alert.SetButton("OK", (c, ev) =>
                    {
                        alert.Dismiss();
                    });
                    alert.Show();
                }
            }
            }
            
        }
    }


    

