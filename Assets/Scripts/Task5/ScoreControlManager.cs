using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreControlManager : MonoBehaviour
{
    //public Text scoreText;
    //public Text dropCountText;
    //public Text remainingTimeText;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI remainingTimeText;

    private int score = 0;
    private int dropCount = 0;
    private GameSettingsManager gameSettingsManager;

    private void Awake()
    {
        gameSettingsManager = FindObjectOfType<GameSettingsManager>();
    }
    public void IncreaseScore()
    {
        score++;
        UpdateScore();
    }

    public void IncreaseDropCount()
    {
        dropCount++;
        UpdateScore();
    }

    public void UpdateRemainingTime(string time)
    {
        remainingTimeText.text = time;
        UpdateTime();
    }

    public void ResetCounters()
    {
        score = 0;
        dropCount = 0;
        remainingTimeText.text = "00:00";
        UpdateScore();
        UpdateTime();
    }

    private void UpdateScore()
    {
        scoreText.text = score.ToString();
        //dropCountText.text = dropCount.ToString();
    }


    private void UpdateTime() 
    /*
        If update time is called inside UpdateScore()
        it will be delayed when IncreaseDropCount and UpdateRemainingTime are called together
    */
    {
        remainingTimeText.text = remainingTimeText.text;
    }

    public int GetScore()
    {
        return score;
    }

    public void HideScroe()
    {
        remainingTimeText.text = "";
        scoreText.text = "";
    }
}