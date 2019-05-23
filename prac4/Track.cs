using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prac4
{
    class Track
    {
        public String title { get; set; }
        public String artist { get; set; }
        public TimeSpan length { get; set; }

        public Track(string title, string artist, TimeSpan length)
        {
            this.title = title;
            this.artist = artist;
            this.length = length;
        }
    }
}
