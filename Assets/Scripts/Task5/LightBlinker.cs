using UnityEngine;
using System.Collections;

public class LightBlinker : MonoBehaviour
{
    public Light blinkingLight; // Reference to the Light component
    public Transform playerTransform; // Reference to the player's transform
    public Vector3 offset = new Vector3(1f, 0f, 0f); // Offset from the player's position
    public float blinkDuration = 0.5f; // Duration of each blink
    public int blinkCount = 3; // Number of blinks
    public float intensity = 2.0f; // Intensity of the light
    public float orbitRadius = 0.5f; // Distance from the head
    public float orbitSpeed = 100f; // Speed of rotation
    public float verticalOffset = 0.15f; // Offset to keep the object slightly above the head's center

    private float currentAngle = 0f; // Current angle around the player
    private void Start()
    {
        if (blinkingLight == null)
        {
            blinkingLight = GetComponent<Light>();
        }
        blinkingLight.intensity = intensity;
    }

    private void Update()
    {
        if (playerTransform == null) return;

        // Increment the angle for smooth orbiting using Time.deltaTime
        currentAngle += orbitSpeed * Time.deltaTime;

        // Wrap the angle between 0 and 360 degrees to prevent overflow
        currentAngle %= 360f;

        // Calculate the new position in a circular orbit
        float x = Mathf.Sin(currentAngle * Mathf.Deg2Rad) * orbitRadius;
        float z = Mathf.Cos(currentAngle * Mathf.Deg2Rad) * orbitRadius;

        // Smoothly update position relative to the head
        Vector3 targetPosition = playerTransform.position + new Vector3(x, verticalOffset, z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, 10f * Time.deltaTime);

        // Make the object face the head
        transform.LookAt(playerTransform);
    
        // if (playerTransform != null)
        // {
        //     // Update the light's position to follow the player with an offset
        //     blinkingLight.transform.position = playerTransform.position + offset;
        // }
    }

    public void StartBlinking()
    {
        StartCoroutine(BlinkLight());
        Update();
    }
    public void StopBlinking()
    {
        blinkingLight.enabled = false;
    }

    private IEnumerator BlinkLight()
    {
        for (int i = 0; i < blinkCount; i++)
        {
            Debug.Log("change color");
            blinkingLight.enabled = true;
            blinkingLight.color = Random.ColorHSV(); // Randomize the light color
            yield return new WaitForSeconds(blinkDuration);
            blinkingLight.enabled = false;
            yield return new WaitForSeconds(blinkDuration);
        }
    }
}