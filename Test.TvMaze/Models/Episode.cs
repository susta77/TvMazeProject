using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TvMaze.Project.Models
{
    public class Episode
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public int Season { get; set; }
        public int Number { get; set; }
        public string Type { get; set; }
        public string Airdate { get; set; }
        public string Airtime { get; set; }
        public string Airstamp { get; set; }
        public int Runtime { get; set; }
        public Rating Rating { get; set; }
        public Image Image { get; set; }
        public string Summary { get; set; }
        public Links _Links { get; set; }
    }
}