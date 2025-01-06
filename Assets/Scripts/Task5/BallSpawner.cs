using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject ballPrefab; // Reference to the ball prefab
    public Transform racketTransform; // Reference to the racket transform
    public float spawnHeight = 1f; // Height above the racket where the ball will spawn
    private GameObject spawnedBall;

    public void SpawnBall()
    {
        Vector3 spawnPosition = new Vector3(racketTransform.position.x, racketTransform.position.y + spawnHeight, racketTransform.position.z);
        spawnedBall = Instantiate(ballPrefab, spawnPosition, Quaternion.identity);
        // Debug.Log("spawned"+spawnedBall);
    }
    public void DestroyBall()
    {
        if (spawnedBall != null)
        {
            Destroy(spawnedBall);
        }
    }
    public void TeleportBallToRacket()
    {
        // Debug.Log(spawnedBall);
        if (spawnedBall != null)
        {
            Vector3 spawnPosition = new Vector3(racketTransform.position.x, racketTransform.position.y + spawnHeight, racketTransform.position.z);
            spawnedBall.transform.position = spawnPosition;
            // Reset the ball's velocity and angular velocity
            Rigidbody ballRigidbody = spawnedBall.GetComponent<Rigidbody>();
            if (ballRigidbody != null)
            {
                ballRigidbody.velocity = Vector3.zero;
                ballRigidbody.angularVelocity = Vector3.zero;
            }
        }
    }
    public void FollowRacket()
    {
        Vector3 followPosition = new Vector3(racketTransform.position.x, racketTransform.position.y + spawnHeight, racketTransform.position.z);
        spawnedBall.transform.position = followPosition;
    }
}