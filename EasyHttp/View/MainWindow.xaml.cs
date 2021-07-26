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
using EasyHttp.ViewModel;

namespace EasyHttp.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel ViewModel => this.DataContext as MainWindowViewModel;
        public MainWindow()
        {
            InitializeComponent();
            App.Frame.Navigate(MyApp.Instance.MyApp_Page.HomePage ?? new HomePage());
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            App.Frame.Navigate(MyApp.Instance.MyApp_Page.HomePage ?? new HomePage());
        }
    }
}
