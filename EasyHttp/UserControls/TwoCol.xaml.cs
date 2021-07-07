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
    /// TwoCol.xaml 的交互逻辑
    /// </summary>
    public partial class TwoCol : UserControl
    {
        public TwoCol()
        {
            InitializeComponent();
        }
        public string KeyParm
        {
            get { return (string)GetValue(KeyParmProperty); }
            set { SetValue(KeyParmProperty, value); }
        }
        public static readonly DependencyProperty KeyParmProperty = DependencyProperty.Register("KeyParm",
            typeof(string), typeof(TwoCol), new PropertyMetadata(""/*, new PropertyChangedCallback(OnKeyParmPropertyChange)*/));

        private static void OnKeyParmPropertyChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public string ValueParm
        {
            get { return (string)GetValue(ValueParmProperty); }
            set { SetValue(ValueParmProperty, value); }
        }
        public static readonly DependencyProperty ValueParmProperty = DependencyProperty.Register("ValueParm",
            typeof(string), typeof(TwoCol), new PropertyMetadata(""));

        private static void OnValueParmPropertyChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        
    }
}
