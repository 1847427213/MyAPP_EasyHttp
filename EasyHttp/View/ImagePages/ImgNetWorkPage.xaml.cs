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

namespace EasyHttp.View.ImagePages
{
    /// <summary>
    /// ImgNetWorkPage.xaml 的交互逻辑
    /// </summary>
    public partial class ImgNetWorkPage : Page
    {
        ImgNetWorkViewModel ViewModel => DataContext as ImgNetWorkViewModel;
        public ImgNetWorkPage()
        {
            InitializeComponent();
        }

        private void ItemsControl_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            int ViewportHeight = (int)e.ViewportHeight;
            ViewModel.CountSize = ViewportHeight / 161 + 1;
            if (ViewportHeight >= 161)
            {
                if (ViewportHeight + e.VerticalOffset >= e.ExtentHeight && e.ExtentHeight != 0)
                    ViewModel.GetNetWorkImages();
            }
        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            var item = (NetWorkImage)((System.Windows.Controls.Image)e.Source).DataContext;
            item.ShowMenu = Visibility.Visible;
        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            try
            {
                var item = (NetWorkImage)((System.Windows.Controls.Border)e.Source).DataContext;
                item.ShowMenu = Visibility.Hidden;
            }
            catch (Exception)
            {

            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.GetNetWorkImages();
        }
    }
}
