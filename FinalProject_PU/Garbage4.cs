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
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_PU
{

    [Activity(Label = "Garbage4",NoHistory =true)]
    public class Garbage4 : Activity
    {
        static string selected;
        ImageView create_issue4_back, create_issue4_btnnext, iconSettngs, iconMap, iconNotifications, iconFunds, iconHome, circleimageview6, crt_issue_6_radio1, crt_issue_6_radio2, crt_issue_6_radio3;
        RadioButton create_issue6_radiobtn1, create_issue6_radiobtn2, create_issue6_radiobtn3;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.createissue6);
            create_issue4_btnnext = (ImageView)FindViewById(Resource.Id.create_issue4_btnnext);
            create_issue4_btnnext.Click += Create_issue4_btnnext_Click;

            circleimageview6 = (ImageView)FindViewById(Resource.Id.circleImageView6);
            crt_issue_6_radio1 = (ImageView)FindViewById(Resource.Id.crt_issue_6_radio1);
            crt_issue_6_radio1.Click += Crt_issue_6_radio1_Click;
            crt_issue_6_radio2 = (ImageView)FindViewById(Resource.Id.crt_issue_6_radio2);
            crt_issue_6_radio2.Click += Crt_issue_6_radio2_Click;
            crt_issue_6_radio3 = (ImageView)FindViewById(Resource.Id.crt_issue_6_radio3);
            crt_issue_6_radio3.Click += Crt_issue_6_radio3_Click;


            create_issue6_radiobtn1 = (RadioButton)FindViewById(Resource.Id.create_issue6_radiobtn1);
            create_issue6_radiobtn2 = (RadioButton)FindViewById(Resource.Id.create_issue6_radiobtn2);
            create_issue6_radiobtn3 = (RadioButton)FindViewById(Resource.Id.create_issue6_radiobtn3);


        }

        private void Crt_issue_6_radio3_Click(object sender, EventArgs e)
        {
            selected = "middle";
            create_issue6_radiobtn1.Checked = true;
            create_issue6_radiobtn2.Checked = false;
            create_issue6_radiobtn3.Checked = false;
        }

        private void Crt_issue_6_radio2_Click(object sender, EventArgs e)
        {
            selected = "leftslowtrack";
            create_issue6_radiobtn1.Checked = false;
            create_issue6_radiobtn2.Checked = true;
            create_issue6_radiobtn3.Checked = false;
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

        private void Crt_issue_6_radio1_Click(object sender, EventArgs e)
        {
            selected = "rightfasttrack";
            create_issue6_radiobtn1.Checked = false;
            create_issue6_radiobtn2.Checked = false;
            create_issue6_radiobtn3.Checked = true;
        }

        private async void Create_issue4_btnnext_Click(object sender, EventArgs e)
        {
            if (selected != "")
            {


                await Task.Run(() =>
                {
                    var gar = JsonConvert.DeserializeObject<Model.Garbage>("objtopass");
                    gar.issuePositionwrtRoad = selected;
                    Control.DataOper.PutData<Issuelocationpickup_Garbage>(this, gar);


                });
            }
            else
            {
                Toast.MakeText(this, "Please select any one from them", ToastLength.Long).Show();
            }
        }

      
        private void IconHome_Click(object sender, EventArgs e)
        {
            var i = new Intent(this, typeof(HomeActivity));
            this.StartActivity(i);
        }

        private void IconFunds_Click(object sender, EventArgs e)
        {
            var i = new Intent(this, typeof(FundsActivity));
            this.StartActivity(i);
        }

        private void IconNotifications_Click(object sender, EventArgs e)
        {
            var i = new Intent(this, typeof(NotificationsActivity));
            this.StartActivity(i);
        }

        private void IconMap_Click(object sender, EventArgs e)
        {
            var i = new Intent(this, typeof(MapActivity));
            this.StartActivity(i);
        }

        private void IconSettngs_Click(object sender, EventArgs e)
        {
            var i = new Intent(this, typeof(SettingsActivity));
            this.StartActivity(i);
        }

    }
}