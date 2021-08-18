using EasyHttp.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// ImgAuditPage.xaml 的交互逻辑
    /// </summary>
    public partial class ImgAuditPage : Page
    {
        ImgAuditViewMode ViewModel => DataContext as ImgAuditViewMode;
        public ImgAuditPage()
        {
            InitializeComponent();
        }

        private void ItemsControl_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            int ViewportHeight = (int)e.ViewportHeight;
            int count = ViewportHeight / 151 + 1;
            ViewModel.CountSize = count;
            if (ViewportHeight >= 151)
            {
                if (ViewportHeight + e.VerticalOffset >= e.ExtentHeight && e.ExtentHeight != 0)
                    ViewModel.GetImgAuditList();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            ViewModel.GetImgAuditList();
        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            var item = (ImgAuditModel)((System.Windows.Controls.Image)e.Source).DataContext;
            item.ShowLook = Visibility.Visible;
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            var item = (ImgAuditModel)((System.Windows.Controls.Button)e.Source).DataContext;
            item.UserNickname = "123";
            item.ShowLook = Visibility.Collapsed;
        }
    }
}
