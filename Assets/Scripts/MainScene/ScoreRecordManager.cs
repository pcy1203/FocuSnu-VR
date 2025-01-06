using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    // Task{i}Date{i}  : Task 1~5, Date 0~9 (0 as recent)
    // Task{i}Score{i} : Task 1~5, Score 0~9 (0 as recent)
    // Task{i}BestScore : Task 1~5
    // Task{i}AverageScore : Task 1~5
    // Task{i}PlayTime : Task 1~5

    private int taskNumber = 1;
    private int taskCount = 5;

    public GameObject[] uiObject;
    public GameObject[] scoreGraphObject;
    public TextMeshProUGUI[] dateText;
    public TextMeshProUGUI[] scoreText;

    private float maxGraphXValue = 340.0f;
    private float minGraphXValue = -300.0f;
    private float maxGraphYValue = 200.0f;
    private float minGraphYValue = -400.0f;

    // Start is called before the first frame update
    void Start()
    {
        // InitializeData();
        // GetDummyData();
        LoadAllTaskRecords();
        DrawGraph();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitializeData()
    {
        for (int taskNum = 1; taskNum <= taskCount; taskNum++)
        {   
            for (int scoreNum = 0; scoreNum < 10; scoreNum++)
            {
                PlayerPrefs.SetString("Task" + taskNum.ToString() + "Date" + scoreNum.ToString(), "");
                PlayerPrefs.SetInt("Task" + taskNum.ToString() + "Score" + scoreNum.ToString(), 0);
                PlayerPrefs.SetInt("Task" + taskNum.ToString() + "BestScore", 0);
            }
        }
    }

    private void GetDummyData()
    {
        // For Test
        PlayerPrefs.SetString("Task1Date9", "2024-11-21 11:00");
        PlayerPrefs.SetInt("Task1Score9", 20);
        PlayerPrefs.SetString("Task1Date8", "2024-11-21 11:10");
        PlayerPrefs.SetInt("Task1Score8", 40);
        PlayerPrefs.SetString("Task1Date7", "2024-11-21 11:20");
        PlayerPrefs.SetInt("Task1Score7", 10);
        PlayerPrefs.SetString("Task1Date6", "2024-11-21 11:30");
        PlayerPrefs.SetInt("Task1Score6", 70);
        PlayerPrefs.SetString("Task1Date5", "2024-11-21 11:40");
        PlayerPrefs.SetInt("Task1Score5", 110);
        PlayerPrefs.SetString("Task1Date4", "2024-11-21 11:50");
        PlayerPrefs.SetInt("Task1Score4", 40);
        PlayerPrefs.SetString("Task1Date3", "2024-11-21 12:00");
        PlayerPrefs.SetInt("Task1Score3", 60);
        PlayerPrefs.SetString("Task1Date2", "2024-11-21 13:00");
        PlayerPrefs.SetInt("Task1Score2", 50);
        PlayerPrefs.SetString("Task1Date1", "2024-11-21 14:00");
        PlayerPrefs.SetInt("Task1Score1", 20);
        PlayerPrefs.SetString("Task1Date0", "2024-11-21 15:00");
        PlayerPrefs.SetInt("Task1Score0", 30);
        PlayerPrefs.SetInt("Task1BestScore", 110);
        PlayerPrefs.SetFloat("Task1AverageScore", 45);
        PlayerPrefs.SetInt("Task1PlayTime", 10);
        PlayerPrefs.SetString("Task2Date1", "2024-11-21 15:00");
        PlayerPrefs.SetInt("Task2Score1", 10);
        PlayerPrefs.SetString("Task2Date0", "2024-11-21 15:00");
        PlayerPrefs.SetInt("Task2Score0", 20);
        PlayerPrefs.SetInt("Task2BestScore", 20);
        PlayerPrefs.SetFloat("Task2AverageScore", 15);
        PlayerPrefs.SetInt("Task2PlayTime", 2);
    }

    public void GetNextTaskRecord()
    {
        HideUI(taskNumber);
        taskNumber++;
        if (taskNumber > taskCount)
        {
            taskNumber = 1;
        }
        ShowUI(taskNumber);
    }

    public void GetPreviousTaskRecord()
    {
        HideUI(taskNumber);
        taskNumber--;
        if (taskNumber == 0)
        {
            taskNumber = taskCount;
        }
        ShowUI(taskNumber);
    }

    private void HideUI(int taskNumber)
    {
        uiObject[taskNumber - 1].SetActive(false);
    }

    private void ShowUI(int taskNumber)
    {
        uiObject[taskNumber - 1].SetActive(true);
    }

    private void LoadAllTaskRecords()
    {
        string date = "";
        string score = "";
        for (int taskNum = 1; taskNum <= taskCount; taskNum++)
        {
            date = "";
            score = "";

            if (PlayerPrefs.GetString("Task" + taskNum.ToString() + "Date0") != "")
            {
                for (int scoreNum = 0; scoreNum < 10; scoreNum++)
                {
                    if (PlayerPrefs.GetString("Task" + taskNum.ToString() + "Date" + scoreNum.ToString()) != "")
                    {
                        date += PlayerPrefs.GetString("Task" + taskNum.ToString() + "Date" + scoreNum.ToString());
                        score += PlayerPrefs.GetInt("Task" + taskNum.ToString() + "Score" + scoreNum.ToString()).ToString();
                    }
                    date += "\n";
                    score += "\n";
                }

                dateText[taskNum - 1].text = date;
                scoreText[taskNum - 1].text = score;
            }
        }
    }

    private void DrawGraph()
    {
        for (int taskNum = 1; taskNum <= taskCount; taskNum++)
        {
            if (PlayerPrefs.GetString("Task" + taskNum.ToString() + "Date0") != "")
            {
                // Max Score
                int maxScore = PlayerPrefs.GetInt("Task" + taskNum.ToString() + "BestScore");
                scoreGraphObject[taskNum - 1].transform.Find("BestScoreText").GetComponent<TextMeshProUGUI>().text = maxScore.ToString();

                // Each Score
                Vector3 position1 = new Vector3(0, 0, 0);
                for (int scoreNum = 0; scoreNum < 10; scoreNum++)
                {
                    if (PlayerPrefs.GetString("Task" + taskNum.ToString() + "Date" + scoreNum.ToString()) != "")
                    {
                        int score = PlayerPrefs.GetInt("Task" + taskNum.ToString() + "Score" + scoreNum.ToString());
                        
                        // Circle
                        Vector3 position2 = CalculateCirclePosition(scoreNum, score, maxScore);
                        scoreGraphObject[taskNum - 1].transform.Find("ScoreCircle" + (scoreNum + 1).ToString()).GetComponent<RectTransform>().anchoredPosition = position2;
                        
                        // Line
                        if (scoreNum != 0)
                        {
                            scoreGraphObject[taskNum - 1].transform.Find("ScoreLine" + (scoreNum).ToString()).GetComponent<RectTransform>().anchoredPosition = CalculateLinePosition(scoreNum, position1, position2);
                            scoreGraphObject[taskNum - 1].transform.Find("ScoreLine" + (scoreNum).ToString()).GetComponent<RectTransform>().localScale = new Vector3(1, CalculateLineScale(position1, position2), 1);
                            scoreGraphObject[taskNum - 1].transform.Find("ScoreLine" + (scoreNum).ToString()).GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, CalculateLineRotation(position1, position2));                            
                        }
                        position1 = position2;
                    } else
                    {
                        // Short Record
                        scoreGraphObject[taskNum - 1].transform.Find("ScoreCircle" + (scoreNum + 1).ToString()).gameObject.SetActive(false);
                        scoreGraphObject[taskNum - 1].transform.Find("ScoreLine" + (scoreNum).ToString()).gameObject.SetActive(false);
                    }
                }

                // Average Score
                if (maxScore > 0)
                {
                    scoreGraphObject[taskNum - 1].transform.Find("ScoreAverage").GetComponent<RectTransform>().anchoredPosition = CalculateAverageLinePosition(taskNum);
                } else
                {
                    scoreGraphObject[taskNum - 1].transform.Find("ScoreAverage").gameObject.SetActive(false);
                }
            } else
            {
                // No Record
                for (int scoreNum = 0; scoreNum < 10; scoreNum++)
                {
                scoreGraphObject[taskNum - 1].transform.Find("ScoreCircle" + (scoreNum + 1).ToString()).gameObject.SetActive(false);
                }
                
                for (int scoreNum = 1; scoreNum < 10; scoreNum++)
                {
                scoreGraphObject[taskNum - 1].transform.Find("ScoreLine" + (scoreNum).ToString()).gameObject.SetActive(false);
                }
                scoreGraphObject[taskNum - 1].transform.Find("ScoreAverage").gameObject.SetActive(false);
            }
        }
        
    }

    private Vector3 CalculateCirclePosition(int num, int score, int maxScore)
    {
        float xPosition = maxGraphXValue - 70.0f * num;
        float yPosition = 0.0f;
        if (maxScore == 0)
        {
            yPosition = minGraphYValue + (maxGraphYValue - minGraphYValue) * (1.0f / 2.0f);
        } else {            
            yPosition = minGraphYValue + (maxGraphYValue - minGraphYValue) * ((float) score / (float) maxScore);
        }
        return new Vector3(xPosition, yPosition, 0);
    }

    private Vector3 CalculateLinePosition(int num, Vector3 position1, Vector3 position2)
    {
        float xPosition = (position1.x + position2.x) / 2.0f;
        float yPosition = (position1.y + position2.y) / 2.0f;
        return new Vector3(xPosition, yPosition, 0);
    }

    private float CalculateLineScale(Vector3 position1, Vector3 position2)
    {
        float distance = Vector3.Distance(position1, position2);
        return (distance / 70.0f) * 4.8f;
    }
    
    private float CalculateLineRotation(Vector3 position1, Vector3 position2)
    {
        return 90 + Mathf.Atan2(position2.y - position1.y, position2.x - position1.x) * Mathf.Rad2Deg;
    }

    private Vector3 CalculateAverageLinePosition(int taskNum)
    {
        float yPosition = minGraphYValue + (maxGraphYValue - minGraphYValue) * (PlayerPrefs.GetFloat("Task" + taskNum.ToString() + "AverageScore") / (float) PlayerPrefs.GetInt("Task" + taskNum.ToString() + "BestScore"));
        return new Vector3(10.0f, yPosition, 0.0f);
    }
    
    // for other codes
    // using System;
    public void AddScoreRecord(int taskNum, int score)
    {
        for (int scoreNum = 9; scoreNum > 0; scoreNum--)
        {
            PlayerPrefs.SetString("Task" + taskNum.ToString() + "Date" + scoreNum.ToString(),
                PlayerPrefs.GetString("Task" + taskNum.ToString() + "Date" + (scoreNum - 1).ToString()));
            PlayerPrefs.SetInt("Task" + taskNum.ToString() + "Score" + scoreNum.ToString(),
                PlayerPrefs.GetInt("Task" + taskNum.ToString() + "Score" + (scoreNum - 1).ToString()));
        }
        PlayerPrefs.SetString("Task" + taskNum.ToString() + "Date0",  DateTime.Now.ToString(("yyyy-MM-dd HH:mm")));
        PlayerPrefs.SetInt("Task" + taskNum.ToString() + "Score0", score);

        if (score > PlayerPrefs.GetInt("Task" + taskNum.ToString() + "BestScore"))
        {
            PlayerPrefs.SetInt("Task" + taskNum.ToString() + "BestScore", score);
        }

        if (PlayerPrefs.GetInt("Task" + taskNum.ToString() + "PlayTime") == 0)
        {
            PlayerPrefs.SetFloat("Task" + taskNum.ToString() + "AverageScore", (float) score);
            PlayerPrefs.SetInt("Task" + taskNum.ToString() + "PlayTime", 1);
        } else
        {
            PlayerPrefs.SetFloat("Task" + taskNum.ToString() + "AverageScore",
                (float) (score + PlayerPrefs.GetFloat("Task" + taskNum.ToString() + "AverageScore") * PlayerPrefs.GetInt("Task" + taskNum.ToString() + "PlayTime"))
                / (float) (PlayerPrefs.GetInt("Task" + taskNum.ToString() + "PlayTime") + 1));
            PlayerPrefs.SetInt("Task" + taskNum.ToString() + "PlayTime", PlayerPrefs.GetInt("Task" + taskNum.ToString() + "PlayTime") + 1);
        }
    }
}