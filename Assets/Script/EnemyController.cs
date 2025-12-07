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
    public bool isGroundedL, isGroundedR, playerDetected;
    public float GroundRayL = 1;
    public float GroundRayR = -1;
    public float GroundRayY = 0.5f;
    
    private float direction = 1;
    
    private Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        Vector2 rightRayOrigin = new Vector2(transform.position.x + GroundRayL, transform.position.y - GroundRayY);
        isGroundedL = Physics2D.Raycast( rightRayOrigin, Vector2.down, groundLayer);
        
        Vector2 leftRayOrigin = new Vector2(transform.position.x - GroundRayR, transform.position.y - GroundRayY);
        isGroundedR = Physics2D.Raycast(leftRayOrigin, Vector2.down, groundLayer);
        
        Vector3 start = playerLook.position;
        Vector3 end = start + (isFacingRight ? Vector3.right : Vector3.left);
        
        playerDetected = Physics2D.Raycast(start, end, lookLength, playerLayer);
        
        Debug.DrawLine(start, end * lookLength, Color.red);
        Debug.DrawRay(new Vector2(transform.position.x + GroundRayL, transform.position.y - 0.5f), Vector2.down, Color.red);
        Debug.DrawRay(new Vector2(transform.position.x - GroundRayR, transform.position.y - 0.5f), Vector2.down, Color.red);
        
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