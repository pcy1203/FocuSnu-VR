using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    
    private float speed = 6.0f;
    private float leftLimit = -7.0f;
    private float rightLimit = 7.0f;

    void Start()
    {
        StartCoroutine(ToggleDirection());
    }

    void Update()
    {
        speed = Random.Range(4.0f, 12.0f);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (transform.position.z < leftLimit) transform.position = new Vector3(transform.position.x, transform.position.y, leftLimit);
        if (transform.position.z > rightLimit) transform.position = new Vector3(transform.position.x, transform.position.y, rightLimit);
    }

    private IEnumerator ToggleDirection() {
        while (true) {
            if (Random.Range(0, 2) == 0) transform.Rotate(0, 180, 0);
            float seconds = Random.Range(0.1f, 0.3f);
            yield return new WaitForSeconds(seconds);
        }
    }
}
