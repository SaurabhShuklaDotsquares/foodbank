using System;
using System.Collections.Generic;
using System.Linq;

namespace FB.ExportReport
{
    public static class Utility
    {
        public static List<ColumnInfo> GetPropertyNames(object obj)
        {
            Type type = obj.GetType().GetGenericArguments()[0];
            if (type.IsClass)
            {
                return type.GetProperties().Select(p => new ColumnInfo
                {
                    Label = p.Name,
                    Name = p.Name
                }).ToList();
            }

            return null;
        }
    }
}
