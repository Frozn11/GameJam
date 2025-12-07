using UnityEngine;

public class PlayerProjectile : MonoBehaviour {
    
    public float speed;
    public float timeToDestroy;
    public bool goRight;
    
    private Rigidbody2D rb;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        goRight = PlayerController.Instance.lookingRight;
        Destroy(gameObject, timeToDestroy);
    }

    // Update is called once per frame
    void FixedUpdate() {
        rb.linearVelocity = new Vector2((goRight ? transform.position.x * speed : -transform.position.x * -speed), 0);
    }
    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
            Destroy(gameObject);
        }
    }
}
