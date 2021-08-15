using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using EasyHttp.Util;
using Newtonsoft.Json.Linq;
using PropertyChanged;

namespace EasyHttp.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class ImgAuditViewMode
    {
        public int PageSize { get; set; } = 0;
        public int CountSize { get; set; } = 100;
        public ObservableCollection<ImgAuditModel> ImgAuditModels { get; set; }
        public ImgAuditViewMode()
        {
            ImgAuditModels = new ObservableCollection<ImgAuditModel>();
            GetImgAuditList();
        }
        public async void GetImgAuditList()
        {
            Http http = new Http();
            var result = await http.GetAsync(HttpUrl.Image.GetAuditList(PageSize, CountSize));
            if (!string.IsNullOrEmpty(result))
            {
                JObject jobject = JObject.Parse(result);
                foreach (var item in jobject.Value<JArray>("body"))
                {
                    var imgpath=$"{ HttpUrl.RootUrl }{item.Value<string>("auditthumbnail")}";
                    //var stream= await http.HttpClient.GetAsync(imgpath);
                    var model = new ImgAuditModel();
                    //model.ImageSource = ImageTool.GetBitmapImage(await stream.Content.ReadAsStreamAsync(), 150);
                    model.ImageSource = imgpath;
                    model.ImageThumbnail = item.Value<string>("auditthumbnail");
                    model.Audittime = item.Value<string>("audittime");
                    model.Auditnote = item.Value<string>("auditnote");
                    model.Auditid = item.Value<int>("auditid");
                    model.UserNickname = item.Value<string>("userNickname");
                    model.UserAccount = item.Value<string>("userAccount");
                    ImgAuditModels.Add(model);
                }
            }
        }
    }
    [AddINotifyPropertyChangedInterface]
    public class ImgAuditModel
    {
        public string ImageSource { get; set; }
        public string ImageThumbnail { get; set; }
        public string Audittime { get; set; }
        public string Auditnote { get; set; }
        public int Auditid { get; set; }
        public string UserNickname { get; set; }
        public string UserAccount { get; set; }
    }
}
