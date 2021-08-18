using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using EasyHttp.Util;
using EasyHttp.View;
using EasyHttp.View.ImagePages;
using Newtonsoft.Json.Linq;
using PropertyChanged;

namespace EasyHttp.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class ImgNetWorkViewModel
    {
        public Command UploadImageCommand { get; set; }
        public Command LookImageCommand { get; set; }
        public ObservableCollection<NetWorkImage> NetWorkImages { get; set; }
        public int CountSize { get; set; }
        public ImgNetWorkViewModel()
        {
            NetWorkImages = new ObservableCollection<NetWorkImage>();
            UploadImageCommand = new Command(UploadImage);
            LookImageCommand = new Command(LookImage);
        }

        private void LookImage(object obj)
        {
            var item = obj as NetWorkImage;
            if (item == null) return;
            App.ImgNavigation.Navigation(new LookImagePage(item.ImgSource));
        }

        private void UploadImage(object obj)
        {
            UploadImageWindow uploadImageWindow = new UploadImageWindow();
            uploadImageWindow.Owner = App.Current.MainWindow;
            uploadImageWindow.ShowDialog();
        }
        public async void GetNetWorkImages()
        {
            Http http = new Http();
            var result = await http.GetAsync(HttpUrl.Image.GetImageList(NetWorkImages.Count, CountSize));
            foreach (var item in NetWorkImage.GetNetWorkImages(result))
            {
                NetWorkImages.Add(item);
            };
        }
    }
    [AddINotifyPropertyChangedInterface]
    public class NetWorkImage
    {
        public int ImaID { get; set; }
        public string ImgSource { get; set; }
        public string ImgThumbnail { get; set; }
        public DateTime ImgTime { get; set; }
        public string UserNickname { get; set; }
        public string UserAccount { get; set; }
        public Visibility ShowMenu { get; set; } = Visibility.Collapsed;
        public static List<NetWorkImage> GetNetWorkImages(string result)
        {
            List<NetWorkImage> images = new List<NetWorkImage>();
            if (!string.IsNullOrEmpty(result))
            {
                JObject jobject = JObject.Parse(result);
                foreach (var item in jobject.Value<JArray>("body"))
                {
                    images.Add(new NetWorkImage()
                    {
                        ImaID = item.Value<int>("imgid"),
                        ImgSource = $"{ HttpUrl.RootUrl }{item.Value<string>("imgsource")}",
                        ImgThumbnail = $"{ HttpUrl.RootUrl }{item.Value<string>("imgthumbnail")}",
                        ImgTime = item.Value<DateTime>("imgtime"),
                        UserNickname = item.Value<string>("userNickname"),
                        UserAccount = item.Value<string>("userAccount")
                    });
                }
            }
            return images;
        }
    }
}
