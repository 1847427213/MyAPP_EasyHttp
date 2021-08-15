using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyHttp.View;
using PropertyChanged;

namespace EasyHttp.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class ImgNetWorkViewModel
    {
        public Command UploadImageCommand { get; set; }
        public ImgNetWorkViewModel()
        {
            UploadImageCommand = new Command(UploadImage);
        }

        private void UploadImage(object obj)
        {
            UploadImageWindow uploadImageWindow = new UploadImageWindow();
            uploadImageWindow.Owner = App.Current.MainWindow;
            uploadImageWindow.ShowDialog();
        }
    }
}
