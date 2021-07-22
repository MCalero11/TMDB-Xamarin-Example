using Movies.Forms.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Movies.Forms.Converters
{
    public class MiniaturePosterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // think about moving sizes to an enum
            return Constants.BaseImageUrl + "/t/p/w185" + value?.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
