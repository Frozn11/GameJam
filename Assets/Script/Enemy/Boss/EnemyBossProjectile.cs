using System;
using UnityEngine;

public class EnemyBossProjectile : MonoBehaviour {
    private Rigidbody2D rb;
    
    public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // берёт положение игрока
        Vector3 direction = PlayerController.Instance.gameObject.transform.position - transform.position;
        rb.linearVelocity = new Vector2(direction.x, direction.y).normalized * speed;

        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);
    }

    void OnTriggerEnter(Collision2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground")) {
            Destroy(gameObject);
        }  
    }
}
