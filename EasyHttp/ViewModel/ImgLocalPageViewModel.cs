using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;
using System.IO;
using System.Windows;
using EasyHttp.View.ImagePages;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using EasyHttp.Util;
using System.Diagnostics;

namespace EasyHttp.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class ImgLocalPageViewModel
    {
        public ObservableCollection<Image> Images { get; set; }
        public List<Image> ImagePaths { get; set; }
        public Command LookImageCommand { get; set; }
        public Command LookImage1Command { get; set; }
        public Command DownLoadImageCommand { get; set; }
        public Command DeleteImageCommand { get; set; }
        public Command AddImageCommand { get; set; }
        public ImgLocalPageViewModel()
        {
            LookImageCommand = new Command(LookImage);
            LookImage1Command = new Command(LookImage1);
            DownLoadImageCommand = new Command(DownLoadImage);
            DeleteImageCommand = new Command(DeleteImage);
            AddImageCommand = new Command(AddImage);
            Images = new ObservableCollection<Image>();
            ImagePaths = new List<Image>();
            GetIamges();
        }
        public int PageSize { get; set; }
        private async void AddImage(object obj)
        {
            var filenames = ImageTool.OpenIamge();
            if (filenames == null) return;
            foreach (var item in filenames)
            {
                var result = ImageTool.SaveImage(item);
                if (string.IsNullOrEmpty(result.Item1)) continue;
                ImagePaths.Add(new Image() { SourcePath = result.Item1, ThumbnailPath = result.Item2 });
            }
            LoadIamges(PageSize);
        }

        private void LookImage(object obj)
        {
            App.ImgNavigation.Navigation(new LookImagePage(((Image)obj).SourcePath));
        }
        private void LookImage1(object obj)
        {
        }
        private void DownLoadImage(object obj)
        {
            var item = (Image)obj;
            ImageTool.SaveBitmapImage(item.SourcePath);
        }
        private void DeleteImage(object obj)
        {
            var item = (Image)obj;
            Images.Remove(item);
            File.Delete(item.SourcePath);
            File.Delete(item.ThumbnailPath);
        }
        public void GetIamges()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(MyApp.Instance.MyApp_Path.LockIamge_SourceImage);
            foreach (var item in directoryInfo.GetFiles())
            {
                if (ImageTool.CheckImage(item.FullName) && ImageTool.ExistsImage(item.FullName))
                {

                    ImagePaths.Add(new Image() { SourcePath = item.FullName, ThumbnailPath = MyApp.Instance.MyApp_Path.LockIamge_Thumbnail + item.Name });
                }
            }
        }
        public Visibility Visibility { get; set; } = Visibility.Collapsed;
        /// <summary>
        /// 加载指定数量的图片
        /// </summary>
        /// <param name="count"></param>
        public async void LoadIamges(int count)
        {
            if (count <= 0) return;
            Visibility = Visibility.Visible;
            if (count > ImagePaths.Count)
                count = ImagePaths.Count;
            var items = ImagePaths.Take(count).ToList();
            ImagePaths.RemoveRange(0, count);
            foreach (var item in items)
            {
                item.ImageSource = ImageTool.GetBitmapImage(item.ThumbnailPath, 150);
                Images.Add(item);
                await Task.Delay(50);
            }
            Visibility = Visibility.Collapsed;
        }
    }
    [AddINotifyPropertyChangedInterface]
    public class Image
    {
        public ImageSource ImageSource { get; set; }
        public string SourcePath { get; set; }
        public string ThumbnailPath { get; set; }
        public string Name { get; set; }
        public Visibility ShowMenu { get; set; } = Visibility.Hidden;
    }
}
