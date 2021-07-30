using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using EasyHttp.Util;
using EasyHttp.View;

namespace EasyHttp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Frame Frame { get; set; } = App.Current.MainWindow.FindName("frame") as Frame;
        public static NavigationTool MainNavigation;
        public static Frame ImgFrame { get; set; }
        public static NavigationTool ImgNavigation;

    }
}
