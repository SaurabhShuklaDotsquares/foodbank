using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Core
{
    public static class SiteKeys
    {
        private static IConfigurationSection _configuration;
        public static void Configure(IConfigurationSection configuration)
        {
            _configuration = configuration;
        }
        public static string DomainName => _configuration["DomainName"];
        public static string DomainUtilityApi => _configuration["DomainUtilityApi"];
        public static string GoogleRecaptchaSiteKey => _configuration["GoogleRecaptchaSiteKey"];
        public static string SMTPServer => _configuration["SMTPServer"];
        public static string SMTPUserName => _configuration["SMTPUserName"];
        public static string SMTPUserPassword => _configuration["SMTPUserPassword"];
        public static string EmailFrom => _configuration["EmailFrom"];
        public static string AdminEmail => _configuration["AdminEmail"];
        public static string BCC => _configuration["BCC"];
        public static string ErrorMail => _configuration["ErrorMail"];
        public static string Postcode4uKey => _configuration["Postcode4uKey"];
        public static string Postcode4uUser => _configuration["Postcode4uUser"];
        public static string ImagePath => _configuration["ImagePath"];
        public static string PasswordHashUpdateDate => _configuration["PasswordHashUpdateDate"];
        public static string PasswordOldHashExpiry => _configuration["PasswordOldHashExpiry"];

        public static string GoCardlessAppOauthUrl_SandBox => _configuration["GoCardlessAppOauthUrl_SandBox"]; 
        public static string GoCardlessAppClientId_SandBox => _configuration["GoCardlessAppClientId_SandBox"]; 
        public static string GoCardlessAppClientSecret_SandBox => _configuration["GoCardlessAppClientSecret_SandBox"];
        public static string GoCardlessAppAccessTokenUrl_SandBox => _configuration["GoCardlessAppAccessTokenUrl_SandBox"]; 

        public static string GoCardlessWebhookSecret => _configuration["GoCardlessWebhookSecret"];
        public static string GoCardlessAppOauthUrl => _configuration["GoCardlessAppOauthUrl"]; 
        public static string GoCardlessAppClientId => _configuration["GoCardlessAppClientId"];
        public static string GoCardlessAppClientSecret => _configuration["GoCardlessAppClientSecret"]; 
        public static string GoCardlessAppAccessTokenUrl => _configuration["GoCardlessAppAccessTokenUrl"];
        public static string ReCaptchaKey => _configuration["ReCaptchaKey"];
        public static string ReCaptchaSecret => _configuration["ReCaptchaSecret"];
        public static string DefaultCountryId => _configuration["DefaultCountryId"];
        public static string DefaultCountryName => _configuration["DefaultCountryName"];
    }
}
