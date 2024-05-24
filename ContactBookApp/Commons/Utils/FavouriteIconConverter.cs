using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBookApp.Commons.Utils
{
    class FavouriteIconConverter : IValueConverter
    {

        
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if ((bool)value!) return "favicon.png";
            else return "notfavicon.png";

        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if(value!.Equals("favicon.png")) return true;
            return false;
        }

    }
}
