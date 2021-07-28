using EasyHttp.Util;
using EasyHttp.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EasyHttp.View.ImagePages
{
    /// <summary>
    /// LookImagePage.xaml 的交互逻辑
    /// </summary>
    public partial class LookImagePage : Page
    {
        LookImagePageViewModel ViewModel;
        public LookImagePage(string url)
        {
            InitializeComponent();
            DataContext = ViewModel = new LookImagePageViewModel();
            ViewModel.ImageSource = ImageTool.GetBitmapImage(url);
        }
        Point MovePoint;
        private void RotateImage(int Angle)
        {
            rotate.CenterX = img.ActualWidth / 2;
            rotate.CenterY = img.ActualHeight / 2;
            rotate.Angle += Angle;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RotateImage(-90);
        }
        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            RotateImage(90);
        }
        private void Page_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var height = img.ActualHeight;
            var width = img.ActualWidth;
            var point = e.GetPosition(img);
            var newOrigin = new Point(point.X / width, point.Y / height);
            var delta = newOrigin - img.RenderTransformOrigin;
            if (e.Delta < 0)
            {
                if (scale.ScaleY * 0.8 >= 0.5)
                {
                    scale.ScaleX = scale.ScaleY = scale.ScaleY * 0.8;
                    translate.Y += height * delta.Y * 0.25 * scale.ScaleY;
                    translate.X += width * delta.X * 0.25 * scale.ScaleX;
                }
            }
            else
            {
                if (scale.ScaleY / 0.8 <= 15)
                {
                    scale.ScaleX = scale.ScaleY = scale.ScaleY / 0.8;
                    translate.Y -= height * delta.Y * 0.2 * scale.ScaleY;
                    translate.X -= width * delta.X * 0.2 * scale.ScaleX;
                }
            }
            ViewModel.Scale = scale.ScaleX;
        }
        private void Page_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mousedown = true;
            MovePoint = e.GetPosition(this);
        }
        bool mousedown = false;
        private void Page_MouseLeave(object sender, MouseEventArgs e)
        {
            mousedown = false;
        }
        private void Page_MouseMove(object sender, MouseEventArgs e)
        {
            if (mousedown && e.LeftButton == MouseButtonState.Pressed)
            {
                var point = e.GetPosition(this);
                var delta = MoveImg(point);
                MovePoint = point;
                translate.X += delta.X;
                translate.Y += delta.Y;
            }
        }
        public Point MoveImg(Point point)
        {
            var delta = new Point();
            switch (rotate.Angle % 360)
            {
                case 0:
                    delta = new Point(point.X - MovePoint.X, point.Y - MovePoint.Y);
                    break;
                case 90:
                case -270:
                    delta = new Point((point.Y - MovePoint.Y), (point.X - MovePoint.X) * -1);
                    break;
                case 180:
                case -180:
                    delta = new Point((point.X - MovePoint.X) * -1, (point.Y - MovePoint.Y) * -1);
                    break;
                case -90:
                case 270:
                    delta = new Point((point.Y - MovePoint.Y) * -1, point.X - MovePoint.X);
                    break;
            }
            return delta;
        }
        private void Page_MouseUp(object sender, MouseButtonEventArgs e)
        {
            mousedown = false;
        }
    }
    public class ScaleConvter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double scale = (double)value;
            return $"{scale.ToString("f1")}X";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
