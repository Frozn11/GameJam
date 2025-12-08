using UnityEngine;

public class EnemyBossShooting : MonoBehaviour {
    public GameObject projectile;
    public Transform projectilePos;

    private float timer;

    private EnemyBossController _enemyBossController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        _enemyBossController = GetComponent<EnemyBossController>();
    }

    // Update is called once per frame
    void Update() {

        float distance = Vector2.Distance(transform.position, PlayerController.Instance.gameObject.transform.position);
        //Debug.Log("Distance: " + distance);
        
        if (distance <= 10f) {
            timer += Time.deltaTime;
                
            if (timer >= 1f) {
                timer = 0;
                Shoot();
            }
        }
        else {
            Debug.Log("HUH?");
        }
        

    }

    void Shoot() {
        Instantiate(projectile, projectilePos.position, Quaternion.identity);
        
    }
}
