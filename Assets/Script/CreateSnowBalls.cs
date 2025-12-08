using UnityEngine;

public class CreateSnowBalls : MonoBehaviour {

    public GameObject prefab;
    public Vector3 StartPos;
    
    public float cooldown;
    public float TimeTCooldown;
    private Rigidbody2D rb;
        

    void Start() {
        StartPos = transform.position;
        rb = prefab.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        cooldown += Time.deltaTime;
        if (cooldown >= TimeTCooldown) {
            
            prefab.transform.position = StartPos;
            rb.linearVelocity = Vector2.zero;
            cooldown = 0;
            
        }   
    }
}
