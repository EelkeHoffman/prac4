using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace prac4
{
    class Program
    {
        static void Main(string[] args)
        {




            Track ichWill = new Track("Ich Will", "Rammstein", TimeSpan.FromMinutes(4));
            Track duHast = new Track("Du Hast", "Rammstein", TimeSpan.FromMinutes(4));
            Track Deutchland = new Track("Sonne", "Rammstein", TimeSpan.FromMinutes(4));
            List<Track> tracks = new List<Track>();
            tracks.Add(ichWill);
            tracks.Add(duHast);
            tracks.Add(Deutchland);
            CD liebeIstFurAlleDa = new CD("MADE IN GERMANY", "Rammstein", tracks);
            List<CD> cds = new List<CD>();
            cds.Add(liebeIstFurAlleDa);

            var rootElem = new XElement("CDS");


            //http://ws.audioscrobbler.com/2.0/?method=album.getinfo&api_key=b5cbf8dcef4c6acfc5698f8709841949&artist=rammstein&album=MADE%20IN%20GERMANY&format=json
            foreach (CD cd in cds)
            {
                var customerElem = new XElement("CD",
                new XAttribute("title", cd.title),
                new XAttribute("artist", cd.artist),

            from c in cd.tracks
            select new XElement("Track",
                new XElement("Artist", c.artist),
                new XElement("Title", c.title),
                new XElement("Length", c.length.ToString()))
                );
                rootElem.Add(customerElem);
            }
            //Console.WriteLine(rootElem);
            //Console.ReadKey();






            var cdxml = new XDocument(new XElement("CDS",
                 from cd in cds
                 select new XElement("CD",
                 new XAttribute("title", cd.title),
                 new XAttribute("Artist", cd.artist),
                 new XElement("Tracks",



            from c in cd.tracks
            select new XElement("Track",
                new XElement("Artist", c.artist),
                new XElement("Title", c.title),
                new XElement("Length", c.length.ToString()))))));
//            Console.WriteLine(cdxml);


            String xmlString;
            using (System.Net.WebClient wc = new System.Net.WebClient())
            {
                xmlString = wc.DownloadString(@"http://ws.audioscrobbler.com/2.0/?method=album.getinfo&api_key=b5cbf8dcef4c6acfc5698f8709841949&artist=rammstein&album=MADE%20IN%20GERMANY");
            }
            XDocument myXMLDoc = XDocument.Parse(xmlString);
            var query =
             from x in myXMLDoc.Descendants("track")
             where !(from track in liebeIstFurAlleDa.tracks select track.title).Contains(x.Element("name").Value.ToString())
             select new Track(x.Element("name").Value.ToString(), x.Element("artist").Element("name").Value.ToString(), new TimeSpan(0, 0, Int32.Parse(x.Element("duration").Value.ToString())));

            foreach (Track t in query)
            {
                Console.WriteLine(t.title);
                liebeIstFurAlleDa.addSong(t);
            }
            var cdXml = new XDocument(new XElement("cd", new XAttribute("artist", liebeIstFurAlleDa.artist), new XAttribute("name", liebeIstFurAlleDa.title), new XElement("tracks",
            from track in liebeIstFurAlleDa.tracks
            select new XElement("track",
            new XElement("artist", track.artist),
            new XElement("title", track.title),
            new XElement("length", track.length.ToString())))));


            Console.WriteLine(cdXml.ToString());
            Console.ReadKey();
        }
    }
}
