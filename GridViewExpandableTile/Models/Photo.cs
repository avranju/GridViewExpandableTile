using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridViewExpandableTile.Models
{
    public class Photo : INotifyPropertyChanged
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("owner")]
        public string Owner { get; set; }
        [JsonProperty("secret")]
        public string Secret { get; set; }
        [JsonProperty("server")]
        public string Server { get; set; }
        [JsonProperty("farm")]
        public int Farm { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("ispublic")]
        public bool IsPublic { get; set; }
        [JsonProperty("isfriend")]
        public bool IsFriend { get; set; }
        [JsonProperty("isfamily")]
        public bool IsFamily { get; set; }

        private TileType _tileType;

        [JsonIgnore]
        public TileType TileType
        {
            get
            {
                return _tileType;
            }

            set
            {
                if (value != _tileType)
                {
                    _tileType = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("TileType"));
                    }
                }
            }
        }

        [JsonIgnore]
        public Uri Uri
        {
            get
            {
                return new Uri(String.Format("http://farm{0}.staticflickr.com/{1}/{2}_{3}_q.jpg",
                    Farm, Server, Id, Secret));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
