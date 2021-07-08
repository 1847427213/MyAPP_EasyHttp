using EasyHttp.ViewModel;
using HandyControl.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EasyHttp.View
{
    /// <summary>
    /// ResponseWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ResponseWindow : BlurWindow
    {
        ResponseWindowViewModel ViewModel;
        public ResponseWindow(HttpResponseMessage response)
        {
            InitializeComponent();
            this.DataContext = ViewModel = new ResponseWindowViewModel(response);
        }
    }
}
