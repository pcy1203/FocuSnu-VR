using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Animator anim;
    public Transform player; // Reference to the player's transform
    public float followRadius = 5f; // Radius around the player for random positions
    public float teleportRadius = 10f; // Radius for random teleport when touched

    private UnityEngine.AI.NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        MoveToRandomPositionNearPlayer();
    }

    void Update()
    {
        // If the enemy is close to its target, choose a new random position
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            MoveToRandomPositionNearPlayer();
        }
    }
    void MoveToRandomPositionNearPlayer()
    {
        Vector3 randomDirection = Random.insideUnitSphere * followRadius;
        randomDirection += player.position;
        UnityEngine.AI.NavMeshHit hit;
        if (UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out hit, followRadius, 1))
        {
            agent.SetDestination(hit.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TeleportPlayer();
        }
    }

    void TeleportPlayer()
    {
        Vector3 randomDirection = Random.insideUnitSphere * teleportRadius;
        randomDirection += transform.position;
        randomDirection.y = player.position.y; // Keep teleportation on the same Y-axis level
        player.position = randomDirection;
    }
}
