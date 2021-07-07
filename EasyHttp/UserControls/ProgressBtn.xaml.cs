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

namespace EasyHttp.UserControls
{
    /// <summary>
    /// ProgressBtn.xaml 的交互逻辑
    /// </summary>
    public partial class ProgressBtn : UserControl
    {
        public string ProForeColor { get; set; } = "#63B4FF";
        public string ProBackColor { get; set; } = "#F4F4F4";
        public ProgressBtn()
        {
            InitializeComponent();
        }
        public string BtnContent
        {
            get { return (string)GetValue(BtnContentProperty); }
            set { SetValue(BtnContentProperty, value); }
        }
        public static readonly DependencyProperty BtnContentProperty = DependencyProperty.Register("BtnContent",
            typeof(string), typeof(ProgressBtn), new PropertyMetadata(""));
        public Command BtnCommand
        {
            get { return (Command)GetValue(BtnCommandProperty); }
            set { SetValue(BtnCommandProperty, value); }
        }
        public static readonly DependencyProperty BtnCommandProperty = DependencyProperty.Register("BtnCommand",
            typeof(Command), typeof(ProgressBtn), new PropertyMetadata(""));
    }
}
