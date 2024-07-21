using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloneNetflix.Models
{
    //this class is subset of the result class to show specific data not all
    public class Media
    {
        public int Id { get; set; }

        public string DisplayTitle { get; set; }
        public string MediaType { get; set; }

        public string Thumbnail { get; set; }
        public string ThumbnailSmall { get; set; }
        public string ThumbnailUrl { get; set; }

        public string ReleaseDate { get; set; }
        public string Overview { get; set; }
    }
}