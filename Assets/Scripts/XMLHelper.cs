using UnityEngine;
using System.IO;
using System.Xml.Serialization;

namespace datatype
{
	public class Level
	{
		[XmlAttribute("index")] public int index;
		[XmlAttribute("state")] public bool state;
		
		public Level()
		{
			this.index = 1;
			this.state = false;
		}

		public Level(int index, bool state = false)
		{
			this.index = index;
			this.state = state;
		}

		public static string path = Application.persistentDataPath + "/progress.xml";
	}
}

public class XMLHelper
{
	public static void Save<T>(ref T[] data, string path)
	{
		FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write);
		XmlSerializer serializer = new XmlSerializer(typeof(T[]));
		XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
		namespaces.Add("", "");
		serializer.Serialize(stream, data, namespaces);
		stream.Close();
	}

	public static T[] Load<T>(string path)
	{
		if (File.Exists(path))
		{
			FileStream stream = new FileStream(path, FileMode.Open , FileAccess.Read);
			XmlSerializer serializer = new XmlSerializer(typeof(T[]));
			T[] data = (T[])serializer.Deserialize(stream);
			stream.Close();
			return data;
		}
		return new T[0];
	}
}