using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBookApp.Commons.Utils
{
    class FavouriteIconExtension : IMarkupExtension<String>
    {
        public bool IsFavourite {  get; set; }
        public string ProvideValue(IServiceProvider serviceProvider)
        {
            return IsFavourite ? "favicon.png" : "notfavicon.png";
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return (this as IMarkupExtension<String>).ProvideValue(serviceProvider);
        }
    }
}
