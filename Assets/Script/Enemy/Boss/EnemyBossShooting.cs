using UnityEngine;

public class EnemyBossShooting : MonoBehaviour {
    public GameObject projectile;
    public Transform projectilePos;
    public float TimeToShoot;
    
    private float timer;

    private EnemyBossController _enemyBossController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        _enemyBossController = GetComponent<EnemyBossController>();
    }

    // Update is called once per frame
    void Update() {
        // период дистанцию между врагом и игроком 
        float distance = Vector2.Distance(transform.position, PlayerController.Instance.gameObject.transform.position);
        //Debug.Log("Distance: " + distance);
        
        //если игрок Ближе или равно 10м 
        if (distance <= 10f) {
            timer += Time.deltaTime;
                
            if (timer >= TimeToShoot) {
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
