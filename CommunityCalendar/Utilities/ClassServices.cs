using System;
using System.Reflection;

namespace CommunityCalendar.Utilities
{
    public class ClassServices : IClassServices
    {

        public Dictionary<string, object> PropsAsDict(object obj)
        {
            Dictionary<string, object> propsAsDict = new();
            PropertyInfo[] properties = obj.GetType().GetProperties();
            foreach (var p in properties)
            {
                propsAsDict.Add(p.Name, p.GetValue(obj));
            }
            return propsAsDict;
        }
    }
}

