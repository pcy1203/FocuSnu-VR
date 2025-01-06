using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class ObjectManager : MonoBehaviour
{
    public List<GameObject> objectsToDisappear;
    public List<GameObject> candidateObjects;
    public List<Transform> spawnPoints;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;

    public int score = 0;
    public float timeLimit = MenuManager.task2Time;
    private float currentTime;
    private bool hasStartedHandleObjects = false;

    [Header("Original Positions")]
    public List<Vector3> originalPositions = new List<Vector3>();

    private List<GameObject> invisibleColliders = new List<GameObject>();
    private List<GameObject> selectedObjectsToDisappear = new List<GameObject>();
    private List<GameObject> selectedCandidateObjects = new List<GameObject>();

    public GameObject instructionPanel;
    public GameObject endGamePanel;
    public TextMeshProUGUI endGameMessage;
    public Button playAgainButton;
    public Button goToHomeButton;
    private bool playing;
    private bool interactionDisabled = false;
    private int maxScore = 0;
    public GameObject hideImage;

    public Transform vrCamera;
    public Transform xrRig;

    void Start()
    {
        interactionDisabled = true; //ui
        currentTime = timeLimit;

        foreach (var obj in objectsToDisappear)
        {
            originalPositions.Add(obj.transform.position);
            obj.SetActive(true);
        }

        foreach (var obj in candidateObjects)
        {
            originalPositions.Add(obj.transform.position);
            obj.SetActive(false);
        }

        selectedObjectsToDisappear = GetRandomObjects(objectsToDisappear, 3);

        foreach (var obj in selectedObjectsToDisappear)
        {
            CreateColliderForObject(obj);
        }

        UpdateScoreUI();
        UpdateTimerUI();
        instructionPanel.SetActive(true); //ui
        endGamePanel.SetActive(false); //ui
        scoreText.text = "";
        timerText.text = "";
        xrRig.transform.localPosition = Vector3.zero - SceneController.cameraOffset;
    }

    void Update()
    {
        if(playing)
        {
            if (currentTime > 0 && !hasStartedHandleObjects)
            {
                currentTime -= Time.deltaTime;
                UpdateTimerUI();
            }
            else if (!hasStartedHandleObjects)
            {
                hasStartedHandleObjects = true;
                StartCoroutine(HandleObjects());
            }
            if(hasStartedHandleObjects){
                currentTime += Time.deltaTime;
                UpdateTimerUI();
                if(currentTime >= 200 && playing)
                {
                    currentTime = 0;
                    playing = false;
                    EndGame(false);
                }
            }
            if(score == spawnPoints.Count){
                EndGame(true);
            }
        }
    }

    public void StartGame()
    {
        instructionPanel.SetActive(false);
        playing = true;
        interactionDisabled = false;
        hideImage.SetActive(false);
        UpdateScoreUI();
    }

    private IEnumerator HandleObjects()
    {
        foreach (var obj in selectedObjectsToDisappear)
        {
            obj.SetActive(false);
        }

        foreach (var colliderObj in invisibleColliders)
        {
            colliderObj.SetActive(true);
        }

        int numCandidateObjectsToSelect = Mathf.Max(0, spawnPoints.Count - selectedObjectsToDisappear.Count);
        selectedCandidateObjects = GetRandomObjects(candidateObjects, numCandidateObjectsToSelect);

        List<GameObject> objectsToSpawn = new List<GameObject>();
        objectsToSpawn.AddRange(selectedObjectsToDisappear);
        //objectsToSpawn.AddRange(selectedCandidateObjects);
        ShuffleList(objectsToSpawn);

        for (int i = 0; i < spawnPoints.Count && i < objectsToSpawn.Count; i++)
        {
            GameObject obj = objectsToSpawn[i];
            obj.SetActive(true);
            obj.transform.position = spawnPoints[i].position;
            maxScore+=1;
        }

        yield return null;
    }

    private void CreateColliderForObject(GameObject obj)
    {
        GameObject colliderObject = new GameObject(obj.name + "_Collider");
        colliderObject.transform.position = obj.transform.position;
        colliderObject.transform.rotation = obj.transform.rotation;
        colliderObject.transform.localScale = obj.transform.localScale;
        BoxCollider boxCollider = colliderObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = true;

        Renderer objRenderer = obj.GetComponent<Renderer>();
        if (objRenderer != null)
        {
            boxCollider.size = objRenderer.bounds.size;
        }
        else
        {
            boxCollider.size = Vector3.one;
        }

        ColliderScript colliderScript = colliderObject.AddComponent<ColliderScript>();
        colliderScript.disappearedObjectName = obj.name;
        colliderScript.objectManager = this;

        invisibleColliders.Add(colliderObject);
        colliderObject.SetActive(false);
    }

    private List<GameObject> GetRandomObjects(List<GameObject> sourceList, int count)
    {
        List<GameObject> randomObjects = new List<GameObject>(sourceList);
        ShuffleList(randomObjects);
        return randomObjects.GetRange(0, Mathf.Min(count, randomObjects.Count));
    }

    private void ShuffleList(List<GameObject> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            GameObject temp = list[i];
            int randomIndex = UnityEngine.Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = $"Score: {score}";
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            if(hasStartedHandleObjects)
            {
                timerText.text = $"Time: {(200 - Mathf.CeilToInt(currentTime))}s";
            }
            else
            {
                timerText.text = $"Countdown: {(Mathf.CeilToInt(currentTime))}s";
            }
        }
            
    }

    public void ResetObject(GameObject obj, int index)
    {
        if (index >= 0 && index < originalPositions.Count)
        {
            obj.transform.position = originalPositions[index];
            obj.SetActive(true);
        }
    }

    void EndGame(bool won)
    {
        scoreText.text = "";
        timerText.text = "";
        interactionDisabled = true;
        playing = false;
        endGameMessage.text = won ? $"You Win!\n Time: {(200 - Mathf.CeilToInt(currentTime))}s" : "Game Over!";
        endGamePanel.SetActive(true);
        AddScoreRecord(2, 200 - Mathf.CeilToInt(currentTime));
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
