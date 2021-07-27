using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using PropertyChanged;

namespace EasyHttp.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class LookImagePageViewModel
    {
        public ImageSource ImageSource { get; set; }
        public double Scale { get; set; } = 1.0d;
        public LookImagePageViewModel()
        { 
        
        }
    }
}
