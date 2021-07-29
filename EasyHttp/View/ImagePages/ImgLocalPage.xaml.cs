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
using Image = EasyHttp.ViewModel.Image;

namespace EasyHttp.View.ImagePages
{
    /// <summary>
    /// ImgLocalPage.xaml 的交互逻辑
    /// </summary>
    public partial class ImgLocalPage : Page
    {
        ImgLocalPageViewModel ViewModel => DataContext as ImgLocalPageViewModel;
        public ImgLocalPage()
        {
            InitializeComponent();

        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            var item = (Image)((System.Windows.Controls.Image)e.Source).DataContext;
            item.ShowMenu = Visibility.Visible;
        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            try
            {
                var item = (Image)((System.Windows.Controls.Border)e.Source).DataContext;
                item.ShowMenu = Visibility.Hidden;
            }
            catch (Exception)
            {

            }
        }
        private void ItemsControl_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (ViewModel.ImagePaths.Count > 0)
            {
                int ViewportHeight = (int)e.ViewportHeight;
                int ViewportWidth = (int)e.ViewportWidth;
                int count = (ViewportHeight / 162 + 1) * (ViewportWidth / 162);
                if (ViewModel.Images.Count == 0)
                {
                    ViewModel.LoadIamges(count);
                }
                else
                {
                    if (e.VerticalOffset + ViewportHeight >= e.ExtentHeight && (e.ExtentHeight > ViewportHeight || ViewModel.ImagePaths.Count < ViewportWidth / 162))
                    {
                        ViewModel.LoadIamges(count);
                    }
                    else if (count > ViewModel.Images.Count && e.ViewportHeightChange > 0 || e.ViewportWidthChange > 0)
                    {
                        ViewModel.LoadIamges(count - ViewModel.Images.Count);
                    }
                }
            }
        }
    }
}
