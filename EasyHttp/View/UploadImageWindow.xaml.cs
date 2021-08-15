using EasyHttp.Util;
using EasyHttp.ViewModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EasyHttp.View
{
    /// <summary>
    /// UploadImageWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UploadImageWindow : Window
    {
        UploadImageViewModel ViewModel;
        public UploadImageWindow()
        {
            InitializeComponent();
            this.DataContext = ViewModel = new UploadImageViewModel();
        }
        public string ImgPath { get; set; }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var paths = ImageTool.OpenIamge();
            if (paths==null) return;
            foreach (var item in paths)
            {
                ViewModel.UploadImageModels.Add(new UploadImageModel() {ImageSource= ImageTool.GetBitmapImage(item, 200) ,ImagePath= item});
            }
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    uploadBtn.IsEnabled = false;
            //    var stream = File.OpenRead(ImgPath);
            //    MultipartFormDataContent formDataContent = new MultipartFormDataContent();
            //    formDataContent.Add(new StreamContent(stream), "Files", $"{DateTimeTool.GetTimeStamp()}{System.IO.Path.GetExtension(ImgPath)}");
            //    Http http = new Http();
            //    var result = await http.PostAsync(HttpUrl.Image.UploadToAudit, formDataContent);
            //    if (JObject.Parse(result).Value<string>("code").Equals("200"))
            //    {
            //        MessageBox.Show(result);
            //        ImgPath = string.Empty;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //finally
            //{
            //    uploadBtn.IsEnabled = true;
            //}
        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            var item = (UploadImageModel)((System.Windows.Controls.Image)e.Source).DataContext;
            item.ShowMenu = Visibility.Visible;
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            try
            {
                var item = (UploadImageModel)((System.Windows.Controls.Button)e.Source).DataContext;
                item.ShowMenu = Visibility.Collapsed;
            }
            catch (Exception)
            {

            }
        }
    }
}
