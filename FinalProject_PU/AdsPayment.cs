using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace FinalProject_PU
{
    [Activity(Label = "AdsPayment",NoHistory =true)]
    public class AdsPayment : Activity
    {
        Button submit;
        ImageView easypaisa, jazzcash, bankaccount, creditcard, back, close;
        RadioButton radioeasy, radiojazz, radiobank, radiocard;
        TextView texthead, texteasy, textjazz, textbank, textcard;
        Typeface tf;
        Model.PaymentInfo paymentInfo;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.AdsPayment);

            texthead = FindViewById<TextView>(Resource.Id.imgTextContribute);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            texthead.SetTypeface(tf, TypefaceStyle.Bold);

            texteasy = FindViewById<TextView>(Resource.Id.TextEasyPaisa);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            texteasy.SetTypeface(tf, TypefaceStyle.Bold);

            textjazz = FindViewById<TextView>(Resource.Id.TextJazzCash);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            textjazz.SetTypeface(tf, TypefaceStyle.Bold);

            textbank = FindViewById<TextView>(Resource.Id.TextBank);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            textbank.SetTypeface(tf, TypefaceStyle.Bold);

            textcard = FindViewById<TextView>(Resource.Id.TextCard);
            tf = Typeface.CreateFromAsset(Assets, "Quicksand-Bold.otf");
            textcard.SetTypeface(tf, TypefaceStyle.Bold);

            easypaisa = FindViewById<ImageView>(Resource.Id.imgEasyPaisa);
            easypaisa.Click += Easypaisa_Click;

            submit = FindViewById<Button>(Resource.Id.button1);
            submit.Click += Submit_Click;

            jazzcash = FindViewById<ImageView>(Resource.Id.imgJazzCash);
            jazzcash.Click += Jazzcash_Click;

            bankaccount = FindViewById<ImageView>(Resource.Id.imgBankTransfer);
            bankaccount.Click += Bankaccount_Click;

            creditcard = FindViewById<ImageView>(Resource.Id.CreditCard);
            creditcard.Click += Creditcard_Click;

            back = FindViewById<ImageView>(Resource.Id.imggoback);
            back.Click += Back_Click;

            close = FindViewById<ImageView>(Resource.Id.close);
            close.Click += Close_Click;

            radiobank = FindViewById<RadioButton>(Resource.Id.Radiobank);
            radiobank.Click += Radiobank_Click;

            radiocard = FindViewById<RadioButton>(Resource.Id.Radiocard);
            radiocard.Click += Radiocard_Click;

            radioeasy = FindViewById<RadioButton>(Resource.Id.RadioEasypaisa);
            radioeasy.Click += Radioeasy_Click;

            radiojazz = FindViewById<RadioButton>(Resource.Id.RadioJazzcash);
            radiojazz.Click += Radiojazz_Click;

            Task.Run(async () =>
            {
                paymentInfo = await new Model.PaymentInfo().getInfoFromDB();
            });


        }

        private async void Submit_Click(object sender, EventArgs e)
        {
            var data = JsonConvert.DeserializeObject<AdsData>(Intent.GetStringExtra("adobject"));
            if (radiobank.Checked)
            {
                Intent i = new Intent(this, typeof(PayByCard));
                i.PutExtra("adobject", JsonConvert.SerializeObject(data));
                StartActivity(i);

            }
            else
            {
                
                if (await data.StoreAd(data))
                {
                    Toast.MakeText(this, "Your ad will be posted after admin approval!", ToastLength.Long).Show();
                    Intent i = new Intent(this, typeof(FragmentHomeActivity));
                    StartActivity(i);
                }
                else
                {
                    Toast.MakeText(this, "Your ad is not posted", ToastLength.Long).Show();
                    Intent i = new Intent(this, typeof(FragmentHomeActivity));
                    StartActivity(i);
                }
            }
            
        }


        private void Radiojazz_Click(object sender, EventArgs e)
        {
            jazzcash.PerformClick();
        }

        private void Radioeasy_Click(object sender, EventArgs e)
        {
            easypaisa.PerformClick();
          
        }

        private void Radiocard_Click(object sender, EventArgs e)
        {
            creditcard.PerformClick();

        }

        private void Radiobank_Click(object sender, EventArgs e)
        {
            bankaccount.PerformClick();
            
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.OnBackPressed();
            this.Finish();
        }

        private void Back_Click(object sender, EventArgs e)
        {
            this.OnBackPressed();
            this.Finish();
        }

        private void Creditcard_Click(object sender, EventArgs e)
        {
            radiocard.Checked = true;
            radiobank.Checked = false;
            radioeasy.Checked = false;
            radiojazz.Checked = false;
        }

        private void Bankaccount_Click(object sender, EventArgs e)
        {
            radiocard.Checked = false;
            radiobank.Checked = true;
            radioeasy.Checked = false;
            radiojazz.Checked = false;
            try
            {
                Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                AlertDialog alert = dialog.Create();
                alert.SetTitle("Bank Account Information");
                alert.SetMessage("Please Transfer your Amount to the Given Bank Account" +
                                "\nAccount Number:" + paymentInfo.bankaccountnumber +
                                "\n" + "Account Title:" + paymentInfo.bankaccounttitle +
                                "\nPlease give this code Number in Refrence:" +
                                Control.UserInfoHolder.User_id.ToString() +
                                Control.UserInfoHolder.currentIssueContext.ToString());
                alert.SetButton("OK", delegate
                {
                    alert.Dispose();
                });
                alert.Show();
            }
            catch (Exception)
            {


            }
        }

        private void Jazzcash_Click(object sender, EventArgs e)
        {
            radiocard.Checked = false;
            radiobank.Checked = false;
            radioeasy.Checked = false;
            radiojazz.Checked = true;

            try
            {
                Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                AlertDialog alert = dialog.Create();
                alert.SetTitle("JazzCash Account Information");
                alert.SetMessage("Please Transfer your Funds to the JazzCash Account" +
                                 "\nAccount Number:" + paymentInfo.jazzcashnumber +
                                 "\n" + "Account Title:" + paymentInfo.jazzcashtitle +
                                 "\nPlease give this code Number in Refrence:" +
                                 Control.UserInfoHolder.User_id.ToString() +
                                 Control.UserInfoHolder.currentIssueContext.ToString());
                alert.SetButton("OK", delegate
                {
                    alert.Dispose();
                });
                alert.Show();
            }
            catch (Exception)
            {


            }
        }

        private void Easypaisa_Click(object sender, EventArgs e)
        {
            radiocard.Checked = false;
            radiobank.Checked = false;
            radioeasy.Checked = true;
            radiojazz.Checked = false;

            try
            {
                Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                AlertDialog alert = dialog.Create();
                alert.SetTitle("Easypaisa Account Information");
                alert.SetMessage("Please Transfer your Funds to the Easypaisa Account" +
                                "\nAccount Number:" + paymentInfo.easypaisanumber +
                                "\n" + "Account Title:" + paymentInfo.easypaisatitle +
                                "\nPlease give this code Number in Refrence:" +
                                Control.UserInfoHolder.User_id.ToString() +
                                Control.UserInfoHolder.currentIssueContext.ToString());
                alert.SetButton("OK", delegate
                {
                    alert.Dispose();
                });
                alert.Show();
            }
            catch (Exception)
            {


            }
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


    }
}