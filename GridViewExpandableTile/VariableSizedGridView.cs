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
    class VariableSizedGridView : GridView
    {
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            Photo photo = item as Photo;

            element.SetValue(VariableSizedWrapGrid.ColumnSpanProperty, (photo.TileType == TileType.Big) ? 2 : 1);
            element.SetValue(VariableSizedWrapGrid.RowSpanProperty, (photo.TileType == TileType.Big) ? 2 : 1);

            base.PrepareContainerForItemOverride(element, item);
        }
    }
}
