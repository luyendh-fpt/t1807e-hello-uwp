using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using T1807EHelloUWP.Entity;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace T1807EHelloUWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Page
    {
        private string LOGIN_URL = "https://2-dot-backup-server-003.appspot.com/_api/v2/members/authentication";
        private string INFORMATION_URL = "https://2-dot-backup-server-003.appspot.com/_api/v2/members/information";
        public Login()
        {
            this.InitializeComponent();
            // read file.
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile sampleFile = storageFolder.GetFileAsync("sample.txt").GetAwaiter().GetResult();
            var token = Windows.Storage.FileIO.ReadTextAsync(sampleFile).GetAwaiter().GetResult();
            Debug.WriteLine(token);

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Basic " + token);
            var responseContent = client.GetAsync(INFORMATION_URL).Result.Content.ReadAsStringAsync().Result;
            JObject jsonJObject = JObject.Parse(responseContent);
            Debug.WriteLine(jsonJObject["email"]);
        }

        private void ButtonLogin_OnClick(object sender, RoutedEventArgs e)
        {
            // tạo đối tượng member login từ giá trị của form.
            var memberLogin = new MemberLogin()
            {
                email = this.Email.Text,
                password = this.Password.Password
            };

            // validate
            var dataContent = new StringContent(JsonConvert.SerializeObject(memberLogin), 
                Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            var responseContent = client.PostAsync(LOGIN_URL, dataContent).Result.Content.ReadAsStringAsync().Result;
            JObject jsonJObject = JObject.Parse(responseContent);
            Debug.WriteLine(jsonJObject["token"]);
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

            Windows.Storage.StorageFile sampleFile = storageFolder.CreateFileAsync("sample.txt",
                Windows.Storage.CreationCollisionOption.ReplaceExisting).GetAwaiter().GetResult();

            Windows.Storage.FileIO.WriteTextAsync(sampleFile, jsonJObject["token"].ToString()).GetAwaiter().GetResult();
            Debug.WriteLine(sampleFile.Path);
        }

        private void ButtonReset_OnClick(object sender, RoutedEventArgs e)
        {
            this.Email.Text = string.Empty;
            this.Password.Password = string.Empty;
        }
    }
}
