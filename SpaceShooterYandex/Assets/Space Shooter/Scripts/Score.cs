using System;
using TMPro;
using UnityEngine;
using YG;

public class Score : MonoBehaviour {
    public int ScoreValue;
    public TextMeshProUGUI ScoreView;
    //public static int BestResult; // в пользу GamePrefs.best

    private void Start () {
        ScoreValue = 0;
        //GetBestResult(); // это уже не нужно, т.к. в GameManager загрузится весь блок данных GamePrefs
    }

    public static int GetBestResult () {
        // это тоже не нужно, т.к. есть GamePrefs.best
        //if (PlayerPrefs.HasKey("Best")) BestResult = PlayerPrefs.GetInt("Best");
        //return BestResult;
    return GamePrefs.best;
    }

    public void AddScore (int score) {
        ScoreValue += score;
        ScoreView.text = ScoreValue.ToString();
    }

    // Единственное место, где определяется рекорд и происходит его сохранение - после гибели игрока.
    public void CheckBestResult (int value) {
        // легаси
        //if (BestResult < value) {
        //    BestResult = value;
        //    PlayerPrefs.SetInt("Best", BestResult); // это уже не нужно
        //}
        if (value > GamePrefs.best) {
            // локальное сохранение
            GamePrefs.best = value;
            GamePrefs.SaveSettings();
            // облачное сохранение
            GameManager.savedGame.highscore = value;
            YandexGame.SaveProgress();
            // обновление лидерборда втёмную
            // Конечно, надо сначала прочитать свой рекорд из лидерборда, и записывать обратно только если он превышен.
            // Но у нас уже есть облачное сохранение, поэтому используем только его
            YandexGame.NewLeaderboardScores("highscores", value);
        }
    }

}

