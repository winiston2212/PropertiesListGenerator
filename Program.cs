using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;
using System.Text;

namespace PropertiesListGenerator
{
    class Program
    {
        private static readonly List<string> Properties = new List<string>() {
            
        }; 

        private static readonly Dictionary<string, PropertyType> Types = new Dictionary<string, PropertyType>() {
            {"uint32", PropertyType.UInt32},
            {"string", PropertyType.String},

        };

        static void Main(string[] args)
        {
            GetTypeFromDocs("System.AppZoneIdentifier");
        }

        private  static string GetPropertyName(string path) {
            var array = path.Split('.');
            var name = array[array.Length-1];
            name = string.Concat(name.Select(x => Char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' ');
            return name;
        }

        private static PropertyType GetTypeFromDocs(string property) {
            var url = "https://docs.microsoft.com/en-us/windows/win32/properties/props-" + property.Replace('.', '-').ToLower();
            var data = new System.Net.WebClient().DownloadString(url);
            var index = data.IndexOf("type = ");
            var type = data.Substring(index).Split("\n")[0];
            Console.WriteLine(type);

            return GetType(type);
        }

        private static PropertyType GetType(string type) {
            foreach (var item in Types)
            {
                if(item.Key.ToLower().Contains(type.ToLower()))
                    return item.Value;
            }

            return PropertyType.String;
        }

        private enum PropertyType {
            UInt32,
            String,
            MultivalueString,
        }
    }
}
