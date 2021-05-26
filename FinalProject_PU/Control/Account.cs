using Android.Content;
using Android.Widget;
using FinalProject_PU.Model;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace FinalProject_PU.Control
{
    class Account
    {
        private static string ResolveIpAddress()
        {
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST  
            Console.WriteLine(hostName);
            // Get the IP  
            var addresslist = Dns.GetHostEntry(hostName).AddressList;
            foreach (var IPAddress in addresslist)
            {
                if (IPAddress.AddressFamily == AddressFamily.InterNetwork)
                {
                    return IPAddress.ToString();
                }
                return string.Empty;
            }

            return string.Empty;
        }


        public static string BaseAddressUri = string.Format("https://fypwebapi20210526122739.azurewebsites.net");

    
        static public int verifyEmail(string emailp,Android.Content.Context acc)
        {
            try
            {
                Random r = new Random();
                SmtpClient client = new SmtpClient();
                MailMessage msg = new MailMessage();
                string email_id = emailp;
                msg.From = new MailAddress("problemupdatepu@gmail.com");
                msg.To.Add(email_id);
                msg.Subject = "Confirm your Email Address";
                var ran = new Random();
                int codets = ran.Next(10001, 99999);
                msg.Body = string.Format("Please enter the code to confirm your email address: {0}", codets);
                msg.IsBodyHtml = true;
                client.Credentials = new NetworkCredential("problemupdatepu@gmail.com", "6302762985");
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                client.Send(msg);
                return codets;
            }
            catch (Exception e)
            {
                Toast.MakeText(acc, "Couldn't Connect to Internet, Please check your connection", ToastLength.Long).Show();
                return 0;
            }
         

               
            
        }

        static public async void UserSignup(User u, Android.Content.Context acc)
        {
            HttpClient client = new HttpClient();

            var json = JsonConvert.SerializeObject(u);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var uri = BaseAddressUri+"/api/account/registernewuser";
            var response = await client.PostAsync(uri, content);
            if (response.StatusCode == HttpStatusCode.Accepted)
            {
                MainThread.BeginInvokeOnMainThread(() => 
                {
                    Toast.MakeText(acc, "Signup Sucessfull\n****Login Now****.", ToastLength.Long).Show();
                    var i = new Intent(acc, typeof(Login));
                    acc.StartActivity(i);
                });
                
            }
        }

       

        static public async Task<User> UserLogin(string email, int password_hash, Android.Content.Context acc)
        {
            string uri = string.Format("{0}/api/account/login/?email_address={1}&password_hash={2}", BaseAddressUri,email, password_hash);
                                              
            HttpClient client = new HttpClient();
            
            var response = await client.GetStringAsync(uri);

            var userObj = JsonConvert.DeserializeObject<User>(response);
            return userObj;
        }

        static public async Task<bool> EmailValidation(string email)
        {
            string uri = BaseAddressUri + "/api/account/validateemail/?email=" + email;
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(uri);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static public async Task<int> forgotton_password_functionality(string email, Android.Content.Context acc)
        {
            
            if (Android.Util.Patterns.EmailAddress.Matcher(email).Matches() == true)
            {
                
                if (!await Account.EmailValidation(email))
                {
                    int codets = verifyEmail(email,acc);
                    

                    var i = new Intent(acc,typeof(OTPVerify));
                    i.PutExtra("codetotest",JsonConvert.SerializeObject(codets));
                    i.PutExtra("email", JsonConvert.SerializeObject(email));
                    acc.StartActivity(i);
                    return codets;



                }
                else
                {
                    Toast.MakeText(acc, "We didn't find any account with this email", ToastLength.Long).Show();
                    return 0;
                }
            }
            else
            {
                Toast.MakeText(acc, "Please enter correct email address!", ToastLength.Long).Show();
                return 0;
            }
        }

        static public void enter_new_password(string email,int codets, EditText otp, Android.Content.Context acc)
        {
            if (otp.Text == codets.ToString())
            {
                MainThread.BeginInvokeOnMainThread(() => {
                    Intent i = new Intent(acc, typeof(Newpassword));
                    i.PutExtra("email", JsonConvert.SerializeObject(email));
                    acc.StartActivity(i);
                });
                
            }
            else
            {
                MainThread.BeginInvokeOnMainThread(() => 
                {
                     Toast.MakeText(acc, "Invalid OTP entered", ToastLength.Long).Show();
                });
               
            }

        }

        static public async void update_new_password(int password_hash,string email, Android.Content.Context acc)
        {
            string uri = BaseAddressUri + "/api/account/updatepassword/?password_hash=" + password_hash + "&email=" + email;
            HttpClient client = new HttpClient();
            var response = await client.PutAsync(uri, null);

            if(response.StatusCode==HttpStatusCode.OK)
            {
                Toast.MakeText(acc, "password changed successfully", ToastLength.Long).Show();
            }
            else
            {
                Toast.MakeText(acc, "Invalid Current Password", ToastLength.Long).Show();
            }

            
        }

        static public async void SetNewPassword(int old_password_hash, string email, int new_password_hash,Android.Content.Context acc)
        {
            string uri = BaseAddressUri + "/api/account/setnewpassword/?old_password_hash=" + old_password_hash + "&new_password_hash=" + new_password_hash + "&email=" + email;
            HttpClient client = new HttpClient();
            var response = await client.PutAsync(uri, null);
            if(response.StatusCode==HttpStatusCode.OK)
            {
                Toast.MakeText(acc, "password changed sucessfully", ToastLength.Long).Show();
            }
        }

        static public async void SetNewUserName(int old_password_hash, string email, string NewUsername,Android.Content.Context acc)
        {
            string uri = BaseAddressUri + "/api/account/setnewusername/?old_password_hash=" + old_password_hash + "&New_Username=" + NewUsername + "&email=" + email;
            HttpClient client = new HttpClient();
            var response = await client.PutAsync(uri, null);
            if(response.StatusCode==HttpStatusCode.OK)
            {
                Toast.MakeText(acc, "Username Updated Successfully", ToastLength.Long).Show();

            }
            else
            {
                Toast.MakeText(acc, "Invalid Current Password", ToastLength.Long).Show();
            }
        }

        static public async void SetNewPhoneNo(int old_password_hash, string email, string newContact, Android.Content.Context acc)
        {
            string uri = BaseAddressUri + "/api/account/setnewphonenumber/?old_password_hash=" + old_password_hash + "&contact_no_new=" + newContact + "&email=" + email;
            HttpClient client = new HttpClient();
            var response = await client.PutAsync(uri, null);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Toast.MakeText(acc, "Phone Number Updated Successfully", ToastLength.Long).Show();

            }
            else
            {
                Toast.MakeText(acc, "Invalid Current Password", ToastLength.Long).Show();
            }
        }


    }
}
