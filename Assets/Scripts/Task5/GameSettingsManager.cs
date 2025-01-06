using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class GameSettingsManager : MonoBehaviour
{
    //public Dropdown timeDropdown; // Reference to the Dropdown component
    //public Toggle ballMultiplyToggle; // Reference to the Toggle component
    // public Toggle lightBlinkToggle; // Reference to the Toggle component
    // public LightBlinker lightBlinker; // Reference to the LightBlinker component

    public GameObject instructionPanel;
    public GameObject endGamePanel;
    public TextMeshProUGUI endGameMessage;
    public Button playAgainButton;
    public Button goToHomeButton;

    private string countdown; // value to hold the countdown

    private int gameDuration; // Duration of the game in minutes
    private bool ballMultiplyCondition; // Condition from the toggle
    private bool lightBlinkCondition; // Condition from the toggle
    private int remainingTime; // Remaining time in seconds
    private ScoreControlManager scoreManager; // Reference to the ScoreManager component
    private BallSpawner[] ballSpawners; // Array to hold references to all BallSpawner components
    private bool isOn = false; // Flag to control the game state
    private Coroutine countdownCoroutine; // Reference to the countdown coroutine

    public Transform vrCamera;
    public Transform xrRig;

    private void Awake()
    {
        // Initialize references in Awake
        scoreManager = FindObjectOfType<ScoreControlManager>();
        if (scoreManager == null)
        {
            Debug.LogError("ScoreManager not found in the scene.");
        }

        ballSpawners = FindObjectsOfType<BallSpawner>();
        if (ballSpawners == null)
        {
            Debug.LogError("BallSpawner not found in the scene.");
        }

        // lightBlinker = FindObjectOfType<LightBlinker>();
        // if (lightBlinker == null)
        // {
        //     Debug.LogError("LightBlinker not found in the scene.");
        // }
        xrRig.transform.localPosition = Vector3.zero - SceneController.cameraOffset;
    }

    public void Run()
    {
        instructionPanel.SetActive(false);
        StartCoroutine(DelayedRun());
    }

    private IEnumerator DelayedRun()
    {
        yield return new WaitForSeconds(3);
        
        // Get the selected time from the dropdown
        gameDuration = 2;
        // Debug.Log("Game duration: " + gameDuration);

        // Get the condition from the toggle
        Debug.Log("Ball multiply condition: " + SceneController.distractBall);
        ballMultiplyCondition = SceneController.distractBall;
        // lightBlinkCondition = lightBlinkToggle.isOn;

        // Debug.Log("Ball multiply condition: " + ballMultiplyCondition);
        // Debug.Log("Light blink condition: " + lightBlinkCondition);

        isOn = !isOn; // Toggle the game state
        if (isOn) 
        {
            scoreManager.ResetCounters(); // Reset the score and drop count
            foreach (var ballSpawner in ballSpawners)
            {
                if (!ballSpawner.CompareTag("FakeBall"))
                {
                    ballSpawner.SpawnBall(); // Spawn normal ball
                    ballSpawner.FollowRacket(); // Follow the racket (normal ball)
                }
            }
            countdownCoroutine = StartCoroutine(Countdown(gameDuration * 60));  // Start the countdown timer
        }else{
            StopGame(); // Stop the game
        }
    }

    // private void LightBlink()
    // {
    //     // Check if the light blink condition is enabled and randomly start the light blinking
    //     float random = UnityEngine.Random.Range(0.0f, 1.0f);
    //     if (lightBlinkCondition && random > 0.7f)
    //     {
    //         lightBlinker.StartBlinking(); // Start the light blinking
    //     }    
    // }

    private void SpawnFakeBall(){
        // Check if the ball multiply condition is enabled and spawn fake balls
        float random = UnityEngine.Random.Range(0.0f, 1.0f);
        if (ballMultiplyCondition)
        {
            Debug.Log(ballSpawners);
            foreach (var ballSpawner in ballSpawners)
            {
                if (ballSpawner.CompareTag("FakeBall") && random > 0.7f)
                {
                    ballSpawner.SpawnBall(); // Spawn fake ball
                    ballSpawner.FollowRacket(); // Follow the racket (fake ball)
                }
            }
        }
    }

    private IEnumerator Countdown(int duration)
    {
        remainingTime = duration;
        while (remainingTime > 0)
        {
            int minutes = remainingTime / 60;
            int seconds = remainingTime % 60;
            countdown = string.Format("{0:00}:{1:00}", minutes, seconds);
            scoreManager.UpdateRemainingTime(countdown);
            // LightBlink(); // Call the LightBlink method
            SpawnFakeBall(); // Call the SpawnFakeBall method
            yield return new WaitForSeconds(1);
            remainingTime--;
        }

        // Handle end of game logic here
        countdown = "00:00";
        scoreManager.UpdateRemainingTime(countdown);
        StopGame(); // Stop the game
        AddScoreRecord(5, scoreManager.GetScore());
    }
    private void StopGame()
    {
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine); // Stop the countdown timer
            countdownCoroutine = null;
        }
        for (int i = 0; i < ballSpawners.Length; i++)
        {
            ballSpawners[i].DestroyBall(); // Destroy all balls
        }
        // scoreManager.ResetCounters(); // Reset the score and drop count
        isOn = false; // Ensure the game state is toggled off
        scoreManager.HideScroe();
        endGameMessage.text = "score : " + scoreManager.GetScore();
        endGamePanel.SetActive(true);
    }
    public bool IsConditionApplied()
    {
        return ballMultiplyCondition;
    }

    public bool IsLightBlinkEnabled()
    {
        return lightBlinkCondition;
    }

    public string GetRemainingTime()
    {
        return countdown;
    }

    public void PlayAgain()
    {
        SceneController.cameraOffset = vrCamera.transform.localPosition;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToHome()
    {
        SceneController.cameraOffset = vrCamera.transform.localPosition;
        SceneManager.LoadScene("MainScene");
    }

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