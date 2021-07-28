using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using EasyHttp.Util;
using PropertyChanged;

namespace EasyHttp.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class LookImagePageViewModel
    {
        public ImageSource ImageSource { get; set; }
        public Command DownLoadImageCommand { get; set; }
        public Command SetBackImageCommand { get; set; }
        public double Scale { get; set; } = 1.0d;
        public LookImagePageViewModel()
        {
            DownLoadImageCommand = new Command(DownLoadImage);
            SetBackImageCommand = new Command(SetBackImage);
        }
        private void SetBackImage(object obj)
         {
            var BackPath = @$"{Environment.CurrentDirectory}\BackIamge.png";
            if (ImageTool.SaveBitmapImage((BitmapImage)ImageSource, BackPath))
            {
                MyApp.Instance.SetBack((BitmapImage)ImageSource);
            }
        }

        private void DownLoadImage(object obj)
        {
            ImageTool.SaveBitmapImage((BitmapImage)ImageSource);
        }
    }
}
