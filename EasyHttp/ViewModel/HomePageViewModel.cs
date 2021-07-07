using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;

namespace EasyHttp.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class HomePageViewModel
    {
        public Command GoGetCommand { get; set; }
        public Command GoPostCommand { get; set; }
        public HomePageViewModel()
        {
            GoGetCommand = new Command(GoGet);
            GoPostCommand = new Command(GoPost);
        }

        private void GoGet(object obj)
        {
            App.Frame.Navigate(App.GetPage);
        }

        private void GoPost(object obj)
        {
            App.Frame.Navigate(App.PostPage);
        }
    }
}
