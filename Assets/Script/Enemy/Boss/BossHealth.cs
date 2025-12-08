using UnityEngine;

public class BossHealth : MonoBehaviour {

    public int health;
    public int maxHealth = 20;
    
    public bool dead = false;

    void Start() {
        health = maxHealth;
    }
    
    void Update() {
        if (health <= 0) {
            dead = true;
            Destroy(gameObject);
        }
    }
    
    // отнимает ХП
    public void Damage(int damage) {
        health -= damage;
    }
    // добавляет ХП
    public void Heal(int heal) {
        health += heal;
    }
}
