using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBookApp.Commons.Utils
{
    class EditIconConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if ((bool)value!) return "cancel.png";
            else if (Application.Current.RequestedTheme == AppTheme.Light) return "editdark.png";
            else return "editlight.png";

        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
