using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using T1807EHelloUWP.Constant;
using T1807EHelloUWP.Entity;

namespace T1807EHelloUWP.Service
{
    class MemberServiceImp : MemberService
    {
       
        public Member GetInformation(string token)
        {
            
            throw new NotImplementedException();
        }

        public string Login(string username, string password)
        {
            try
            {
                //tạo đối tượng member login từ giá trị của form.
                var memberLogin = new MemberLogin()
                {
                    email = username,
                    password = password
                };
                // validate
                if (!ValidaTeMemberLogin(memberLogin))
                {
                    throw new Exception("Login fails!");
                }
                // lấy token từ api.
                var token = GetTokenFromApi(memberLogin);
                //lưu token ra file để dùng lại
                SaveTokenToLocalStorage(token);
                return token;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }

        public Member Register(Member member)
        {
            try
            {
                var httpClient = new HttpClient();
                //httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + token);
                HttpContent content = new StringContent(JsonConvert.SerializeObject(member), Encoding.UTF8,
                    "application/json");

                var httpRequestMessage = httpClient.PostAsync(ApiUrl.API_BASE_URL, content);
                var responseContent = httpRequestMessage.Result.Content.ReadAsStringAsync().Result;
                // parse member object
                var resMember = JsonConvert.DeserializeObject<Member>(responseContent);
                return resMember;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }

        private String GetTokenFromApi(MemberLogin memberLogin)
        {
            // thực hiện request lên api lấy token về.
            var dataContent = new StringContent(JsonConvert.SerializeObject(memberLogin),
                Encoding.UTF8, "application/json");
            var client = new HttpClient();
            var responseContent = client.PostAsync(ApiUrl.LOGIN_URL, dataContent).Result.Content.ReadAsStringAsync().Result;
            var jsonJObject = JObject.Parse(responseContent);
            if (jsonJObject["token"] == null)
            {
                throw new Exception("Login fails");
            }
            return jsonJObject["token"].ToString();
        }

        private void SaveTokenToLocalStorage(string token)
        {
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile sampleFile = storageFolder.CreateFileAsync("abcdz.txt",
                Windows.Storage.CreationCollisionOption.ReplaceExisting).GetAwaiter().GetResult();
            Windows.Storage.FileIO.WriteTextAsync(sampleFile, token).GetAwaiter().GetResult();
        }

        private bool ValidaTeMemberLogin(MemberLogin memberLogin)
        {
            return true;
        }

    }
}
