using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class PlayerControl : MonoBehaviour
{
    public GameObject joyStick;
    public GameObject taskGameManager;
    public TaskGameManager gameManager;
    public DistractionController distractionController;
    public TextMeshProUGUI scoreText;

    private int score = 0;

    private float minYPosition = 0.3f;
    private float maxYPosition = 3.0f;
    private float minZPosition = -3.0f;
    private float maxZPosition = 3.0f;

    public GameObject instructionPanel;
    public GameObject endGamePanel;
    public TextMeshProUGUI endGameMessage;

    public float panelDistance = 3.0f;
    public Transform vrCamera;
    public Transform xrRig;

    public GameObject reversePanel;
    private bool isReverse = false;
    private float reverse = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<TaskGameManager>();
        taskGameManager = GameObject.Find("GameManager");
        taskGameManager.SetActive(false);

        reversePanel = GameObject.Find("PanelReverse");
        reversePanel.SetActive(false);

        //PositionPanelInFrontOfPlayer(instructionPanel);
        instructionPanel.SetActive(true);
        scoreText.text = "";
        xrRig.transform.localPosition = Vector3.zero - SceneController.cameraOffset;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SendValue(Vector2 value)
    {
        float zTranslation = value.x * Time.deltaTime * -1 * reverse;
        float yTranslation = value.y * Time.deltaTime * -1 * reverse;
        if (transform.position.z + zTranslation < minZPosition || transform.position.z + zTranslation > maxZPosition)
        {
            zTranslation = 0.0f;
        } else if (transform.position.y + yTranslation < minYPosition || transform.position.y + yTranslation > maxYPosition)
        {
            yTranslation = 0.0f;
        } else
        {
            transform.Translate(zTranslation, yTranslation, 0);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Barrier")) {
            // score -= 10;
            // gameManager.UpdateScore(score);
            // Destroy(collision.gameObject);
            EndGame();
        }
        if (collision.gameObject.CompareTag("Coin")) {
            score += 10;
            gameManager.UpdateScore(score);
            Destroy(collision.gameObject);
        }
    }

    // Added (1127)
    public void StartGame()  // 버튼에 추가
    {
        instructionPanel.SetActive(false);
        taskGameManager.SetActive(true);
        InvokeRepeating("ReverseDirection", 30.0f, 30.0f);
        scoreText.text = "Score : 0";
    }


    private void ReverseDirection()
    {
        isReverse = !isReverse;
        reversePanel.SetActive(isReverse);
        if (isReverse)
        {
            reverse = -1.0f;
        } else
        {
            reverse = 1.0f;
        }
    }

    void PositionPanelInFrontOfPlayer(GameObject panel)
    {
        Vector3 forward = vrCamera.forward;
        forward.y = 0;
        forward.Normalize();

        panel.transform.position = vrCamera.position + forward * panelDistance;
        panel.transform.rotation = Quaternion.LookRotation(forward);
    }

    void EndGame()
    {
        scoreText.text = "";
        endGameMessage.text = "Your Score : " + score.ToString();
        //PositionPanelInFrontOfPlayer(endGamePanel);
        endGamePanel.SetActive(true);
        AddScoreRecord(4, score);
        gameObject.SetActive(false);
    }

    public void PlayAgain()  // 버튼에 추가
    {
        SceneController.cameraOffset = vrCamera.transform.localPosition;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToHome()  // 버튼에 추가
    {
        SceneController.cameraOffset = vrCamera.transform.localPosition;
        //Debug.Log("scene: "+SceneController.cameraOffset);
        SceneManager.LoadScene("MainScene");
    }

    // Added (1127)    
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