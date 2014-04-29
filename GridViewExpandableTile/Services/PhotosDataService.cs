using GridViewExpandableTile.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace GridViewExpandableTile.Services
{
    class PhotosDataService
    {
        // TODO: Be nice and change this to use your own Flickr API key.
        private const string API_KEY = "bf3b5784792dcda75692837db1b6b0e5";
        private const string BASE_URL = "http://api.flickr.com/services/rest/?";
        private const string SEARCH_METHOD = "flickr.photos.search";

        private static readonly string[] SEARCH_TAGS = new string[] {
            "cats",
            "dogs",
            "puppies",
            "pythons",
            "elephants",
            "lions",
            "tigers",
            "hyenas",
            "snakes",
            "anacondas",
            "dinosaur",
            "velociraptor",
            "lake",
            "mountains",
            "hills",
            "babies"
        };

        static int nextIndex = 0;

        public static bool IsBusy { get; set; }

        public static bool HasMoreItems
        {
            get
            {
                return nextIndex < SEARCH_TAGS.Length;
            }
        }

        async public static Task<List<PhotoGroup>> LoadPhotos(int groupsCount)
        {
            if (!HasMoreItems || IsBusy)
            {
                return null;
            }

            IsBusy = true;
            groupsCount = Math.Min(groupsCount, SEARCH_TAGS.Length - nextIndex);

            List<PhotoGroup> photos = new List<PhotoGroup>();
            Task[] tasks = new Task[groupsCount];
            Random rnd = new Random();

            for (int i = nextIndex, j = 0; i < (nextIndex + groupsCount); ++i, ++j)
            {
                tasks[j] = Search(SEARCH_TAGS[i], rnd.Next(5, 10)).ContinueWith((pgtask) =>
                {
                    photos.Add(pgtask.Result);
                });
            }

            await Task.WhenAll(tasks);
            nextIndex += groupsCount;
            IsBusy = false;
            
            return photos;
        }

        async public static Task<PhotoGroup> Search(string search, int numberOfPhotos)
        {
            // build the url
            string url = String.Format("{0}method={1}&api_key={2}&per_page={3}&page=1&format=json&nojsoncallback=1&text={4}",
                BASE_URL, SEARCH_METHOD, API_KEY, numberOfPhotos, search);

            // load the json
            var uri = new Uri(url);
            var client = new HttpClient();
            var json = await client.GetStringAsync(uri);

            // parse and build Photo objects
            JObject result = JObject.Parse(json);
            var photos = result["photos"]["photo"].Children().ToList();
            var pg = new PhotoGroup { Name = search };
            pg.Photos = new List<Photo>();
            foreach (var p in photos)
            {
                var photo = JsonConvert.DeserializeObject<Photo>(p.ToString());
                var server = Int32.Parse(photo.Server);
                photo.TileType = (server % 2 == 0) ? TileType.Default : TileType.Big;
                pg.Photos.Add(photo);
            }

            return pg;
        }
    }
}
