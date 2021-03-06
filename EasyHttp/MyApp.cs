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
        public Page ShowPage { get; set; }
        public Page HomePage { get; set; }
        public Page ImagePage { get; set; }
    }
    [AddINotifyPropertyChangedInterface]
    public class MyApp_Path
    {
        public MyApp_Path()
        {
            CreateDirectory(ImgPath);
            CreateDirectory(LockIamge);
            CreateDirectory(LockIamge_SourceImage);
            CreateDirectory(LockIamge_Thumbnail);
        }
        public string ImgPath { get; set; } = @$"{Environment.CurrentDirectory}\Iamge\";
        public string BackImgPath { get; set; } = @$"{Environment.CurrentDirectory}\Iamge\BackIamge.png";
        public string LockIamge { get; set; } = @$"{Environment.CurrentDirectory}\Iamge\LockIamge\";
        public string LockIamge_SourceImage { get; set; } = @$"{Environment.CurrentDirectory}\Iamge\LockIamge\SourceImage\";
        public string LockIamge_Thumbnail { get; set; } = @$"{Environment.CurrentDirectory}\Iamge\LockIamge\Thumbnail\";
        private void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
