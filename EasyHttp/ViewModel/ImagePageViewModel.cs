using EasyHttp.View.ImagePages;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EasyHttp.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class ImagePageViewModel
    {
        public Page ImgLocalPage { get; set; }
        public Page ImgNetWorkPage { get; set; }
        public Visibility ShowGoBack { get; set; } = Visibility.Collapsed;


        public Command GoBackCommand { get; set; }
        public Command GoNetWorkPageCommand { get; set; }
        public Command GoLocalPageCommand { get; set; }

        public ImagePageViewModel()
        {
            GoBackCommand = new Command(GoBack);
            GoNetWorkPageCommand = new Command(GoNetWorkPage);
            GoLocalPageCommand = new Command(GoLocalPage);
        }

        private void GoLocalPage(object obj)
        {
            App.ImgNavigation.Navigation(App.ImgFrame, ImgLocalPage ??= new ImgLocalPage());
        }

        private void GoNetWorkPage(object obj)
        {
            App.ImgNavigation.Navigation(App.ImgFrame, ImgNetWorkPage ??= new ImgNetWorkPage());
        }

        private void GoBack(object obj)
        {
            App.ImgNavigation.GoBack(App.ImgFrame);
        }
    }
}
