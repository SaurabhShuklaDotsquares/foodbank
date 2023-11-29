using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FB.Web.Code.Libs
{
    public static class AzureAdKeys
    {
        private static IConfigurationSection _configuration;
        public static void Configure(IConfigurationSection configuration)
        {
            _configuration = configuration;
        }
        public static string ClientId => _configuration["ClientId"];
        public static string ClientSecret => _configuration["ClientSecret"];
        public static string RedirectURI => _configuration["RedirectURI"];
    }
}
