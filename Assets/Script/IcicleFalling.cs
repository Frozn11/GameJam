using System;
using UnityEngine;

public class IcicleFalling : MonoBehaviour {

    public float startSpeed = 1f;
    public float maxSpeed = 50f;
    public float accelerationRate = 0.5f;
    public int damage = 1;
    
    [Space]
    public float currentSpeed;
    [Space] 
    
    private Vector2 startPos;
    public bool inverted;
    public Rigidbody2D rb;
    
    void Start() {
        currentSpeed = startSpeed;
        startPos = transform.position;
    }
    
    // Update is called once per frame
    void FixedUpdate() {
        //Increase speed over time, clamped to maxSpeed
        currentSpeed = Mathf.Min(currentSpeed + accelerationRate * Time.fixedDeltaTime, maxSpeed);
        
        //Apply velocity, ↓this part checks if it should go up or down
        float velocity = inverted ? currentSpeed : -currentSpeed;
        rb.linearVelocity = new Vector2(0, velocity);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            HealthBar.Instance.Damage(damage);
            gameObject.SetActive(false);
            transform.position = startPos;
            
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Ground")) {
            gameObject.SetActive(false);
            transform.position = startPos;
            
        }
    }
    
}
