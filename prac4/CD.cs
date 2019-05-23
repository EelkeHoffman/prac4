using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prac4
{
    class CD
    {
        public String title { get; set; }
        public String artist { get; set; }
        public List<Track> tracks { get; set; }

        public CD(string title, string artist, List<Track> tracks)
        {
            this.title = title;
            this.artist = artist;
            this.tracks = tracks;
        }

        internal void addSong(Track t)
        {
            tracks.Add(t);
        }
    }
}
