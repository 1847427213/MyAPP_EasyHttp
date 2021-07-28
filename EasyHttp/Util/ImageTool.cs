using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAnimatedGif;
using System.IO;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using System.Drawing;

namespace EasyHttp.Util
{
    public class ImageTool
    {
        public static BitmapImage GetBitmapImage(string filename, int DecodePixel = 0)
        {
            FileStream fileStream = new FileStream(filename, FileMode.Open);
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, bytes.Length);
            fileStream.Dispose();
            MemoryStream memoryStream = new MemoryStream(bytes);
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.StreamSource = memoryStream;
            if (DecodePixel != 0)
                bitmapImage.DecodePixelWidth = DecodePixel;
            bitmapImage.EndInit();
            bitmapImage.Freeze();
            memoryStream.Dispose();
            return bitmapImage;
        }
        public static bool SaveBitmapImage(string filename)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Title = "保存图片",
                Filter = "image files (*.png)|*.png|(*.jpg)|*.jpg",
                FileName = $"{DateTime.Now.Ticks}",
                AddExtension = true,
            };
            if (saveFileDialog.ShowDialog().Value)
            {
                FileStream fileStream = new FileStream(saveFileDialog.FileName, FileMode.OpenOrCreate);
                Stream stream = new FileStream(filename, FileMode.Open);
                stream.CopyTo(fileStream);
                fileStream.Flush();
                fileStream.Dispose();
                stream.Dispose();
                return true;
            }
            return false;
        }
        public static bool SaveBitmapImage(BitmapImage bitmapImage, string backname)
        {
            try
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                if (File.Exists(backname))
                    File.Delete(backname);
                FileStream fileStream = new FileStream(backname, FileMode.CreateNew);
                encoder.Save(fileStream);
                fileStream.Dispose();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public static bool SaveBitmapImage(BitmapImage bitmapImage)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Title = "保存图片",
                Filter = "image files (*.png)|*.png|(*.jpg)|*.jpg",
                FileName = $"{DateTime.Now.Ticks}",
                AddExtension = true,
            };
            if (saveFileDialog.ShowDialog().Value)
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                FileStream fileStream = new FileStream(saveFileDialog.FileName, FileMode.OpenOrCreate);
                encoder.Save(fileStream);
                fileStream.Dispose();
                return true;
            }
            return false;
        }
        public static Bitmap GetBitmapByBitmapImage(BitmapImage bitmapImage)
        {
            Bitmap bitmap;
            MemoryStream outStream = new MemoryStream();
            BitmapEncoder enc = new BmpBitmapEncoder();
            enc.Frames.Add(BitmapFrame.Create(bitmapImage));
            enc.Save(outStream);
            bitmap = new Bitmap(outStream);
            return bitmap;
        }
    }
}
