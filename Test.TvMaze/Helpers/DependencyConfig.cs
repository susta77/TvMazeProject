using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bogem.Hedyla.Helpers
{
    public static class DependencyConfig
    {
        public static IJwtService JwtService { get; private set; }
        private readonly IJwtService _JwtService { get; set; }



        static DependencyConfig(IJwtService JwtService)
        {


            _JwtService = JwtService;
        }
    }
}