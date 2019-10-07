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
using T1807EHelloUWP.Constant;
using T1807EHelloUWP.Entity;
using T1807EHelloUWP.Service;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace T1807EHelloUWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Page
    {
        private MemberService memberService;

        public Login()
        {
            this.InitializeComponent();
            //this.memberService = new MemberServiceImp();
            //// read file.
            //Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            //Windows.Storage.StorageFile sampleFile = storageFolder.GetFileAsync("sample.txt").GetAwaiter().GetResult();
            //var token = Windows.Storage.FileIO.ReadTextAsync(sampleFile).GetAwaiter().GetResult();
            //Debug.WriteLine(token);

            //HttpClient client = new HttpClient();
            //client.DefaultRequestHeaders.Add("Authorization", "Basic " + token);
            //var responseContent = client.GetAsync(ApiUrl.INFORMATION_URL).Result.Content.ReadAsStringAsync().Result;
            //JObject jsonJObject = JObject.Parse(responseContent);
            //Debug.WriteLine(jsonJObject["email"]);
        }

        private void ButtonLogin_OnClick(object sender, RoutedEventArgs e)
        {
            memberService.Login(Email.Text, Password.Password);
            ResetLoginForm();
        }

        private void ButtonReset_OnClick(object sender, RoutedEventArgs e)
        {
            ResetLoginForm();
        }

        private void ResetLoginForm()
        {
            this.Email.Text = string.Empty;
            this.Password.Password = string.Empty;
        }
    }
}
