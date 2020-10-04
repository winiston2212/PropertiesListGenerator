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
            //GPS
            "System.GPS.Latitude",
            "System.GPS.LatitudeRef",
            "System.GPS.Longitude",
            "System.GPS.LongitudeRef",
            "System.GPS.Altitude",

            //Photo
            "System.Photo.ExposureTime",
            "System.Photo.FocalLength",
            "System.Photo.Aperture",
            "System.Photo.DateTaken",

            //Audio
            "System.Audio.ChannelCount",
            "System.Audio.EncodingBitrate",
            "System.Audio.Compression",
            "System.Audio.Format",
            "System.Audio.SampleRate",

            //Music
            "System.Music.AlbumID",
            "System.Music.DisplayArtist",
            "System.Media.CreatorApplication",

        }; 

        private static readonly Dictionary<string, PropertyType> Types = new Dictionary<string, PropertyType>() {
            {"UInt32", PropertyType.UInt32},
            {"String", PropertyType.String},
            {"Multivalue String", PropertyType.MultivalueString},
            {"Double", PropertyType.Double},
            {"DateTime", PropertyType.DateTime}

        };

        private static string FilePath = "C:\\Users\\winis\\Documents\\FilePropertyList.cs";
        static void Main(string[] args)
        {
            GetTypeFromDocs("System.AppZoneIdentifier");
            var propertiesList = new List<PropertyItem>();
            foreach (var item in Properties)
            {
                var prop = new PropertyItem();
                prop.Path = item;
                prop.Name = GetPropertyName(item);
                propertiesList.Add(prop);
            }

            SaveData(propertiesList);
        }

        private static void SaveData(List<PropertyItem> items) {
            StreamWriter writer = File.CreateText(FilePath);
            writer.WriteLine("using System; \nusing System.Collections.Generic;");
            writer.WriteLine("class PropertiesList {");
            writer.WriteLine("\tpublic List<PropertiesListItem> Properties = new List<PropertiesListItem>() {");
            foreach (var item in items)
            {
                writer.WriteLine("\t\t" + "new PropertyListItem() {");
                writer.WriteLine(string.Format("{0}Name = \"{1}\",", "\t\t\t", item.Name));
                writer.WriteLine(string.Format("{0}Path = \"{1}\",", "\t\t\t", item.Path));
                writer.WriteLine("\t\t" + "},");
            }
            writer.WriteLine("\t};");
            writer.WriteLine("}");

            writer.Close();
        } 
        private static string GetPropertyName(string path) {
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
            Double,
            DateTime,
        }

        private class PropertyItem {
            public PropertyType Type {get; set;}
            public string Name {get; set;}
            public string Path {get; set;}
            public string ConverterName {get; set;}
            public string ControlType {get; set;}
            public string Section {get; set;}
            public bool IsReadOnly {get; set;}
            
            public PropertyItem() {

            }
        }

    }
}
