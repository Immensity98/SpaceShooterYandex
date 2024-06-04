using KulibinSpace.MessageBus;
using UnityEngine;
using UnityEngine.Events;
using YG;

public class GameManager : MonoBehaviour {
    // после перезапуска сцены подписчики заново подпишутся
    public static UnityAction startGameAction; 
    public static UnityAction restartGameAction;
    public static SavedGame savedGame;
    public string debugLanguage = "";
    public LanguageManager languageManager; // экземпляр скриптуемого
    private bool started = false; // чисто для LoadFromYandex
    static GameManager instance;

	void Awake () {
		if (instance == null) {
			DontDestroyOnLoad(gameObject);
			instance = this;
		} else if (instance != this) {
			Destroy(gameObject);
		}
	}

    private void OnEnable() => YG.YandexGame.GetDataEvent += LoadFromYandex;
    private void OnDisable() => YG.YandexGame.GetDataEvent -= LoadFromYandex;

    // идентичный код в Start и в LoadFromYandex
    private void SyncData () {
        if (savedGame == null) {
            savedGame = new SavedGame { highscore = GamePrefs.best }; // очевидный приоритет локальных данных в условиях неизвестности с облаком.
        } else {
            GamePrefs.best = savedGame.highscore; // а тут приоритет облачных данных
        }
    }

    private void Start () {
        // локализация
        if (debugLanguage != "") languageManager.LoadLanguage(debugLanguage);
        // локальное сохранение
        GamePrefs.LoadSettings(); // один раз на старте игры загружаем все настройки и личный рекорд.
        // облачное сохранение заполняется из локального хранилища, ведь мы ещё не знаем, будет ли что-то в LoadFromYandex
        // а если savedGame уже не null, это значит, что LoadFromYandex уже сработал и данные из облака получены.
        // Это значит, что и в локальном сохранении надо использовать те же данные
        SyncData();
        started = true;
    }

    // Проблема: LoadFromYandex может произойти до Start, может и после, а может и вообще не случиться.
    // Поэтому надо понять, где хранятся приоритетные данные - в облаке или в GamePrefs
    void LoadFromYandex () {
        languageManager.LoadLanguage(YG.YandexGame.EnvironmentData.language);
		// Загрузка рекорда из облака яндекса.
		savedGame = YandexGame.savesData.savedGame;
        // Если Start ещё не был, то нет смысла заново создавать, т.к. в коде Start уже есть всё, что нужно
        // А если Start произошёл, то надо восстановить поломанное
		if (started) SyncData();
	}

}
