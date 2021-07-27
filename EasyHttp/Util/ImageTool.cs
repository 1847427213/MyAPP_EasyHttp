using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using WpfAnimatedGif;

namespace EasyHttp.Util
{
    public class ImageTool
    {
        public static BitmapImage GetBitmapImage(string filename, int DecodePixel = 0)
        {
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(filename);
            if (DecodePixel != 0)
                bitmapImage.DecodePixelWidth = DecodePixel;
            bitmapImage.EndInit();
            return bitmapImage;
        }
    }
}
