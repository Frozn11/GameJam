using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    public int healthCurrent;
    public Image[] healthBarImage;
    public bool dead;


    void Start() {
        healthCurrent = healthBarImage.Length;
    }
    
    void Update() {
        if (dead) {
            Debug.Log("Dead");
        }
    }


    public void Damage(int damage) {
        if (healthCurrent <= 0) return;
        healthCurrent -= damage;
        healthBarImage[healthCurrent].enabled = false;
        if (healthCurrent <= 0) dead = true;
    }   
    public void Heal(int heal) {
        if (healthCurrent >= healthBarImage.Length) return;
        healthCurrent += heal;
        healthBarImage[healthCurrent - 1].enabled = true;
    }
}


