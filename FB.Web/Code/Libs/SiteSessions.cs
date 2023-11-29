using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;


namespace FB.Web
{

    public static class MMOHttpContext
    {
        private static IServiceProvider services = null;

        /// <summary>
        /// Provides static access to the framework's services provider
        /// </summary>
        public static IServiceProvider Services
        {
            get { return services; }
            set
            {
                if (services != null)
                {
                    throw new Exception("Can't set once a value has already been set.");
                }
                services = value;
            }
        }
    }

    public static class SessionExtensions
    {
        public static void SetObjectAsJson<T>(this ISession session, string key, T value)
        {
            session.SetString(key, System.Text.Json.JsonSerializer.Serialize(value));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : System.Text.Json.JsonSerializer.Deserialize<T>(value);
        }
    }

    public class SiteSessions
    {
        public static Dictionary<string, object> SessionUser
        {
            get
            {
                ISession session = ContextProvider.HttpContext.Session;

                if (session.GetObjectFromJson<Dictionary<string, object>>(nameof(SessionUser)) == null)
                    session.SetObjectAsJson(nameof(SessionUser), new Dictionary<string, object>());

                return session.GetObjectFromJson<Dictionary<string, object>>(nameof(SessionUser));
            }
        }

        public static void UpdateSessionUser(Dictionary<string, object> data)
        {
            if (data == null)
                data = new Dictionary<string, object>();

            ContextProvider.HttpContext.Session.SetObjectAsJson(nameof(SessionUser), data);
        }


        public static void RemoveReportSession(string key)
        {
            string keyname = key;
            ContextProvider.HttpContext.Session.Remove(key);
        }

        public static void SetReportSession<T>(string key, T data)
        {
            RemoveReportSession(key);
            ContextProvider.HttpContext.Session.SetObjectAsJson(key, data);
        }


        public static T GetReportSession<T>(string key)
        {
            return (T)ContextProvider.HttpContext.Session.GetObjectFromJson<T>(key);
        }
        //public static CharityGoCardLessPlanDto DonnerPlan
        //{
        //    get { return ContextProvider.HttpContext.Session.GetObjectFromJson<CharityGoCardLessPlanDto>("DonnerPlan") == null ? null : ContextProvider.HttpContext.Session.GetObjectFromJson<CharityGoCardLessPlanDto>("DonnerPlan"); }
        //    set { ContextProvider.HttpContext.Session.SetObjectAsJson<CharityGoCardLessPlanDto>("DonnerPlan", value); }
        //}
    }
}
