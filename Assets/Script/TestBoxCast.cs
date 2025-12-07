using UnityEngine;

public class TestBoxCast : MonoBehaviour {
    public bool Check;
    public LayerMask CheckLayer;
    
    public Vector2 boxSize = new Vector2(1f, 1f);
    public float castDistance = 0.5f;
    public LayerMask groundLayer;
    
    void Update() {
        Vector2 castOrigin = new Vector2(transform.position.x, transform.position.x);
        float castAngle = 0f;
        Vector2 castDirection = Vector2.down;
        Check = Physics2D.BoxCast(
            castOrigin,
            boxSize,
            castAngle,
            castDirection,
            castDistance,
            groundLayer);
    }
private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        // The BoxCast is conceptual, but we can draw the start position (origin) 
        // and the end position (origin + direction * distance) to visualize the sweep.
        
        Vector3 startBoxCenter = transform.position;
        Vector3 endBoxCenter = transform.position + (Vector3)Vector2.down * castDistance;
        
        // Draw the box at the start position
        Gizmos.DrawWireCube(startBoxCenter, boxSize);
        // Draw the box at the max cast distance
        Gizmos.DrawWireCube(endBoxCenter, boxSize);
    }
    
}
