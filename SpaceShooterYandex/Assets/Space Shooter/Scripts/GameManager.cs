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
            if (savedGame.highscore > 0) GamePrefs.best = savedGame.highscore; // а тут приоритет облачных данных
            // а в режиме инкогнито (не входить в учётку ЯИ) там всегда нули
            // Поэтому если в облаке нули, то оставляем локальное сохранение.
        }
        //print("GameManager.SyncData, synced GamePrefs.best = " + GamePrefs.best);
    }

    private void Start () {
        Application.targetFrameRate = 60; // на всякий случай.
        // локализация тут нужна только в редакторе, для отладки
        // Загрузка локали по языку платформы в другом месте
        #if UNITY_EDITOR
        if (debugLanguage != "") languageManager.LoadLanguage(debugLanguage);
        #endif
        // из локального сохранения
        GamePrefs.LoadSettings(); // один раз на старте игры загружаем все настройки и личный рекорд.
        //print("GameManager.Start, from local save GamePrefs.best = " + GamePrefs.best);
        // облачное сохранение заполняется из локального хранилища, ведь мы ещё не знаем, будет ли что-то в LoadFromYandex
        // а если savedGame уже не null, это значит, что LoadFromYandex уже сработал и данные из облака получены.
        // Это значит, что и в локальном сохранении надо использовать те же данные
        SyncData();
        started = true;
        YandexGame.GameReadyAPI();
    }

    // Проблема: LoadFromYandex может произойти до Start, может и после, а может и вообще не случиться.
    // Поэтому надо понять, где хранятся приоритетные данные - в облаке или в GamePrefs
    void LoadFromYandex () {
        //print("GameManager.LoadFromYandex");
        languageManager.LoadLanguage(YG.YandexGame.EnvironmentData.language);
		// Загрузка рекорда из облака яндекса.
		savedGame = YandexGame.savesData.savedGame;
        // Как понять, что там ещё не было никакого сохранения?
        // Если Start ещё не был, то нет смысла заново создавать, т.к. в коде Start уже есть всё, что нужно
        // А если Start произошёл, то надо восстановить поломанное
		if (started) SyncData();
	}

}
