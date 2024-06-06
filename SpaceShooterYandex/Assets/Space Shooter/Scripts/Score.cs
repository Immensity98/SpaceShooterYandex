using System;
using TMPro;
using UnityEngine;
using YG;

public class Score : MonoBehaviour {
    public int ScoreValue;
    public TextMeshProUGUI ScoreView;
    public TextMeshProUGUI highScoreView;
    //public static int BestResult; // в пользу GamePrefs.best
    string scoreLocale;
    [Header("Устанавливать только для очищающей сборки!")]
    public bool forceSaveNotBest = false; // сбросить свои рекорды.
    public GameObject forceSaveBanner;

    private void Start () {
        if (forceSaveNotBest) forceSaveBanner.SetActive(true);
        ScoreValue = 0;
        //GetBestResult(); // это уже не нужно, т.к. в GameManager загрузится весь блок данных GamePrefs
        scoreLocale = LanguageManager.Get("score");
        AddScore(0);
    }

    public static int GetBestResult () {
        // это тоже не нужно, т.к. есть GamePrefs.best
        //if (PlayerPrefs.HasKey("Best")) BestResult = PlayerPrefs.GetInt("Best");
        //return BestResult;
    return GamePrefs.best;
    }

    public void UpdateHighscore () {
        highScoreView.text = string.Format(LanguageManager.Get("highscore"), GamePrefs.best);
    }

    public void AddScore (int score) {
        ScoreValue += score;
        ScoreView.text = string.Format(scoreLocale, ScoreValue); // оптимизация
        //ScoreView.text = ScoreValue.ToString();
    }

    // Единственное место, где определяется рекорд и происходит его сохранение - после гибели игрока.
    public void CheckBestResult (int value) {
        // легаси
        //if (BestResult < value) {
        //    BestResult = value;
        //    PlayerPrefs.SetInt("Best", BestResult); // это уже не нужно
        //}
        //print("CheckBestResult " + value + ", GamePrefs.best = " + GamePrefs.best);
        if ((!forceSaveNotBest && value > GamePrefs.best) || forceSaveNotBest) {
            // локальное сохранение
            GamePrefs.best = value;
            GamePrefs.SaveSettings();
            // облачное сохранение
            GameManager.savedGame.highscore = value;
            YandexGame.SaveProgress();
            // обновление лидерборда "втёмную"
            // Конечно, надо сначала прочитать свой рекорд из лидерборда, и записывать обратно только если он превышен.
            // Но у нас уже есть облачное сохранение, поэтому используем только его
            YandexGame.NewLeaderboardScores("highscores", value);
        }
    }

}

