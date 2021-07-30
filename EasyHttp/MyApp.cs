using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using EasyHttp.Model;
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
        public MyApp_Path MyApp_Path { get; set; } = new MyApp_Path();
        public MyApp_User MyApp_User { get; set; } = new MyApp_User();
        public ImageSource MainBack { get; set; }

        private void IniBack()
        {
            if (File.Exists(MyApp_Path.BackImgPath))
            {
                SetBack(MyApp_Path.BackImgPath);
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
    [AddINotifyPropertyChangedInterface]
    public class MyApp_Path
    {
        public MyApp_Path()
        {
            CreateDirectory(ImgesPath);
        }

        public string BackImgPath { get; set; } = @$"{Environment.CurrentDirectory}\Iamge\BackIamge.png";
        public string ImgesPath { get; set; } = @$"{Environment.CurrentDirectory}\Iamge\LockIamge";
        private void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
