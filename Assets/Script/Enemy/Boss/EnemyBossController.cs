using UnityEngine;

public class EnemyBossController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 3f;
    // Use a single boolean to track direction
    public bool isFacingRight = true; 

    [Header("Patrol & Collision")]
    public LayerMask groundLayer;
    public LayerMask playerLayer;
    private float direction = 1;
    
    public float siz;
    
    private Vector2 box = new Vector2(6f, 1.5f);
    
    private Rigidbody2D rb;

    
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        Collider2D player = Physics2D.OverlapBox(transform.position, box,0, playerLayer);
        if (player != null) {
            FacePlayer(player.transform);
        }
    }
        

    public void FacePlayer(Transform player) {
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
        localScale.x = siz; // Reverse the X scale
        transform.localScale = localScale;
    }  
    private void FlipL() {
        isFacingRight = false;
        direction = -1f;
        // 2. Flip the sprite visually
        Vector3 localScale = transform.localScale;
        localScale.x = -siz; // Reverse the X scale
        transform.localScale = localScale;
    }

}