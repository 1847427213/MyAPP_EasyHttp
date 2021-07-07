using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using PropertyChanged;
using EasyHttp.View;

namespace EasyHttp.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class MainWindowViewModel
    {
        public Command GoHomeCommand { get; set; }
        public MainWindowViewModel()
        {
            GoHomeCommand = new Command(GoHome);
        }

        private void GoHome(object obj)
        {
            App.Frame.Navigate(App.HomePage);
        }
    }
}
