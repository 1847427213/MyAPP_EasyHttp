using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EasyHttp
{
    public class MyApp
    {
        private static MyApp instance;
        public static MyApp Instance { get; set; } = instance ?? new MyApp();
        public MyApp_Page MyApp_Page { get; set; } = new MyApp_Page();
    }
    public class MyApp_Page
    {
        public Page HomePage { get; set; }
        public Page GetPage { get; set; }
        public Page PostPage { get; set; }
        public Page ImagePage { get; set; }
    }
}
