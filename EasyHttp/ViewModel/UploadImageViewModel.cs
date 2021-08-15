using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using EasyHttp.Util;
using Newtonsoft.Json.Linq;
using PropertyChanged;

namespace EasyHttp.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class UploadImageViewModel
    {
        public Command DeleteImageCommand { get; set; }
        public Command AddImageCommand { get; set; }
        public Command UploadImageCommand { get; set; }
        public bool IsEnabled { get; set; } = true;
        public ObservableCollection<UploadImageModel> UploadImageModels { get; set; }
        public UploadImageViewModel()
        {
            UploadImageModels = new ObservableCollection<UploadImageModel>();
            DeleteImageCommand = new Command(DeleteImage);
            AddImageCommand = new Command(AddImage);
            UploadImageCommand = new Command(UploadImage);
        }

        private async void UploadImage(object obj)
        {
            IsEnabled = false;
            Http http = new Http();
            int count = UploadImageModels.Count;
            for (int i = 0; i < UploadImageModels.Count; i++)
            {
                try
                {
                    var stream = File.OpenRead(UploadImageModels[i].ImagePath);
                    MultipartFormDataContent formDataContent = new MultipartFormDataContent();
                    formDataContent.Add(new StreamContent(stream), "file", $"{DateTimeTool.GetTimeStamp()}{Path.GetExtension(UploadImageModels[i].ImagePath)}");
                    var result = await http.PostAsync(HttpUrl.Image.UploadToAudit(), formDataContent);
                    await stream.DisposeAsync();
                    formDataContent.Dispose();
                    UploadImageModels.Remove(UploadImageModels[i]);
                    i--;
                    //if (JObject.Parse(result).Value<string>("code").Equals("200"))
                    //{
                    //    MessageBox.Show(result);
                    //}
                }
                catch (Exception)
                {
                }
            }
            IsEnabled = true;
            MessageBox.Show($"共{count}张图片，上传成功{count - UploadImageModels.Count}张，失败{UploadImageModels.Count}张");
        }

        private async void AddImage(object obj)
        {
            var paths = ImageTool.OpenIamge();
            if (paths == null) return;
            foreach (var item in paths)
            {
                UploadImageModels.Add(new UploadImageModel() { ImageSource = ImageTool.GetBitmapImage(item, 200), ImagePath = item });
                await Task.Delay(10);
            }
        }

        private void DeleteImage(object obj)
        {
            UploadImageModels.Remove((UploadImageModel)obj);
        }
    }
    [AddINotifyPropertyChangedInterface]
    public class UploadImageModel
    {
        public ImageSource ImageSource { get; set; }
        public string ImagePath { get; set; }
        public Visibility ShowMenu { get; set; } = Visibility.Collapsed;
    }
}
