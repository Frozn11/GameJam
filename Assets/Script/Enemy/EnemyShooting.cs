using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject projectile;
    public Transform projectilePos;

    private float timer;

    private EnemyController enemyController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        enemyController = GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update() {

        float distance = Vector2.Distance(transform.position, PlayerController.Instance.gameObject.transform.position);
        //Debug.Log("Distance: " + distance);
        
        if (distance <= 10f && enemyController.IsPlayerDetected()) {
            timer += Time.deltaTime;
                
            if (timer >= 1f) {
                timer = 0;
                Shoot();
            }
        }
        else {
            enemyController.LosePlayer();
        }
        

    }

    void Shoot() {
            Instantiate(projectile, projectilePos.position, Quaternion.identity);
        
    }
}
