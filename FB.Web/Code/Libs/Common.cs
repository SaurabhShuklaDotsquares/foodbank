using Microsoft.AspNetCore.Mvc.Rendering;
using FB.Core;
using FB.Data.Models;
using FB.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace FB.Web
{
    public static class Common
    {
        //private Common() { }

        public static string GetRandomPasswordSalt()
        {
            return Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10);
        }

        public static string RandomCode(int length)
        {
            System.Security.Cryptography.RandomNumberGenerator rng = System.Security.Cryptography.RandomNumberGenerator.Create();

            char[] chars = new char[length];

            //based on your requirment you can take only alphabets or number
            string validChars = "abcdefghijklmnopqrstuvwxyzABCEDFGHIJKLMNOPQRSTUVWXYZ1234567890";

            for (int i = 0; i < length; i++)
            {
                byte[] bytes = new byte[1];
                rng.GetBytes(bytes);

                Random rnd = new Random(bytes[0]);

                chars[i] = validChars[rnd.Next(0, 61)];
            }

            return (new string(chars));
        }

        public static string CreateRandomPassword(int passwordLength)
        {
            //return System.Web.Security.Membership.GeneratePassword(passwordLength, 2);
            string smallAlpha = "abcdefghijkmnopqrstuvwxyz";
            string bigAlpha = "ABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            string num = "0123456789";
            string spec = "!@$?_-";

            char[] chars = new char[passwordLength];
            Random rd = new Random();

            int i = 0;
            for (; i < 3; i++)
            {
                chars[i] = smallAlpha[rd.Next(0, smallAlpha.Length)];
            }

            for (; i < 6; i++)
            {
                chars[i] = bigAlpha[rd.Next(0, bigAlpha.Length)];
            }

            for (; i < 8; i++)
            {
                chars[i] = num[rd.Next(0, num.Length)];
            }

            for (; i < 10; i++)
            {
                chars[i] = spec[rd.Next(0, spec.Length)];
            }

            return (new string(chars)).Shuffle();
        }

        public static string Shuffle(this string str)
        {
            char[] array = str.ToCharArray();
            Random rng = new Random();
            int n = array.Length;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                var value = array[k];
                array[k] = array[n];
                array[n] = value;
            }
            return new string(array);
        }

        public static string RandomAlphabatesCode(int length)
        {
            System.Security.Cryptography.RandomNumberGenerator rng = System.Security.Cryptography.RandomNumberGenerator.Create();

            char[] chars = new char[length];

            //based on your requirment you can take only alphabets or number
            string validChars = "abcdefghijklmnopqrstuvwxyz";

            for (int i = 0; i < length; i++)
            {
                byte[] bytes = new byte[1];
                rng.GetBytes(bytes);

                Random rnd = new Random(bytes[0]);

                chars[i] = validChars[rnd.Next(0, 25)];
            }

            return (new string(chars));
        }

        public static int GetColIndex(string cellAddress)
        {
            int ci = 0;
            cellAddress = cellAddress.ToUpper();
            for (int ix = 0; ix < cellAddress.Length && cellAddress[ix] >= 'A'; ix++)
                ci = (ci * 26) + ((int)cellAddress[ix] - 64);
            return ci;
        }

        public static int GetRowIndex(string cellAddress)
        {
            string col = cellAddress;
            int startIndex = col.IndexOfAny("0123456789".ToCharArray());
            string column = col.Substring(0, startIndex);
            int row = Int32.Parse(col.Substring(startIndex));
            return row;
        }
       

        public static Dictionary<string, string> GetColumns()
        {
            Dictionary<string, string> columns = new Dictionary<string, string>();
            columns.Add("Title", "1");
            columns.Add("Forenames", "1");
            columns.Add("Surname", "1");
            columns.Add("Email", "0");
            columns.Add("Gender", "1");
            columns.Add("UserName", "1");
            columns.Add("Password", "1");
            columns.Add("PasswordQuestion", "1");
            columns.Add("PasswordAnswer", "1");
            columns.Add("HouseNumber", "0");
            columns.Add("StreetName", "1");
            columns.Add("City", "1");
            columns.Add("Postcode", "1");
            columns.Add("Reference", "1");

            return columns;
        }

        public static decimal GetClaimAmountByTaxRate(decimal? amount, decimal? taxRate)
        {
            decimal amt = (amount ?? 0) * ((taxRate ?? 0) / (100 - (taxRate ?? 0)));
            return Convert.ToDecimal(amt.ToString("n2"));
            //return Convert.ToDecimal(Math.Round(amt, 2).ToString("n2"));
        }

        public static String GenerateHtmlFromUrl(string path)
        {
            StringBuilder sb = new StringBuilder();
            // used on each read operation
            byte[] buf = new byte[8192];

            // prepare the web page we will be asking for
            HttpWebRequest request = (HttpWebRequest)
                WebRequest.Create(path);

            // execute the request
            HttpWebResponse response = (HttpWebResponse)
                request.GetResponse();

            // we will read data via the response stream
            Stream resStream = response.GetResponseStream();

            string tempString = null;
            int count = 0;

            do
            {
                // fill the buffer with data
                count = resStream.Read(buf, 0, buf.Length);

                // make sure we read some data
                if (count != 0)
                {
                    // translate from bytes to ASCII text
                    tempString = Encoding.ASCII.GetString(buf, 0, count);

                    // continue building the string
                    sb.Append(tempString);
                }
            }
            while (count > 0); // any more data to read?

            return sb.ToString();
        }

        public static T ExecuteHTTPRequest<T>(string requestUrl, string methodType, Dictionary<string, string> headers = null, dynamic data = null) where T : class
        {
            WebRequest request = WebRequest.Create(requestUrl);
            if (headers != null)
            {
                foreach (KeyValuePair<string, string> header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            request.Method = methodType;
            if (data != null && methodType.ToLower().Contains("post"))
            {
                Stream reqStream = request.GetRequestStream();                
                string postData = JsonConvert.SerializeObject(data);
                byte[] postArray = Encoding.ASCII.GetBytes(postData);
                request.ContentType = "application/json";
                reqStream.Write(postArray, 0, postArray.Length);
                reqStream.Close();
            }

            WebResponse response = request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string responseText = reader.ReadLine();
            T goResponse = JsonConvert.DeserializeObject<T>(responseText);
            return goResponse;
        }

        public static List<UserDataAccessDto> GetDataAccessibility(int roleID, int userID)
        {
            List<UserDataAccessDto> userDataAccesslist = new List<UserDataAccessDto>();
            try
            {
                if (roleID != (int)UserRoles.SuperAdmin && roleID != (int)UserRoles.Internal && roleID != (int)UserRoles.Branch && roleID != (int)UserRoles.Donor && roleID != (int)UserRoles.TechnicalSupport)
                {
                    using (MMOOlgaDevContext context = new MMOOlgaDevContext())
                    {
                        var dataAccessibility = context.FoodbankUserDataAccessibility.Where(x => x.UserId == userID).ToList();
                        var branchIDs = new Dictionary<int, List<int>>();
                        if (dataAccessibility.Count > 0)
                        {
                            foreach (var data in dataAccessibility.Where(p => p.CentralOfficeId != null).GroupBy(x => x.CentralOfficeId).ToList())
                            {
                                foreach (var item in data.Where(p => p.CharityId != null).GroupBy(x => x.CharityId).ToList())
                                {
                                    branchIDs.Add(item.Key.Value, item.Where(p => p.BranchId != null).Select(x => x.BranchId.Value).ToList());
                                }
                                userDataAccesslist.Add(new UserDataAccessDto
                                {
                                    CentralOfficeID = data.Key.Value,
                                    CharityBranches = branchIDs
                                });
                            }
                        }
                    }
                }
            }
            catch 
            {
            }
            return userDataAccesslist;
        }

        public static string GetHtml(string argTemplateDocument, string[] _ArrValues)
        {
            int i;
            StreamReader filePtr;
            string fileData = argTemplateDocument;
            var templatePath = Path.Combine(ContextProvider.HostEnvironment.WebRootPath + "/EmailTemplates/");
            filePtr = System.IO.File.OpenText(templatePath + argTemplateDocument);
            fileData = filePtr.ReadToEnd();
            if ((_ArrValues == null))
            {
                fileData = fileData.Replace("@copyrightyear@", DateTime.Now.Year.ToString());
                return fileData;
            }
            else
            {
                for (i = _ArrValues.GetLowerBound(0); i <= _ArrValues.GetUpperBound(0); i++)
                {
                    fileData = fileData.Replace("@v" + i.ToString() + "@", (string)_ArrValues[i]);
                }
                fileData = fileData.Replace("@copyrightyear@", DateTime.Now.Year.ToString());
                fileData = fileData.Replace("@headerlogo1@", SiteKeys.DomainName + "Content/images/device_logo.png");
                fileData = fileData.Replace("@headerlogo2@", SiteKeys.DomainName + "Content/images/MMO_Logo.png?h=40");
                return fileData;
            }
        }

        public static List<SelectListItem> AddNone(this List<SelectListItem> listSource, bool isInt = true)
        {
            if (listSource != null && listSource.Count() > 0)
            {
                listSource.Insert(0, new SelectListItem { Text = Constants.NoneText, Value = isInt ? Constants.NoneIntValue : Constants.NoneStringValue });
                return listSource;
            }
            else
                return listSource;
        }
    }
}