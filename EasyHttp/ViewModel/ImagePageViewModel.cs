using EasyHttp.View.ImagePages;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EasyHttp.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class ImagePageViewModel
    {
        public Page ImgHomePage { get; set; }

        public Command GoBackCommand { get; set; }
        
        public ImagePageViewModel()
        {
            GoBackCommand = new Command(GoBack);
        }
        private void GoBack(object obj)
        {
            App.ImgNavigation.GoBack(App.ImgFrame);
        }
    }
}
