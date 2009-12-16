using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Longgong.mm.app
{
    /// <summary>
    /// converts a image url into a Image
    /// for use within a MenuItem
    /// </summary>
    [ValueConversion(typeof(String), typeof(Image))]
    public class MenuIconConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return Binding.DoNothing;
            }

            var imageUrl = value.ToString();
            if (string.IsNullOrEmpty(imageUrl))
            {
                return Binding.DoNothing;
            }

            var img = new Image {Width = 16, Height = 16};
            var bmp = new BitmapImage(new Uri(imageUrl, UriKind.RelativeOrAbsolute));
            img.Source = bmp;
            return img;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}