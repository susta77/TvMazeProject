using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TvMaze.Project.Models
{
    public class JwtUser
    {
        public Guid? UserGuid { get; set; }
        public Guid? CompanyGuid { get; set; }
    }
}