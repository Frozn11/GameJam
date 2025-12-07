using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject projectile;
    public Transform projectilePos;

    private float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        float distance = Vector2.Distance(transform.position, PlayerController.Instance.gameObject.transform.position);
        
        if (timer >= 1f) {
            timer = 0;
            Shoot();
        }
    }

    void Shoot() {
            Instantiate(projectile, projectilePos.position, Quaternion.identity);
        
    }
}
