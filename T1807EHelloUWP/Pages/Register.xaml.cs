using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
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
using T1807EHelloUWP.Service;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace T1807EHelloUWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Register : Page
    {
        private MemberService memberService;

        public Register()
        {
            this.InitializeComponent();
            this.memberService = new MemberServiceImp();
        }
        

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var member = new Member
            {
                firstName = "Dao",
                lastName = "Hung",
                password = "123456",
                address = "Hai Ba Trung",
                avatar = "https://i.ytimg.com/vi/MBtJdkiEhBk/maxresdefault.jpg",
                birthday = "2000-12-26",
                email = "hungdx1234567@gmail.com",
                gender = 1,
                introduction = "Hello T1807E",
                phone = "091234567"
            };
            // validate phía client.
            Debug.WriteLine(JsonConvert.SerializeObject(member));

            member = memberService.Register(member);
            if (member == null)
            {
                // show error
            }
            else
            {
                // show success
            }
        }
    }

}
