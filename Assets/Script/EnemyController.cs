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
    public Transform playerLook;
    public float lookLength;
    public float GroundRayL = 1;
    public float GroundRayR = -1;
    public float GroundRayY = 0.5f;
    private float direction = 1;
    
    private bool isGroundedL, isGroundedR, playerDetected;
    
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
        
        Vector2 start = playerLook.position;
        Vector2 end = (isFacingRight ? Vector3.right : Vector3.left);
        
        playerDetected = Physics2D.Raycast(start, end, lookLength, playerLayer);
        Debug.DrawRay(start, end * lookLength, Color.green);

        if (playerDetected) {
            Debug.Log("Player Detected");
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
        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);
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

}