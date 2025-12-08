using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    public int healthCurrent;
    public Image[] healthBarImage;
    
    public static HealthBar Instance;

    private void Awake() {
        Instance = this;
    }

    void Start() {
        healthCurrent = healthBarImage.Length;
    }
    
    // отнимает ХП
    public void Damage(int damage) {
        if (healthCurrent <= 0 || PlayerController.Instance.invincible) return;
        healthCurrent -= damage;
        healthBarImage[healthCurrent].enabled = false;
        if (healthCurrent <= 0) {
            PlayerController.Instance.dead = true;
            OptionsMenu.Instance.DeadMenu();
        }
    }   
    
    // добавляет ХП
    public void Heal(int heal) {
        if (healthCurrent >= healthBarImage.Length || PlayerController.Instance.IsDead()) return;
        healthCurrent += heal;
        healthBarImage[healthCurrent - 1].enabled = true;
    }   
    
    // добавляет ХП до лимита 
    public void HealMax() {
        healthCurrent = 3;
        healthBarImage[0].enabled = true;
        healthBarImage[1].enabled = true;
        healthBarImage[2].enabled = true;
    }
}


