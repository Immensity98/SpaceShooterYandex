using System;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int ScoreValue;
    public TextMeshProUGUI ScoreView;
    public static int BestResult;

    private void Start()
    {
        ScoreValue = 0;  
        GetBestResult();
    }

    public static int GetBestResult()
    {
        if (PlayerPrefs.HasKey("Best"))
        {
            BestResult = PlayerPrefs.GetInt("Best");
        }

        return BestResult;
    }

    public void AddScore(int score)
    {
        ScoreValue += score;
        ScoreView.text = ScoreValue.ToString();
    }

    public void CheckBestResult(int value)
    {
        if (BestResult < value)
        {
            BestResult = value; 
            PlayerPrefs.SetInt("Best", BestResult); 
        }
    }

}

