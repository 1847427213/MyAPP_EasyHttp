using EasyHttp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PropertyChanged;
using Newtonsoft.Json.Linq;

namespace EasyHttp.Model
{
    [AddINotifyPropertyChangedInterface]
    public class MyApp_User
    {
        public string NickName { get; set; }
        public string Token { get; set; }
        public Identity Identity { get; set; } = Identity.None;
        public ImageSource HeadImg { get; set; }
        public void SetUserInfo(JObject body)
        {
            this.NickName = body.Value<string>("userNickname");
            this.HeadImg = ImageTool.GetBitmapImage2($"{HttpUrl.RootUrl}{body.Value<string>("userHeadimg")}?{DateTime.Now.Ticks}");
            this.Identity = (Identity)body.Value<int>("userIdentity");
            this.Token = body.Value<string>("token");
        }
        public void DefaultUserInfo()
        {
            this.NickName = "游客";
            this.HeadImg = ImageTool.GetBitmapImage2($@"{Environment.CurrentDirectory}\defaultheadimg.png");
            this.Identity = Identity.Visitors;
            this.Token = string.Empty;
        }
    }
    public enum Identity
    {
        Admin,
        User,
        Visitors,
        None
    }
}
