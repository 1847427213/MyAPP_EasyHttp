using EasyHttp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PropertyChanged;

namespace EasyHttp.Model
{
    [AddINotifyPropertyChangedInterface]
    public class MyApp_User
    {
        public string NickName { get; set; }
        public string Token { get; set; }
        public Identity Identity { get; set; } = Identity.None;
        public ImageSource HeadImg { get; set; }
        public void SetUserInfo(string nickname, string headimg, int identity)
        {
            this.NickName = nickname;
            this.HeadImg = ImageTool.GetBitmapImage2($"{HttpUrl.RootUrl}{headimg}?{DateTime.Now.Ticks}");
            this.Identity = (Identity)identity;
        }
        public void DefaultUserInfo()
        {
            this.NickName = "游客";
            this.HeadImg = ImageTool.GetBitmapImage2($@"{Environment.CurrentDirectory}\defaultheadimg.png");
            this.Identity = Identity.Visitors;
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
