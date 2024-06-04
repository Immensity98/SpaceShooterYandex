using UnityEngine;
using UnityEngine.Events;
using System.Xml;
using System;

public delegate void LanguageAction ();

[CreateAssetMenu(fileName = "New LanguageManager object", menuName = "ScriptableObjects/LanguageManager", order = 51)]
public class LanguageManager : ScriptableObject {

    public static event LanguageAction languageChanged;
    private static LanguageManager instance = null;

    public TextAsset[] locales;
    
    private static XmlElement root = null;

    void Awake () {
        instance = this;
    }

    public static string Get (string path) {
        if (root == null) return "no locale";
        try {
            XmlNode node = root.SelectSingleNode(path); 
            if (node == null) {
                return path;
            } else {
                string value = node.InnerText;
                value = value.Replace("\\n", "\n");
                return value;
            }
        }
        catch (System.Exception e) {
            Debug.Log(e.Message);
            return path;
        }
    }

    // язык приходит в виде "ru" или "en" и т.п., совпадающей с именем файла локали в массиве locales
    public void LoadLanguage (string language) {
        TextAsset locale = Array.Find(locales, p => p.name == language);
        if (locale != null) {
            XmlDocument mainDoc = XmlKit.Open(locale.text);
            if (mainDoc != null) {
                root = mainDoc.DocumentElement;
                languageChanged();
            } else {
                Debug.Log("No xml for locale " + language + " here!");
            }
        } else {
            Debug.Log("No locale " + language + " here!");
        }
    }
	
	
}