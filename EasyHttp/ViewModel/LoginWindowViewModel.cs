using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using EasyHttp.Util;
using EasyHttp.View;
using Newtonsoft.Json.Linq;
using PropertyChanged;

namespace EasyHttp.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class LoginWindowViewModel
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public Command LoginCommand { get; set; }
        public LoginWindowViewModel()
        {
            LoginCommand = new Command(Login);
        }
        private async void Login(object obj)
        {
            var LoginWindow = (LoginWindow)obj;
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(PassWord))
            {
                MessageBox.Show("账号或者密码错误!");
                return;
            }
            MultipartFormDataContent formDataContent = new MultipartFormDataContent();
            formDataContent.Add(new StringContent(UserName), "userAccount");
            formDataContent.Add(new StringContent(PassWord), "UserPassWord");
            Http http = new Http();
            var request = await http.PostAsync(HttpUrl.LoginUrl, formDataContent);
            JObject jobject = JObject.Parse(request);
            var code = jobject.Value<string>("code");
            var message = jobject.Value<string>("message");
            if (code.Equals("200"))
            {
                var body = jobject.Value<JObject>("body");
                MyApp.Instance.MyApp_User.SetUserInfo(body.Value<string>("userNickname"), body.Value<string>("userHeadimg"), body.Value<int>("userIdentity"));
            }
            else
            {
                MessageBox.Show(message);
                return;
            }
            LoginWindow.Close();
        }
    }
}
