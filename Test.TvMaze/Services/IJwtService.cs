using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using TvMaze.Project.Models;

namespace TvMaze.Project.Services
{
    public interface IJwtService
    {
        Token GenerarToken(string keySecret, IEnumerable<Claim> claims = null, JwtUser user = null);
        Token ObtenerInformacionToken(ClaimsIdentity claims);
        string DecodificarToken(string token);
        string ObtenerStringToken(ClaimsIdentity claims);
    }
}