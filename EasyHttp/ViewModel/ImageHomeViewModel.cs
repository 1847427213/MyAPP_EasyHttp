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

namespace EasyHttp.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class ImageHomeViewModel
    {
        public ObservableCollection<Image> Images { get; set; }
        public Command LookImageCommand { get; set; }
        public Command LookImage1Command { get; set; }
        public Command DownLoadImageCommand { get; set; }
        public Command DeleteImageCommand { get; set; }
        public ImageHomeViewModel()
        {
            LookImageCommand = new Command(LookImage);
            LookImage1Command = new Command(LookImage1);
            DownLoadImageCommand = new Command(DownLoadImage);
            DeleteImageCommand = new Command(DeleteImage);
            Images = new ObservableCollection<Image>();
            Getiamges();
        }
        private void LookImage(object obj)
        {
            App.ImgFrame.Navigate(new LookImagePage(((Image)obj).Paths));
        }
        private void LookImage1(object obj)
        {
        }
        private void DownLoadImage(object obj)
        {
        }
        private void DeleteImage(object obj)
        {
            var item = (Image)obj;
            Images.Remove(item);
        }
        public async void Getiamges()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(@"C:\Users\赵晟\Desktop\Images");
            foreach (var item in directoryInfo.GetFiles())
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource =new Uri( item.FullName);
                bitmapImage.DecodePixelWidth = 120;
                bitmapImage.EndInit();
                Images.Add(new Image() { ImageSource = bitmapImage, Paths=item.FullName, Name = item.Name });
                await Task.Delay(10);
            }
        }
    }
    [AddINotifyPropertyChangedInterface]
    public class Image
    {
        public ImageSource ImageSource { get; set; }
        public string Paths { get; set; }
        public string Name { get; set; }
        public Visibility ShowMenu { get; set; } = Visibility.Hidden;
    }
}
