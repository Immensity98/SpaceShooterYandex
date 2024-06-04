using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Xml.Serialization; 
using System.Text;
using System;
using UnityEngine;

// 10:02 14.03.2017 модуль с общими процедурами для работы с XML

public static class XmlKit {

    public static XmlDocument Open (string data) {
		// 9:12 02.04.2019 проба чтения через ридер с настройкой игнорирования комментариев
		XmlReaderSettings readerSettings = new XmlReaderSettings();
		readerSettings.IgnoreComments = true;
		XmlReader reader = XmlReader.Create(new StringReader(data), readerSettings);
		//
        XmlDocument xmlDoc = new XmlDocument ();
		//xmlDoc.Load(new StringReader(data));
		xmlDoc.Load(reader);
	return xmlDoc;
	}	

	public static string UTF8ByteArrayToString(byte[] characters) {
		UTF8Encoding encoding = new UTF8Encoding(); 
		string constructedString = encoding.GetString(characters); 
	return (constructedString); 
	} 
 
	public static byte[] StringToUTF8ByteArray(string pXmlString) { 
		UTF8Encoding encoding = new UTF8Encoding(); 
		byte[] byteArray = encoding.GetBytes(pXmlString); 
	return byteArray;
	} 

	public static string SerializeObject(Type type, object pObject) { 
		string XmlizedString = null; 
		MemoryStream memoryStream = new MemoryStream(); 
		XmlSerializer xs = new XmlSerializer(type); 
		XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8); 
		xs.Serialize(xmlTextWriter, pObject); 
		memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
		XmlizedString = XmlKit.UTF8ByteArrayToString(memoryStream.ToArray()); 
	return XmlizedString; 
	}
 
	// Here we deserialize it back into its original form 
	public static object DeserializeObject(Type type, string pXmlizedString) { 
		XmlSerializer xs = new XmlSerializer(type);
		MemoryStream memoryStream = new MemoryStream(XmlKit.StringToUTF8ByteArray(pXmlizedString)); 
		//XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8); 
	return xs.Deserialize(memoryStream);
	}

	public static object DeserializeFromFile (Type type, string path) {
		if (File.Exists(path)) {
			StreamReader r = File.OpenText(path);
			string data = r.ReadToEnd(); 
			r.Close(); 
			if (data != "") return DeserializeObject(type, data);		
		}
	return null;
	}

	// выдрано из туториала https://levelup.gitconnected.com/unity-reading-external-xml-files-ed199df66288
	public static T ImportXml<T> (string path) {
		try {
			XmlSerializer serializer = new XmlSerializer(typeof(T));
			using (var stream = new FileStream(path, FileMode.Open)) {
				return (T)serializer.Deserialize(stream);
			}
		} catch (Exception e) {
			Debug.LogError("Exception importing xml file: " + e);
			return default;
		}
	}

	public static T Deserialize<T> (TextAsset ta) {
		XmlSerializer serializer = new XmlSerializer(typeof(T));
		using (System.IO.StringReader reader = new System.IO.StringReader(ta.text)) {
			return (T)serializer.Deserialize(reader);
		}
	}

	public static void SerializeToFile (Type type, object pObject, string path) {
		StreamWriter writer;
		FileInfo t = new FileInfo(path); 
		if (!t.Exists) {
			if (!System.IO.Directory.Exists(t.DirectoryName)) System.IO.Directory.CreateDirectory(t.DirectoryName);
			writer = t.CreateText();
		}
		else { 
			t.Delete(); 
			writer = t.CreateText(); 
		}
		writer.Write(SerializeObject(type, pObject));
		writer.Close(); 
	} 

	// 13:16 15.05.2018 проверяет атрибут enabled = "true", отсутствие атрибута по умолчанию = true
	public static bool Enabled (XmlNode node) {
		XmlAttribute enable = node.Attributes["enable"]; // 19:15 14.05.2018 флаг доступности базы
		return ((enable == null) || (enable != null) && (enable.Value == "true"));
	}
	
}
