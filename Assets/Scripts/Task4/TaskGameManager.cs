using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TaskGameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject[] barrierPrefabs;
    public GameObject coinPrefab;
    public GameObject player;

    private float stableXPosition = -4.8f;
    private float minYPosition = 0.3f;
    private float maxYPosition = 3.0f;
    private float minZPosition = -3.0f;
    private float maxZPosition = 3.0f;

    private float time = 0.0f;
    private float barrierCount = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnCoin", 2.0f, 6.0f);
        InvokeRepeating("SpawnBarrier", 5.0f, 6.0f);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > barrierCount * 60.0f)
        {
            barrierCount += 1.0f;
            InvokeRepeating("SpawnBarrier", 5.0f, 6.0f);
        }
    }

    public void UpdateScore(int score)
    {
        scoreText.text = "Score : " + score.ToString();
    }

    private void SpawnBarrier()
    {
        Vector3 playerPosition = player.transform.position;
        float yPosition = playerPosition.y;
        float zPosition = playerPosition.z;

        while (((yPosition - playerPosition.y < 0.5f) && (yPosition - playerPosition.y > -0.5f))
            || ((zPosition - playerPosition.z < 0.5f) && (zPosition - playerPosition.z > -0.5f)))
        {
            yPosition = Random.Range(minYPosition, maxYPosition);
            zPosition = Random.Range(minZPosition, maxZPosition);
        }
        
        int index = Random.Range(0, barrierPrefabs.Length);
        Instantiate(barrierPrefabs[index], new Vector3(stableXPosition, yPosition, zPosition), barrierPrefabs[index].transform.rotation);
    }

    private void SpawnCoin()
    {
        Vector3 playerPosition = player.transform.position;
        float yPosition = playerPosition.y;
        float zPosition = playerPosition.z;

        while (((yPosition - playerPosition.y < 0.5f) && (yPosition - playerPosition.y > -0.5f))
            || ((zPosition - playerPosition.z < 0.5f) && (zPosition - playerPosition.z > -0.5f)))
        {
            yPosition = Random.Range(minYPosition, maxYPosition);
            zPosition = Random.Range(minZPosition, maxZPosition);
        }
        
        Instantiate(coinPrefab, new Vector3(stableXPosition, yPosition, zPosition), coinPrefab.transform.rotation);
    }
}
