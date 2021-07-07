using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using EasyHttp.View;

namespace EasyHttp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Frame Frame { get; set; }
        public static Page HomePage { get; set; } = new HomePage();
        public static Page GetPage { get; set; } = new GetPage();
        public static Page PostPage { get; set; } = new PostPage();
    }
}
