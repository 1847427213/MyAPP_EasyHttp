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
        public Page ImgAuditPage { get; set; }
        public Visibility ShowGoBack { get; set; } = Visibility.Collapsed;


        public Command GoBackCommand { get; set; }
        public Command GoNetWorkPageCommand { get; set; }
        public Command GoLocalPageCommand { get; set; }
        public Command GoAuditPageCommand { get; set; }

        public ImagePageViewModel()
        {
            GoBackCommand = new Command(GoBack);
            GoNetWorkPageCommand = new Command(GoNetWorkPage);
            GoLocalPageCommand = new Command(GoLocalPage);
            GoAuditPageCommand = new Command(GoAuditPage);
        }

        private void GoAuditPage(object obj)
        {
            App.ImgNavigation.Navigation(ImgAuditPage ??= new ImgAuditPage());
        }

        private void GoLocalPage(object obj)
        {
            App.ImgNavigation.Navigation(ImgLocalPage ??= new ImgLocalPage());
        }

        private void GoNetWorkPage(object obj)
        {
            App.ImgNavigation.Navigation(ImgNetWorkPage ??= new ImgNetWorkPage());
        }

        private void GoBack(object obj)
        {
            App.ImgNavigation.GoBack();
        }
    }
}
