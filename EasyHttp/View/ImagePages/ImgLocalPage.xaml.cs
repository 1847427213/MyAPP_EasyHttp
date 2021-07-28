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
    }
}
