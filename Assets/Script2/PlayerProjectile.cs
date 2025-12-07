using System;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour {
    
    public float speed;
    public float timeToDestroy;
    
    private bool goRight;
    private Vector2 shootDir; 
    
    private Rigidbody2D rb;
    
    
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        goRight = PlayerController.Instance.lookingRight;
 
        float direction = goRight ? 1f : -1f;
        rb.linearVelocity = new Vector2(direction * speed, 0);
 
        Destroy(gameObject, timeToDestroy);
    }

    // Update is called once per frame
    void FixedUpdate() {
        //rb.linearVelocity = new Vector2((goRight ? transform.position.x * speed : -transform.position.x * -speed), 0);
    }
}
