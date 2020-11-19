using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PropertiesListGenerator
{
    class Program
    {
        private static readonly List<string> Properties = new List<string>() {
            //Core
            "System.RatingText",
            "System.ItemFolderPathDisplay",
            "System.ItemTypeText",
            "System.Title",
            "System.Subject",
            "System.Comment",
            "System.Copyright",
            "System.DateCreated",
            "System.DateModified",

            //Image
            "System.Image.ImageID",
            "System.Image.CompressedBitsPerPixel",
            "System.Image.BitDepth",
            "System.Image.Dimensions",
            "System.Image.HorizontalResolution",
            "System.Image.VerticalResolution",
            "System.Image.CompressionText",
            "System.Image.ResolutionUnit",
            "System.Image.HorizontalSize",
            "System.Image.VerticalSize",

            //GPS
            "System.GPS.Latitude",
            "System.GPS.LatitudeRef",
            "System.GPS.Longitude",
            "System.GPS.LongitudeRef",
            "System.GPS.Altitude",

            //Photo
            "System.Photo.CameraManufacturer",
            "System.Photo.CameraModel",
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
            "System.Music.AlbumArtist",
            "System.Music.AlbumTitle",
            "System.Music.Artist",
            "System.Music.BeatsPerMinute",
            "System.Music.Composer",
            "System.Music.Conductor",
            "System.Music.DiscNumber",
            "System.Music.Genre",
            "System.Music.TrackNumber",

            //Media
            "System.Media.AverageLevel",
            "System.Media.Duration",
            "System.Media.FrameCount",
            "System.Media.ProtectionType",

            //Media
            "System.Media.AuthorUrl",
            "System.Media.ContentDistributor",
            "System.Media.DateReleased",
            "System.Media.SeriesName",
            "System.Media.SeasonNumber",
            "System.Media.EpisodeNumber",
            "System.Media.Producer",
            "System.Media.PromotionUrl",
            "System.Media.ProviderStyle",
            "System.Media.Publisher",
            "System.Media.ThumbnailLargePath",
            "System.Media.ThumbnailLargeUri",
            "System.Media.ThumbnailSmallPath",
            "System.Media.ThumbnailSmallUri",
            "System.Media.UniqueFileIdentifier",
            "System.Media.UserWebUrl",
            "System.Media.Writer",
            "System.Media.Year",
        };

        private static readonly Dictionary<string, string> Converters = new Dictionary<string, string>() {
            {"UInt32", "new UInt32ToString()"},
            {"Multivalue String", "new StringArrayToString()"},
            {"Double", "new DoubleToString()"},
            {"DateTime", "new DateTimeOffsetToString()"}

        };

        private static string FilePath = "C:\\Users\\winis\\OneDrive\\Programming\\GitHub\\PropertiesListGenerator\\bin\\FilePropertyList.cs";
        static void Main(string[] args)
        {
            var propertiesList = new List<PropertyItem>();
            foreach (var item in Properties)
            {
                var prop = new PropertyItem(item);
                propertiesList.Add(prop);
            }

            SaveData(propertiesList);
        }

        private static void SaveData(List<PropertyItem> items)
        {
            StreamWriter writer = File.CreateText(FilePath);
            writer.WriteLine("using System; \nusing System.Collections.Generic;");
            writer.WriteLine("class PropertiesList {");
            writer.WriteLine("\tpublic List<PropertiesListItem> Properties = new List<PropertiesListItem>() {");
            foreach (var item in items)
            {
                writer.WriteLine("\t\t" + "new FileProperty() {");
                writer.WriteLine(string.Format("{0}Name = \"{1}\",", "\t\t\t", item.Name));
                writer.WriteLine(string.Format("{0}Property = \"{1}\",", "\t\t\t", item.Path));
                writer.WriteLine(string.Format("{0}Section = \"{1}\",", "\t\t\t", item.Section));
                if(!string.IsNullOrWhiteSpace(item.ConverterName))
                    writer.WriteLine(string.Format("{0}Converter = \"{1}\",", "\t\t\t", item.ConverterName));
                writer.WriteLine("\t\t" + "},");
            }
            writer.WriteLine("\t};");
            writer.WriteLine("}");

            writer.Close();
        }

        private enum PropertyType
        {
            UInt32,
            String,
            MultivalueString,
            Double,
            DateTime,
        }

        private class PropertyItem
        {
            public PropertyType Type { get; set; }
            public string Name { get; set; }
            public string Path { get; set; }
            public string ConverterName { get; set; }
            public string ControlType { get; set; }
            public string Section { get; set; }
            public bool IsReadOnly { get; set; }

            public PropertyItem(string path)
            {
                Path = path;
                SetPropertyName();
                SetTypeFromDocs();
                SetSection();
            }
            public PropertyItem()
            {

            }

            private void SetPropertyName()
            {
                var array = Path.Split('.');
                var name = array[array.Length - 1];
                name = string.Concat(name.Select(x => Char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' ');
                Name = name;
            }
            private void SetTypeFromDocs()
            {
                var url = "https://docs.microsoft.com/en-us/windows/win32/properties/props-" + Path.Replace('.', '-').ToLower();
                var data = new System.Net.WebClient().DownloadString(url);
                var type = data.Split("type = ")[1].Split("\n")[0];
                ConverterName = GetConverter(type: type);
            }

            private void SetSection() {
                Section = Path.Split('.')[1];
            }

            private string GetConverter(string type) {
                foreach (var item in Converters)
                {
                    if (item.Key.Trim().ToLower().Equals(type.ToLower()))
                        return item.Value;
                }
                return null;
            }
        }

    }
}
