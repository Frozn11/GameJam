using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Detection Settings")]
    [Tooltip("The range within which the enemy will start chasing the player.")]
    public float sightRange = 10f;
    
    [Tooltip("The distance at which the enemy will stop moving and stand ready to attack.")]
    public float stoppingDistance = 1.5f;

    [Header("Movement Settings")]
    public float moveSpeed = 4f;

    // A reference to the Player's Transform component
    private Transform playerTransform;

    // The Rigidbody2D component on the enemy
    private Rigidbody2D rb;

    void Start()
    {
        // 1. Find the player GameObject by its tag (make sure your player has the tag "Player")
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player GameObject not found! Make sure your player is tagged 'Player'.");
        }

        // 2. Get the Rigidbody2D component for physics-based movement
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found on the Enemy!");
        }
    }

    void Update()
    {
        if (playerTransform == null)
        {
            // If we lost the player or didn't find them, do nothing
            return;
        }

        // Calculate the distance between the enemy and the player
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        // Check if the player is within the sight range
        if (distanceToPlayer < sightRange)
        {
            // The player is seen! Chase them.
            ChasePlayer(distanceToPlayer);
        }
        else
        {
            // Player is out of sight, stop moving
            rb.linearVelocity = Vector2.zero;
        }
    }

    void ChasePlayer(float currentDistance)
    {
        // 1. Check if we are outside the stopping distance
        if (currentDistance > stoppingDistance)
        {
            // Calculate the direction vector towards the player
            Vector2 direction = (playerTransform.position - transform.position).normalized;

            // Apply velocity to the Rigidbody2D for movement
            // Use direction * moveSpeed to move towards the player
            rb.linearVelocity = direction * moveSpeed;

            // Handle sprite flipping (optional, but important for 2D)
            FlipSprite(direction.x);
        }
        else
        {
            // 2. We are close enough, stop moving (enter attack state if implemented)
            rb.linearVelocity = Vector2.zero;
        }
    }
    
    // Simple method to flip the enemy sprite based on movement direction
    void FlipSprite(float directionX)
    {
        // Only flip if we are moving significantly
        if (Mathf.Abs(directionX) > 0.1f)
        {
            // Check if the direction is left (< 0) or right (> 0)
            if (directionX < 0)
            {
                // Moving left, face left (Scale X = -1)
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                // Moving right, face right (Scale X = 1)
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
    
}