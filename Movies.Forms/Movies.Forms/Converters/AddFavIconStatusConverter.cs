using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Movies.Forms.Converters
{
    public class AddFavIconStatusConverter : IValueConverter
    {
        // warning: icons not added or tested in ios project
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null || !(value is bool)) return default;

            return (bool)value ? "ic_pressed_add_fav_btn" : "ic_add_fav_btn";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
