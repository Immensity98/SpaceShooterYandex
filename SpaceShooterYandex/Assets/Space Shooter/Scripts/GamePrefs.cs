#define social

// просто чтобы легче было контролировать вызовы обёртки
// Сама же обёртка нужна для того, чтобы сохранение не сбрасывалось после обновления на ЯИ
// https://roundedbox.ru/#100_and_1_problem_of_unity_in_webgl/troubles_with_saves
public static class Prefs {
#if social
	public static float GetFloat (string key) { return Social.PlayerPrefs.GetFloat(key); }
	public static void SetFloat (string key, float value) { Social.PlayerPrefs.SetFloat(key, value); }
	public static int GetInt (string key) { return Social.PlayerPrefs.GetInt(key); }
	public static void SetInt (string key, int value) { Social.PlayerPrefs.SetInt(key, value); }
	public static string GetString (string key) { return Social.PlayerPrefs.GetString(key); }
	public static void SetString (string key, string value) { Social.PlayerPrefs.SetString(key, value); }
	public static bool HasKey (string key) { return Social.PlayerPrefs.HasKey(key); }
	public static void Save () { Social.PlayerPrefs.Save(); }
	public static void DeleteKey (string key) { Social.PlayerPrefs.DeleteKey(key); }
#else
	public static float GetFloat (string key) { return PlayerPrefs.GetFloat(key); }
	public static void SetFloat (string key, float value) { PlayerPrefs.SetFloat(key, value); }
	public static int GetInt (string key) { return PlayerPrefs.GetInt(key); }
	public static void SetInt (string key, int value) { PlayerPrefs.SetInt(key, value); }
	public static string GetString (string key) { return PlayerPrefs.GetString(key); }
	public static void SetString (string key, string value) { PlayerPrefs.SetString(key, value); }
	public static bool HasKey (string key) { return PlayerPrefs.HasKey(key); }
	public static void Save () { PlayerPrefs.Save(); }
	public static void DeleteKey (string key) { PlayerPrefs.DeleteKey(key); }
#endif
}

// это просто обёртка, сахар для PlayerPrefs
public static class GamePrefs {
    // - личный рекорд
    public static int best;
    static string bestName = "Best";
	// - music volume
	public static float musicVolume;
	static float musicVolumeDefault = 0.7f;
	static string musicVolumeName = "musicVolume";
	// - effects volume
	public static float effectsVolume;
	static float effectsVolumeDefault = 0.7f;
	static string effectsVolumeName = "effectsVolume";
	// - global audio
	public static bool audioOn = true;
	static string audioOnName = "audioOn";
	
	// метод должен сработать однократно, в самом начале игры, на первой сцене.
	public static void LoadSettings () {
		// - личный рекорд
		if (Prefs.HasKey(bestName)) best = Prefs.GetInt(bestName); else best = 0;
		// - musicVolume
		if (Prefs.HasKey(musicVolumeName)) musicVolume = Prefs.GetFloat(musicVolumeName); else musicVolume = musicVolumeDefault;
		// - effectsVolume
		if (Prefs.HasKey(effectsVolumeName)) effectsVolume = Prefs.GetFloat(effectsVolumeName); else effectsVolume = effectsVolumeDefault;
		// - global audio
		if (Prefs.HasKey(audioOnName)) {
			audioOn = Prefs.GetInt(audioOnName) == 1 ? true : false; 
		} else audioOn = true;		
	}
	
	public static void SaveSettings () {
		Prefs.SetInt(bestName, best);
		Prefs.SetInt(audioOnName, audioOn ? 1 : 0);
		Prefs.SetFloat(musicVolumeName, musicVolume);
		Prefs.SetFloat(effectsVolumeName, effectsVolume);
		// общее сохранение
		Prefs.Save();
	}
}
