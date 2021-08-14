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
        public Command NavigatCommand { get; set; }
        public ObservableCollection<Menus> LeftMenus { get; set; }
        public MainWindowViewModel()
        {
            NavigatCommand = new Command(Navigat);
            LeftMenus = new ObservableCollection<Menus>()
            {
                new Menus(){ Name="Home",IsSelect=true },
                new Menus(){ Name="Images"}
            };
        }

        private void Navigat(object obj)
        {
            switch (((Menus)obj).Name)
            {
                case "Home":
                    MyApp.Instance.MyApp_Page.ShowPage = MyApp.Instance.MyApp_Page.HomePage ??= new HomePage();
                    break;
                case "Images":
                    MyApp.Instance.MyApp_Page.ShowPage = MyApp.Instance.MyApp_Page.ImagePage ??= new ImagePage();
                    break;
            }
        }
    }
    [AddINotifyPropertyChangedInterface]
    public class Menus
    {
        public string Name { get; set; }
        public bool IsSelect { get; set; }
    }
}
