using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public Material[] cardFrontMaterials;
    public Material cardBackMaterial;
    public Transform gridParent;
    public Vector2 gridSize = new Vector2(6, 4);
    public float spacing = 2.0f;

    private List<Card> cards = new List<Card>();
    private Card firstSelected = null;
    private Card secondSelected = null;
    private bool interactionDisabled = false;

    public TextMeshProUGUI timerText;
    private float currentTime;

    public GameObject instructionPanel;
    public GameObject endGamePanel;
    public TextMeshProUGUI endGameMessage;
    public Button playAgainButton;
    public Button goToHomeButton;
    public Transform vrCamera;
    public Transform xrRig;

    private bool playing = false;

    void Start()
    {
        interactionDisabled = true;
        SetupGrid();
        currentTime = 0f;

        instructionPanel.SetActive(true);
        endGamePanel.SetActive(false);
        xrRig.transform.localPosition = Vector3.zero - SceneController.cameraOffset;
    }

    void Update()
    {
        if(playing){
            currentTime += Time.deltaTime;
            if (currentTime >= 300 && playing)
            {
                currentTime = 0;
                playing = false;
                EndGame(false);
            }
            UpdateTimerUI();
        }
    }

    public void StartGame()
    {
        instructionPanel.SetActive(false);
        playing = true;
        interactionDisabled = false;
    }

    void SetupGrid()
    {
        List<int> cardIndices = new List<int>();
        for (int i = 0; i < cardFrontMaterials.Length; i++)
        {
            cardIndices.Add(i);
            cardIndices.Add(i);
        }
        cardIndices = cardIndices.OrderBy(x => UnityEngine.Random.value).ToList();

        for (int i = 0; i < gridSize.x * gridSize.y; i++)
        {
            GameObject cardObj = Instantiate(cardPrefab, gridParent);
            cardObj.name = $"Card_{i}";
            Card card = cardObj.GetComponent<Card>();
            card.cardFront = cardFrontMaterials[cardIndices[i]];
            card.cardBack = cardBackMaterial;
            card.SetFaceUp(false);
            card.gameManager = this;
            card.OnFlipComplete.AddListener(OnCardFlipComplete);
            cards.Add(card);

            int row = i / (int)gridSize.x;
            int col = i % (int)gridSize.x;
            cardObj.transform.localPosition = new Vector3(col * spacing, 0, row * spacing);
        }
    }

    public void CardClicked(Card clickedCard)
    {
        if(interactionDisabled){
            return;
        }else{
            interactionDisabled = true;
            clickedCard.Animate();
            if (firstSelected == null)
            {
                firstSelected = clickedCard;
                interactionDisabled = false;
            }
            else if (secondSelected == null)
            {
                secondSelected = clickedCard;
                StartCoroutine(CheckForMatch());
            }
        }
        
    }

    System.Collections.IEnumerator CheckForMatch()
    {
        while (firstSelected.IsAnimating() || secondSelected.IsAnimating())
        {
            yield return null;
        }
        if (firstSelected.cardFront == secondSelected.cardFront)
        {
            firstSelected = null;
            secondSelected = null;
            interactionDisabled = false;

            if (cards.All(c => c.IsFaceUp()))
            {
                EndGame(true);
            }
        }
        else
        {
            yield return new WaitForSeconds(0.5f);

            firstSelected.FlipBack();
            secondSelected.FlipBack();

            yield return new WaitForSeconds(0.5f);

            firstSelected = null;
            secondSelected = null;

            StartCoroutine(SwapRandomCards());
        }
    }

    void OnCardFlipComplete(Card card)
    {
    }

    System.Collections.IEnumerator SwapRandomCards()
    {
        List<Card> availableCards = cards.Where(c => !c.IsFaceUp() && !c.IsAnimating()).ToList();
        if (availableCards.Count < 2)
        {
            interactionDisabled = false;
            yield break;
        }

        Card card1 = availableCards[UnityEngine.Random.Range(0, availableCards.Count)];
        Card card2 = availableCards[UnityEngine.Random.Range(0, availableCards.Count)];
        while (card2 == card1)
        {
            card2 = availableCards[UnityEngine.Random.Range(0, availableCards.Count)];
        }

        Vector3 pos1 = card1.transform.localPosition;
        Vector3 pos2 = card2.transform.localPosition;

        float swapDuration = 1f;
        float elapsed = 0f;

        interactionDisabled = true;

        while (elapsed < swapDuration)
        {
            card1.transform.localPosition = Vector3.Lerp(pos1, pos2, elapsed / swapDuration);
            card2.transform.localPosition = Vector3.Lerp(pos2, pos1, elapsed / swapDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        card1.transform.localPosition = pos2;
        card2.transform.localPosition = pos1;

        interactionDisabled = false;
    }

    public bool IsInteractionDisabled()
    {
        return interactionDisabled;
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
            timerText.text = $"Time: {(300 - Mathf.CeilToInt(currentTime))}s";
    }


    void EndGame(bool won)
    {
        interactionDisabled = true;
        playing = false;
        endGameMessage.text = won ? $"You Win! Time: {(300 - Mathf.CeilToInt(currentTime))}s" : "Game Over";
        endGamePanel.SetActive(true);
        AddScoreRecord(1, 300 - Mathf.CeilToInt(currentTime));
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
