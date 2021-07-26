using EasyHttp.View.ImagePages;
using EasyHttp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EasyHttp.View
{
    /// <summary>
    /// ImagePage.xaml 的交互逻辑
    /// </summary>
    public partial class ImagePage : Page
    {
        ImagePageViewModel ViewModel=>DataContext as ImagePageViewModel;
        public ImagePage()
        {
            InitializeComponent();
            App.ImgFrame = frame;
            App.ImgFrame.Content = ViewModel.ImgHomePage ??= new ImgHomePage();
        }
    }
}
