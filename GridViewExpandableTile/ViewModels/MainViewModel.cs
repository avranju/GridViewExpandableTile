using GridViewExpandableTile.Models;
using GridViewExpandableTile.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridViewExpandableTile.ViewModels
{
    public class MainViewModel
    {
        public ObservableCollection<Photo> Photos { get; set; }

        public MainViewModel()
        {
        }

        public async Task InitAsync()
        {
            var group = await PhotosDataService.Search("puppy", 10);
            Photos = new ObservableCollection<Photo>(group.Photos);
        }
    }
}
