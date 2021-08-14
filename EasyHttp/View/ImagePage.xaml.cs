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
            frame.Navigated += ImgFrame_Navigated;
            App.ImgNavigation = new Util.NavigationTool(frame);
            App.ImgNavigation.Navigation(ViewModel.ImgNetWorkPage ??= new ImgNetWorkPage());
        }

        private void ImgFrame_Navigated(object sender, NavigationEventArgs e)
        {
            var pagename= e.Content.GetType().Name;
            if (pagename.Equals("LookImagePage"))
            {
                ViewModel.ShowGoBack = Visibility.Visible;
            }
            else ViewModel.ShowGoBack = Visibility.Collapsed;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if ((bool)NetWork.IsChecked)
            {
                App.ImgNavigation.Navigation(ViewModel.ImgNetWorkPage ??= new ImgNetWorkPage());
            }
            else App.ImgNavigation.Navigation(ViewModel.ImgLocalPage ??= new ImgLocalPage());
        }
    }
}
