using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 3f;
    // Use a single boolean to track direction
    private bool isFacingRight = true; 

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
        Vector3 end = start + (isFacingRight ? Vector3.right : Vector3.left) * lookLength;
        
        playerDetected = Physics2D.BoxCast(start, end, 2, end, groundLayer);
        
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
    
// --- Add this method to your EnemyController script ---

    private void OnDrawGizmosSelected()
    {
        // Ensure we have a Rigidbody to get the correct origin
        if (rb == null) return;

        // Define the exact parameters used in the BoxCast check
        Vector2 boxCastSize = new Vector2(1f, 1f); // Use the same size as in the check
        float boxCastDistance = lookLength;        
        Vector2 boxCastDirection = isFacingRight ? Vector2.right : Vector2.left;
        Vector2 boxCastOrigin = rb.position; 
    
        // Calculate the center of the total swept area for easier visualization
        Vector2 centerOfSweep = boxCastOrigin + (boxCastDirection * boxCastDistance / 2f);
    
        // Create the final position of the box
        Vector2 finalBoxCenter = boxCastOrigin + (boxCastDirection * boxCastDistance);

        // 1. Draw the total swept volume (as a wire cube)
        // This cube represents the whole detection area from start to finish.
        Gizmos.color = new Color(1f, 0.5f, 0f, 0.5f); // Orange color, semi-transparent
        Gizmos.DrawWireCube(centerOfSweep, new Vector3(
            boxCastSize.x + boxCastDistance, // Width is the box width + sweep distance
            boxCastSize.y,
            0f
        ));

        // 2. Draw the starting box (clearer starting point)
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(boxCastOrigin, boxCastSize);

        // 3. Draw the final position of the box
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(finalBoxCenter, boxCastSize);
    }
    
}