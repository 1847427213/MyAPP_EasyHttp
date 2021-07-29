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
            Getiamges();
        }

        private void AddImage(object obj)
        {
            var filenames = ImageTool.OpenIamge();
            if (filenames == null) return;
            foreach (var item in filenames)
            {
                ImagePaths.Add(new Image() { Paths = item, IsLocal = false });
            }
            LoadIamges(15);
        }

        private void LookImage(object obj)
        {
            App.ImgNavigation.Navigation(App.ImgFrame, new LookImagePage(((Image)obj).Paths));
        }
        private void LookImage1(object obj)
        {
        }
        private void DownLoadImage(object obj)
        {
            var item = (Image)obj;
            ImageTool.SaveBitmapImage(item.Paths);
        }
        private void DeleteImage(object obj)
        {
            var item = (Image)obj;
            Images.Remove(item);
            File.Delete(item.Paths);
        }
        public void Getiamges()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(MyApp.Instance.MyApp_Path.ImgesPath);
            foreach (var item in directoryInfo.GetFiles())
            {
                if (ImageTool.CheckImage(item.FullName))
                {
                    ImagePaths.Add(new Image() { Paths = item.FullName });
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
                BitmapImage bitmapImage;
                if (!item.IsLocal)
                {
                    var path = ImageTool.SaveImage(item.Paths);
                    if (string.IsNullOrEmpty(path)) continue;
                    bitmapImage = ImageTool.GetBitmapImage(path, 150);
                    Images.Add(new Image() { ImageSource = bitmapImage, Paths = path, Name = Path.GetFileNameWithoutExtension(path) });
                }
                else
                {
                    bitmapImage = ImageTool.GetBitmapImage(item.Paths, 150);
                    Images.Add(new Image() { ImageSource = bitmapImage, Paths = item.Paths, Name = Path.GetFileNameWithoutExtension(item.Paths) });
                }
                await Task.Delay(50);
            }
            Visibility = Visibility.Collapsed;
        }
    }
    [AddINotifyPropertyChangedInterface]
    public class Image
    {
        public ImageSource ImageSource { get; set; }
        public string Paths { get; set; }
        public string Name { get; set; }
        public Visibility ShowMenu { get; set; } = Visibility.Hidden;
        public bool IsLocal { get; set; } = true;
    }
}
