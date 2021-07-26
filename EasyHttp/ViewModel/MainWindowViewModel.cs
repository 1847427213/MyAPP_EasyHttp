using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using PropertyChanged;
using EasyHttp.View;
using System.Collections.ObjectModel;

namespace EasyHttp.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class MainWindowViewModel
    {
        public Command GoHomeCommand { get; set; }
        public Command NavigatCommand { get; set; }
        public ObservableCollection<Menus> LeftMenus { get; set; }
        public MainWindowViewModel()
        {
            GoHomeCommand = new Command(GoHome);
            NavigatCommand = new Command(Navigat);
            LeftMenus = new ObservableCollection<Menus>()
            {
                new Menus(){ Name="Home",IsSelect=true },
                new Menus(){ Name="Get"},
                new Menus(){ Name="Post"},
                new Menus(){ Name="Images"}
            };
        }

        private void Navigat(object obj)
        {
            switch (((Menus)obj).Name)
            {
                case "Home":
                    App.Frame.Navigate(MyApp.Instance.MyApp_Page.HomePage ??= new GetPage());
                    break;
                case "Get":
                    App.Frame.Navigate(MyApp.Instance.MyApp_Page.GetPage ??= new GetPage());
                    break;
                case "Post":
                    App.Frame.Navigate(MyApp.Instance.MyApp_Page.PostPage ??= new PostPage());
                    break;
                case "Images":
                    App.Frame.Navigate(MyApp.Instance.MyApp_Page.ImagePage ??= new ImagePage());
                    break;
                default:
                    break;
            }
        }

        private void GoHome(object obj)
        {
            App.Frame.Navigate(MyApp.Instance.MyApp_Page.HomePage ??= new HomePage());
        }
    }
    [AddINotifyPropertyChangedInterface]
    public class Menus
    {
        public string Name { get; set; }
        public bool IsSelect { get; set; }
    }
}
