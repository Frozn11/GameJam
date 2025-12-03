using System;
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
    public float attackRadius = 3f;
    
    [Header("References")]
    public Transform player; // Assign the player Transform in Inspector
    private Rigidbody2D rb;
    private Vector2 startPosition;
    private bool facingRight = true;
    private bool isChasing = false;
    private bool isPatrolling = true;  // Режим патрулирования
    
    private float currentCooldown = 0f; 
    public float knockbackCooldown = 2f; 
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    void Update()
    {
        if (currentCooldown > 0f)
        {
            currentCooldown -= Time.deltaTime;
        }
        Debug.Log(currentCooldown);
        
        
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
        if (distanceToPlayer <= detectionRange ) {
            isPatrolling = false;
            if (distanceToPlayer <= attackRadius) {
                // Игрок в радиусе атаки - атакуем!
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

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            AttackPlayer();
        }
    }

    void AttackPlayer()
    {
        // Simple attack: Deal damage or knockback (customize as needed)
        // Example: Apply force to player
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        if (playerRb != null && currentCooldown <= 0f)
        {
            Vector2 knockback = (player.position - transform.position).normalized * 5f;
            playerRb.AddForce(knockback, ForceMode2D.Impulse); 
            Debug.Log("Attacking Player!");
            currentCooldown = knockbackCooldown; 
            
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
