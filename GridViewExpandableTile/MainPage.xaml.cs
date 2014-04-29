using GridViewExpandableTile.Models;
using GridViewExpandableTile.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace GridViewExpandableTile
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private MainViewModel _viewModel = new MainViewModel();

        public MainPage()
        {
            this.InitializeComponent();

            Init();
        }
        async private Task Init()
        {
            // initialize view model
            await _viewModel.InitAsync();

            tilesGrid.ItemsSource = _viewModel.Photos;
        }

        private void tilesGrid_ItemClick(object sender, ItemClickEventArgs e)
        {
            var photo = e.ClickedItem as Photo;
            photo.TileType = (photo.TileType == TileType.Big) ? TileType.Default : TileType.Big;

            // update the tile spanning settings on the item
            GridViewItem container = tilesGrid.ContainerFromItem(e.ClickedItem) as GridViewItem;
            container.SetValue(VariableSizedWrapGrid.ColumnSpanProperty, (photo.TileType == TileType.Big) ? 2 : 1);
            container.SetValue(VariableSizedWrapGrid.RowSpanProperty, (photo.TileType == TileType.Big) ? 2 : 1);

            // force template selection to occur again
            // PERFORMANCE WARNING: This will re-evaluate the data template for *all* the items in the grid.
            tilesGrid.ItemTemplateSelector = null;
            tilesGrid.ItemTemplateSelector = Resources["TileTemplateSelector"] as TileTemplateSelector;
        }
    }
}
