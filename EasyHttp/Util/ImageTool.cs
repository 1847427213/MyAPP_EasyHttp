﻿using System;
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
        /// <summary>
        /// 根据路径获取bitmapimage
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <param name="DecodePixel">缩放到像素</param>
        /// <returns></returns>
        public static BitmapImage GetBitmapImage(string filename, int DecodePixel = 0)
        {
            using FileStream fileStream = new FileStream(filename, FileMode.Open);
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.StreamSource = fileStream;
            if (DecodePixel != 0)
                bitmapImage.DecodePixelWidth = DecodePixel;
            bitmapImage.EndInit();
            bitmapImage.Freeze();
            return bitmapImage;
        }
        /// <summary>
        /// 加载图片
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="DecodePixel"></param>
        /// <returns></returns>
        public static BitmapImage GetBitmapImage2(string filename, int DecodePixel = 0)
        {
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.UriSource = new Uri(filename);
            if (DecodePixel != 0)
                bitmapImage.DecodePixelWidth = DecodePixel;
            bitmapImage.EndInit();
            //bitmapImage.Freeze();
            return bitmapImage;
        }
        /// <summary>
        /// 选择位置保存
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns></returns>
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
        /// <summary>
        /// 选择位置保存
        /// </summary>
        /// <param name="bitmapImage">图片对象</param>
        /// <returns></returns>
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
        /// <summary>
        /// 指定位置保存
        /// </summary>
        /// <param name="bitmapImage">图片对象</param>
        /// <param name="backname">保存位置</param>
        /// <returns></returns>
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
        /// <summary>
        /// 保存图片到本地文件夹
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns></returns>
        public static string SaveImage(string filename)
        {
            try
            {
                var savename = $@"{MyApp.Instance.MyApp_Path.ImgesPath}\{DateTime.Now.Ticks}.png";
                Stream stream = new FileStream(filename, FileMode.Open);
                FileStream fileStream = new FileStream(savename, FileMode.CreateNew);
                var bytes = new byte[stream.Length];
                stream.CopyTo(fileStream);
                fileStream.Flush();
                stream.Dispose();
                fileStream.Dispose();
                return savename;
            }
            catch (Exception)
            {

                return string.Empty;
            }
        }

        /// <summary>
        /// bitmap转bitmapimage
        /// </summary>
        /// <param name="bitmapImage">图片对象</param>
        /// <returns></returns>
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
        /// <summary>
        /// 检查是否是图像
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns></returns>
        public static bool CheckImage(string path)
        {
            return allowExts.Any(x => x == Path.GetExtension(path).ToLower());
        }
        public static string[] allowExts = new string[] { ".png", ".jpeg", ".jpg" };
        /// <summary>
        /// 打开图片-可多选
        /// </summary>
        /// <returns></returns>
        public static string[] OpenIamge()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Title = "保存图片",
                Filter = "image files (*.png)|*.png|(*.jpg)|*.jpg",
                Multiselect = true
            };
            if (openFileDialog.ShowDialog().Value)
            {
                return openFileDialog.FileNames;
            }
            return null;
        }
    }
}
