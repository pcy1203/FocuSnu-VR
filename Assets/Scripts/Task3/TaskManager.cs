using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class TaskManager : MonoBehaviour
{
    public XRSocketInteractor socketInteractor1;
    public XRSocketInteractor socketInteractor2;
    public XRSocketInteractor socketInteractor3;
    public XRSocketInteractor socketInteractor4;
    public GameObject[] imageBoxPrefabs;
    private GameObject currentImageBox1;
    private GameObject currentImageBox2;
    private GameObject currentImageBox3;
    private GameObject currentImageBox4;
    public GameObject[] audioListenerPrefabs;
    private GameObject currentAudioListener;
    private List<Vector3> positionList;
    private List<int> indexList; // for Prefabs
    private List<int> numberList; // for Boxes
    public TextMeshProUGUI scoreText;
    private bool scoreUpdated = false;
    private bool gameEnded = false;
    private int score = 0;
    private int prevScore = 0;

    public GameObject instructionPanel;
    public GameObject endGamePanel;
    public TextMeshProUGUI endGameMessage;

    public Transform vrCamera;
    public Transform xrRig;
    public float panelDistance = 3.0f;

    void Start()
    {
        positionList = new List<Vector3>();
        positionList.Add(new Vector3(-22.1f, 1.06f, -5.09f));
        positionList.Add(new Vector3(-23.72f, 1.06f, -0.46f));
        positionList.Add(new Vector3(-22.68f, 1.06f, 3.48f));
        positionList.Add(new Vector3(-21.48f, 1.06f, 7.75f));

        indexList = new List<int>();
        for (int i = 0; i < imageBoxPrefabs.Length / 4; i++)
        {
            indexList.Add(i);
        }

        numberList = new List<int>{0, 1, 2, 3};

        socketInteractor1 = GameObject.Find("SocketLock1").GetComponent<XRSocketInteractor>();
        socketInteractor2 = GameObject.Find("SocketLock2").GetComponent<XRSocketInteractor>();
        socketInteractor3 = GameObject.Find("SocketLock3").GetComponent<XRSocketInteractor>();
        socketInteractor4 = GameObject.Find("SocketLock4").GetComponent<XRSocketInteractor>();

        //PositionPanelInFrontOfPlayer(instructionPanel);
        instructionPanel.SetActive(true);
        scoreText.text = "";
        xrRig.transform.localPosition = Vector3.zero - SceneController.cameraOffset;
    }

    void Update()
    {
        if (socketInteractor1.hasSelection && socketInteractor2.hasSelection &&
            socketInteractor3.hasSelection && socketInteractor4.hasSelection)
        {
            if (!scoreUpdated) 
            {
                UpdateScore(socketInteractor1.selectTarget.gameObject.name, currentImageBox1.name,
                    socketInteractor2.selectTarget.gameObject.name, currentImageBox2.name,
                    socketInteractor3.selectTarget.gameObject.name, currentImageBox3.name,
                    socketInteractor4.selectTarget.gameObject.name, currentImageBox4.name);
                scoreUpdated = true;
            }
            
            if (indexList.Count > 0 && score % 40 == 0 && prevScore != score)
            {
                int index = PickAndRemoveRandomNumber();
                SpawnImageBoxes(index);
                SpawnAudioListener(index);
                scoreUpdated = false;
                prevScore = score;
            } else
            {
                EndGame();
            }
        }
    }

    private int PickAndRemoveRandomNumber()
    {
        int randomIndex = UnityEngine.Random.Range(0, indexList.Count);
        int pickedNumber = indexList[randomIndex];
        indexList.RemoveAt(randomIndex);
        return pickedNumber;
    }

    private void SpawnImageBoxes(int index)
    {
        Shuffle(numberList);

        if (currentImageBox1 && currentImageBox2 && currentImageBox3 && currentImageBox4)
        {
            Destroy(currentImageBox1);
            Destroy(currentImageBox2);
            Destroy(currentImageBox3);
            Destroy(currentImageBox4);
        }
        
        currentImageBox1 = Instantiate(imageBoxPrefabs[index * 4 + 0], positionList[numberList[0]], imageBoxPrefabs[index * 4 + 0].transform.rotation);
        currentImageBox2 = Instantiate(imageBoxPrefabs[index * 4 + 1], positionList[numberList[1]], imageBoxPrefabs[index * 4 + 1].transform.rotation);
        currentImageBox3 = Instantiate(imageBoxPrefabs[index * 4 + 2], positionList[numberList[2]], imageBoxPrefabs[index * 4 + 2].transform.rotation);
        currentImageBox4 = Instantiate(imageBoxPrefabs[index * 4 + 3], positionList[numberList[3]], imageBoxPrefabs[index * 4 + 3].transform.rotation);
    }

    private void SpawnAudioListener(int index)
    {
        if (currentAudioListener)
        {
            Destroy(currentAudioListener);
        }

        currentAudioListener = Instantiate(audioListenerPrefabs[index], new Vector3(0, 0, 0), audioListenerPrefabs[index].transform.rotation);
        currentAudioListener.GetComponent<AudioSource>().Play();
    }

    private void UpdateScore(string response1, string answer1, string response2, string answer2,
        string response3, string answer3, string response4, string answer4)
    {
        if (response1 == answer1) score += 10;
        if (response2 == answer2) score += 10;
        if (response3 == answer3) score += 10;
        if (response4 == answer4) score += 10;
        scoreText.text = "Score : " + score.ToString();
    }

    private void Shuffle(List<int> list)
    {
        int n = list.Count;
        for (int i = 0; i < n; i++)
        {
            int k = UnityEngine.Random.Range(0, n);
            int temp = list[k];
            list[k] = list[i];
            list[i] = temp;
        }
    }

    // Added (1127)
    public void StartGame()  // 버튼에 추가
    {
        instructionPanel.SetActive(false);
        int index = PickAndRemoveRandomNumber();
        SpawnImageBoxes(index);
        SpawnAudioListener(index);
        scoreText.text = "Score : 0";
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
        if (!gameEnded)
        {
            AddScoreRecord(3, score);
            gameEnded = true;
        }
    }

    public void PlayAgain()  // 버튼에 추가
    {
        SceneController.cameraOffset = vrCamera.transform.localPosition;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToHome()  // 버튼에 추가
    {
        SceneController.cameraOffset = vrCamera.transform.localPosition;
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
