using UnityEngine;

public class EnemyController : MonoBehaviour {
    
    public float SeePlayer;
    public float Speed;
    public bool grounded;
    public LayerMask playerLayer;
    public LayerMask groundLayer;
    public Transform LookPos;
    // Use a single boolean to track direction
    public bool isFacingRight = true; 
    
    public float GroundRayL = 1;
    public float GroundRayR = -1;
    public float GroundRayY = 0.5f;
    public float lookLength;
    
    private bool isGroundedL, isGroundedR, playerDetected;
    
    public Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
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
    }

}
