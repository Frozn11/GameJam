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

        Invoke("Destroy", timeToDestroy);
    }

    void Destroy() {
        Destroy(gameObject);
        RemoveFromList(gameObject);
    }

    public void RemoveFromList(GameObject other) {
        PlayerCreateProjectile.Instance.projectiles.Remove(other);
        Destroy(gameObject);
        
        
    }
    
}
