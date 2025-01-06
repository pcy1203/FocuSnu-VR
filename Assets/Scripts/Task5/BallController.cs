using UnityEngine;

public class BallController : MonoBehaviour
{
    private BallSpawner[] ballSpawners;
    private ScoreControlManager scoreManager;

    void Start()
    {
        ballSpawners = FindObjectsOfType<BallSpawner>();
        scoreManager = FindObjectOfType<ScoreControlManager>();
    }


    void OnCollisionEnter(Collision collision)
    {
    
        foreach (var ballSpawner in ballSpawners)
        {
            // Check if the current object is a fake ball
            if(ballSpawner.CompareTag(gameObject.tag)) 
            // Check if the ball spawner tag matches the current object tag
            // don't know why but the spawner is changing to fakeball so
            // had to get all the spawner and loop them all
            // only filtering the ones with same tag (FakeBall/NormalBall)
            // if the tag does not match, ignore
            {
                bool isFakeBall = CompareTag("FakeBall");
                if (collision.gameObject.CompareTag("Racket") && !isFakeBall)
                {
                    // Increase score for normal balls hitting the racket
                    scoreManager.IncreaseScore();
                }
                else if (collision.gameObject.CompareTag("Ground"))
                {
                    if (isFakeBall)
                    {
                        // Destroy fake balls hitting the ground
                        Destroy(gameObject);
                    }
                    else
                    {
                        // Increase drop count and respawn normal balls hitting the ground
                        scoreManager.IncreaseDropCount();
                        Debug.Log("Teleporting ball to racket");
                        ballSpawner.TeleportBallToRacket();
                    }
                }
            }
        }

    }
}