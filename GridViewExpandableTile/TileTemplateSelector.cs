using GridViewExpandableTile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace GridViewExpandableTile
{
    class TileTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DefaultItemTemplate { get; set; }
        public DataTemplate BigItemTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item)
        {
            Photo photo = item as Photo;
            return (photo.TileType == TileType.Big) ? BigItemTemplate : DefaultItemTemplate;
        }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            Photo photo = item as Photo;
            return (photo.TileType == TileType.Big) ? BigItemTemplate : DefaultItemTemplate;
        }
    }
}
