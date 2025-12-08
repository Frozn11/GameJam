using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 3f;
    // Use a single boolean to track direction
    public bool isFacingRight = true; 

    [Header("Patrol & Collision")]
    public LayerMask groundLayer;
    public LayerMask playerLayer;
    public Transform LookPos;
    public float lookLength;
    public float GroundRayL = 1;
    public float GroundRayR = -1;
    public float GroundRayY = 0.5f;
    private float direction = 1;
    
    private bool isGroundedL, isGroundedR, playerDetected;

    private Vector2 box = new Vector2(6f, 1.5f);
    
    private Rigidbody2D rb;

    
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        // Check ground Right so there no ground turn left
        Vector2 rightRayOrigin = new Vector2(transform.position.x + GroundRayL, transform.position.y - GroundRayY);
        isGroundedL = Physics2D.Raycast( rightRayOrigin, Vector2.down, groundLayer);
        
        // Check ground Left so there no ground turn right
        Vector2 leftRayOrigin = new Vector2(transform.position.x - GroundRayR, transform.position.y - GroundRayY);
        isGroundedR = Physics2D.Raycast(leftRayOrigin, Vector2.down, groundLayer);
        
        Debug.DrawRay(new Vector2(transform.position.x + GroundRayL, transform.position.y - 0.5f), Vector2.down, Color.red);
        Debug.DrawRay(new Vector2(transform.position.x - GroundRayR, transform.position.y - 0.5f), Vector2.down, Color.red);
        
        Vector2 start = LookPos.position;
        Vector2 end = (isFacingRight ? Vector3.right : Vector3.left);
        
        Debug.DrawRay(start, end * lookLength, Color.green);

        if (Physics2D.Raycast(start, end, lookLength, playerLayer)) {
            playerDetected = true;
            Debug.Log("Player Detected");
        }

        if (playerDetected) {
            Debug.Log("ye eh");
            Collider2D player = Physics2D.OverlapBox(transform.position, box,0, playerLayer);
            if (player != null) {
                FacePlayer(player.transform);
            }
        }
        
        if (!isGroundedR) {
            FlipL();
        }
        if (!isGroundedL) {
            FlipR();
        }
        
    }

    void FixedUpdate() {
        // Use rb.velocity for movement
        if (!IsPlayerDetected()) {
            rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);
        }
    }

    void FacePlayer(Transform player) {
        Debug.Log("ye huh");
        if (player.position.x > transform.position.x) {
            FlipR();
            Debug.Log("ye R");
        }
        else {
            FlipL();
            Debug.Log("ye L");
        }
        
    }
    private void FlipR() {
        isFacingRight = true;
        direction = 1f;
        // 2. Flip the sprite visually
        Vector3 localScale = transform.localScale;
        localScale.x = 1f; // Reverse the X scale
        transform.localScale = localScale;
    }  
    private void FlipL() {
        isFacingRight = false;
        direction = -1f;
        // 2. Flip the sprite visually
        Vector3 localScale = transform.localScale;
        localScale.x = -1f; // Reverse the X scale
        transform.localScale = localScale;
    }

    public void LosePlayer() {
        playerDetected = false;
    }

    public bool IsPlayerDetected() {
        return playerDetected;
    } 

}