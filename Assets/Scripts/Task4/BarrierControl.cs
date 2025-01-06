using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierControl : MonoBehaviour
{
    private float lifeTime = 20.0f;

    private float stableXPosition = -4.8f;
    private float minYPosition = 0.3f;
    private float maxYPosition = 3.0f;
    private float minZPosition = -3.0f;
    private float maxZPosition = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RandomMoveCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            Destroy(gameObject);
        }
    }
    
    IEnumerator RandomMoveCoroutine()
    {
        float randomDuration = Random.Range(2.0f, 4.0f);
        float speed = 1.0f / randomDuration;

        float yPosition = Random.Range(minYPosition, maxYPosition);
        float zPosition = Random.Range(minZPosition, maxZPosition);
        Vector3 endPosition = new Vector3(stableXPosition, yPosition, zPosition);
        Vector3 startPosition = transform.position;

        if (endPosition.z - startPosition.z > 0.0f)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 270, transform.rotation.z);
        } else
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 90, transform.rotation.z);
        }
        
        float elapsedTime = 0.0f;
        while (elapsedTime < randomDuration)
        {
            float t = elapsedTime / randomDuration;
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = endPosition;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(RandomMoveCoroutine());
    }
}