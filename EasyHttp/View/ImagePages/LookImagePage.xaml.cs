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
    /// LookImagePage.xaml 的交互逻辑
    /// </summary>
    public partial class LookImagePage : Page
    {
        LookImagePageViewModel ViewModel;
        public LookImagePage(string url)
        {
            InitializeComponent();
            DataContext = ViewModel = new LookImagePageViewModel();
            ViewModel.ImageSource = new BitmapImage(new Uri(url));
        }
    }
}
