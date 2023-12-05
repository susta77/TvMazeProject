using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TvMaze.Project.Models
{
    public class Show
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Language { get; set; }
        public List<string> Genres { get; set; }
        public string Status { get; set; }
        public int Runtime { get; set; }
        public int AverageRuntime { get; set; }
        public string Premiered { get; set; }
        public string Ended { get; set; }
        public string OfficialSite { get; set; }
        public Schedule Schedule { get; set; }
        public Rating Rating { get; set; }
        public int Weight { get; set; }
        public Network Network { get; set; }
        public object WebChannel { get; set; }
        public object DvdCountry { get; set; }
        public Externals Externals { get; set; }
        public Image Image { get; set; }
        public string Summary { get; set; }
        public int Updated { get; set; }
        public Links _Links { get; set; }
    }
}