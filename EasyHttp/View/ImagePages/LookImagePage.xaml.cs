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
            ViewModel.ImageSource = new BitmapImage(new Uri(url));
        }

        private void img_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void img_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                MoveImage(e.GetPosition(RootGrid));
            }
        }
        Point MovePoint;
        private void MasterImage_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            TransformGroup group = this.FindResource("ImageTransform") as TransformGroup;
            Point point = e.GetPosition(img);
            double scale = e.Delta * 0.001;
            if (ViewModel.Scale + scale <= 1) ViewModel.Scale = 1;
            else ViewModel.Scale += scale;
            ZoomImage(group, point, scale);
        }
        private static void ZoomImage(TransformGroup group, Point point, double scale)
        {
            Point pointToContent = group.Inverse.Transform(point);
            ScaleTransform transform = group.Children[0] as ScaleTransform;
            if (transform.ScaleX + scale < 1)
            {
                return;
            }
            transform.CenterX = point.X;
            transform.CenterY = point.Y;
            Debug.WriteLine(transform.CenterX);
            transform.ScaleX += scale;
            transform.ScaleY += scale;
            Debug.WriteLine(transform.CenterX);
            //TranslateTransform transform1 = group.Children[1] as TranslateTransform;
            //transform1.X = -1 * ((pointToContent.X * transform.ScaleX) - point.X);
            //transform1.Y = -1 * ((pointToContent.Y * transform.ScaleY) - point.Y);
        }
        Point previousMousePoint = default;
        private void MoveImage(Point position)
        {
            if (previousMousePoint == default)
                previousMousePoint = position;
            TransformGroup group = FindResource("ImageTransform") as TransformGroup;
            TranslateTransform transform = group.Children[1] as TranslateTransform;
            transform.X += position.X - this.previousMousePoint.X;
            transform.Y += position.Y - this.previousMousePoint.Y;
            if (transform.X != 0 && transform.Y != 0)
                this.previousMousePoint = position;
        }
        private void RotateImage(int num)
        {
            TransformGroup group = FindResource("ImageTransform") as TransformGroup;
            RotateTransform transform = group.Children[2] as RotateTransform;
            transform.CenterX = img.ActualWidth / 2;
            transform.CenterY = img.ActualHeight / 2;
            transform.Angle +=num;
        }

        private void img_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.previousMousePoint = default;
        }

        private void img_MouseLeave(object sender, MouseEventArgs e)
        {
            this.previousMousePoint = default;
            MovePoint = default;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RotateImage(-90);
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
