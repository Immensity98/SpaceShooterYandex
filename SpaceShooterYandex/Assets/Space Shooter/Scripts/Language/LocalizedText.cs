using UnityEngine;
using UnityEngine.UI;
using TMPro;

// 13:37 03.06.2019 добавляю поддержку TextMeshPro

public class LocalizedText : MonoBehaviour {
    public string localizedID = string.Empty;
	public string arg0, arg1, arg2; // 9:21 15.09.2021 строковые аргументы для форматирования
	private string larg0, larg1, larg2;

    void Start () {
        LocalizeText();
    }

	void OnEnable () => LanguageManager.languageChanged += LocalizeText;
	void OnDisable () => LanguageManager.languageChanged -= LocalizeText;

	private bool Key (string key) {
		return (!System.String.IsNullOrEmpty(key) && key.Contains("/"));
	}

    public void LocalizeText () {
		if (localizedID != "") {
			Text text = GetComponent<Text>();
			if (text != null) {
				text.text = LanguageManager.Get(localizedID);
			} else {
				TextMeshProUGUI tmp_text = GetComponent<TextMeshProUGUI>();
				if (tmp_text != null) {
					//tmp_text.text = LanguageManager.Instance.Get(localizedID);
					//tmp_text.SetText(LanguageManager.Instance.Get(localizedID)); // неудачная попытка заставить переварить теги Rich Text
					// 17.12.2021 попробую локализовать и аргументы
					if (Key(arg0)) larg0 = LanguageManager.Get(arg0);
					if (Key(arg1)) larg1 = LanguageManager.Get(arg1);
					if (Key(arg2)) larg2 = LanguageManager.Get(arg2);
					tmp_text.SetText(System.String.Format(LanguageManager.Get(localizedID), larg0, larg1, larg2));
				}
			}
		}
    }

	public void Clear () {
		Text text = GetComponent<Text>();
		if (text != null) text.text = string.Empty;
		else {
			TextMeshProUGUI tmp_text = GetComponent<TextMeshProUGUI>();
			if (tmp_text != null) tmp_text.text = string.Empty;
		}
	}

	public void Localize (string key, string val) {
		localizedID = key + "/" + val;
		LocalizeText();
	}

}