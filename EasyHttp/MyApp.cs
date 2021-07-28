using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using EasyHttp.Util;
using PropertyChanged;

namespace EasyHttp
{
    [AddINotifyPropertyChangedInterface]
    public class MyApp
    {
        public MyApp()
        {
            IniBack();
        }
        private static MyApp instance;
        public static MyApp Instance { get; set; } = instance ??= new MyApp();
        public MyApp_Page MyApp_Page { get; set; } = new MyApp_Page();
        public ImageSource MainBack { get; set; }
        private void IniBack()
        {
            var BackPath = @$"{Environment.CurrentDirectory}\BackIamge.png";
            if (File.Exists(BackPath))
            {
                SetBack(BackPath);
            }
        }
        public void SetBack(BitmapImage bitmapImage)
        {
            MainBack = bitmapImage;
        }
        public void SetBack(string filename)
        {
            MainBack = ImageTool.GetBitmapImage(filename);
        }
    }
    [AddINotifyPropertyChangedInterface]
    public class MyApp_Page
    {
        public Page HomePage { get; set; }
        public Page GetPage { get; set; }
        public Page PostPage { get; set; }
        public Page ImagePage { get; set; }
    }
}
