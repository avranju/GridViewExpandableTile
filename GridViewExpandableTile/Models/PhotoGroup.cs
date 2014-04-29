using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridViewExpandableTile.Models
{
    class PhotoGroup
    {
        public string Name { get; set; }
        public IList<Photo> Photos { get; set; }
    }
}
