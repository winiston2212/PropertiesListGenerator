# PropertiesListGenerator
This application is used to generate code for a properties list for the Files UWP application. Property type is obtained through the MS docs website.

# Sample Input
```
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
```
# Output
```
using System; 
using System.Collections.Generic;
class PropertiesList {
	public List<PropertiesListItem> Properties = new List<PropertiesListItem>() {
		new PropertyListItem() {
			Name = "Latitude",
			Path = "System.GPS.Latitude",
			Section = "GPS",
			Type = "String",
		},
		new PropertyListItem() {
			Name = "Latitude Ref",
			Path = "System.GPS.LatitudeRef",
			Section = "GPS",
			Type = "String",
		},
		new PropertyListItem() {
			Name = "Longitude",
			Path = "System.GPS.Longitude",
			Section = "GPS",
			Type = "String",
		},
		new PropertyListItem() {
			Name = "Longitude Ref",
			Path = "System.GPS.LongitudeRef",
			Section = "GPS",
			Type = "String",
		},
		new PropertyListItem() {
			Name = "Altitude",
			Path = "System.GPS.Altitude",
			Section = "GPS",
			Type = "Double",
		},
		new PropertyListItem() {
			Name = "Exposure Time",
			Path = "System.Photo.ExposureTime",
			Section = "Photo",
			Type = "Double",
		},
		new PropertyListItem() {
			Name = "Focal Length",
			Path = "System.Photo.FocalLength",
			Section = "Photo",
			Type = "Double",
		},
		new PropertyListItem() {
			Name = "Aperture",
			Path = "System.Photo.Aperture",
			Section = "Photo",
			Type = "Double",
		},
		new PropertyListItem() {
			Name = "Date Taken",
			Path = "System.Photo.DateTaken",
			Section = "Photo",
			Type = "DateTime",
		},
		new PropertyListItem() {
			Name = "Channel Count",
			Path = "System.Audio.ChannelCount",
			Section = "Audio",
			Type = "UInt32",
		},
		new PropertyListItem() {
			Name = "Encoding Bitrate",
			Path = "System.Audio.EncodingBitrate",
			Section = "Audio",
			Type = "UInt32",
		},
		new PropertyListItem() {
			Name = "Compression",
			Path = "System.Audio.Compression",
			Section = "Audio",
			Type = "String",
		},
		new PropertyListItem() {
			Name = "Format",
			Path = "System.Audio.Format",
			Section = "Audio",
			Type = "String",
		},
		new PropertyListItem() {
			Name = "Sample Rate",
			Path = "System.Audio.SampleRate",
			Section = "Audio",
			Type = "UInt32",
		},
		new PropertyListItem() {
			Name = "Album I D",
			Path = "System.Music.AlbumID",
			Section = "Music",
			Type = "String",
		},
		new PropertyListItem() {
			Name = "Display Artist",
			Path = "System.Music.DisplayArtist",
			Section = "Music",
			Type = "String",
		},
		new PropertyListItem() {
			Name = "Creator Application",
			Path = "System.Media.CreatorApplication",
			Section = "Media",
			Type = "String",
		},
	};
}
```
