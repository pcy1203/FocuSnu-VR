using UnityEngine;

public class FloatingCube : MonoBehaviour
{
    public Transform xrHead; // Reference to the player's head
    public float orbitRadius = 0.5f; // Distance from the head
    public float orbitSpeed = 30f; // Speed of rotation
    public float verticalOffset = 0.2f; // Offset to keep the object slightly above the head's center

    private float currentAngle = 0f; // Current angle around the player

    // void Update()
    // {
    //     if (head == null) return;

    //     // Increment angle based on orbit speed
    //     currentAngle += orbitSpeed * Time.deltaTime;
    //     // Wrap the angle between 0 and 360 degrees to prevent overflow
    //     currentAngle %= 360f;
    //     // Clamp the angle to stay in front of the player (e.g., between -90° and +90°)
    //     float clampedAngle = Mathf.PingPong(currentAngle, 180f) - 90f;

    //     // Calculate the position around the head within the viewing range
    //     Vector3 offset = new Vector3(
    //         Mathf.Sin(clampedAngle * Mathf.Deg2Rad) * orbitRadius,
    //         verticalOffset,
    //         Mathf.Cos(clampedAngle * Mathf.Deg2Rad) * orbitRadius
    //     );

    //     // Set the position relative to the head
    //     transform.position = head.position + head.forward * offset.z + head.right * offset.x + Vector3.up * offset.y;

    //     // Make the object face the player
    //     transform.LookAt(head);
    // }
    void Update()
    {
        if (xrHead == null) return;

        // Increment the angle for smooth orbiting using Time.deltaTime
        currentAngle += orbitSpeed * Time.deltaTime;

        // Wrap the angle between 0 and 360 degrees to prevent overflow
        currentAngle %= 360f;

        // Calculate the new position in a circular orbit
        float x = Mathf.Sin(currentAngle * Mathf.Deg2Rad) * orbitRadius;
        float z = Mathf.Cos(currentAngle * Mathf.Deg2Rad) * orbitRadius;

        // Smoothly update position relative to the head
        Vector3 targetPosition = xrHead.position + new Vector3(x, verticalOffset, z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, 10f * Time.deltaTime);

        // Make the object face the head
        transform.LookAt(xrHead);
    }
}
