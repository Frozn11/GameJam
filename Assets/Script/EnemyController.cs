using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 3f; // Walking speed
    public float chaseSpeed = 5f; // Speed when chasing
    public float patrolDistance = 5f; // How far to patrol before turning
    public float detectionRange = 10f; // Distance to detect player
    public float attackRange = 1.5f; // Distance to attack player
    public float jumpForce = 5f; // For jumping over obstacles (optional)
    public float attackRadius = 2f;
    
    [Header("References")]
    public Transform player; // Assign the player Transform in Inspector
    private Rigidbody2D rb;
    private Vector2 startPosition;
    private bool facingRight = true;
    private bool isChasing = false;
    private bool isPatrolling = true;  // Режим патрулирования
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform; // Auto-find player
        }
    }

    void Update()
    {
        
        // Flip sprite based on direction
        if (rb.velocity.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (rb.velocity.x < 0 && facingRight)
        {
            Flip();
        }
        
        // Check distance to player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRange) {
            isPatrolling = false;
            if (distanceToPlayer <= attackRadius) {
                // Игрок в радиусе атаки - атакуем!
                AttackPlayer();
            }
            else {
                // Игрок близко, но не для атаки - преследуем
                isChasing = false;
                ChasePlayer();
            }
        }
        else
        {
            isPatrolling = true;
            Patrol();
        }
        
        

    }

    void Patrol()
    {
        // Move left/right within patrol distance
        if (transform.position.x >= startPosition.x + patrolDistance)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        else if (transform.position.x <= startPosition.x - patrolDistance)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(speed, rb.velocity.y); // Default right
        }
    }

    void ChasePlayer()
    {
        // Move towards player
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * chaseSpeed, rb.velocity.y);
    }

    void AttackPlayer()
    {
        // Simple attack: Deal damage or knockback (customize as needed)
        Debug.Log("Attacking Player!");
        // Example: Apply force to player
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            Vector2 knockback = (player.position - transform.position).normalized * 5f;
            playerRb.AddForce(knockback, ForceMode2D.Impulse);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

}
