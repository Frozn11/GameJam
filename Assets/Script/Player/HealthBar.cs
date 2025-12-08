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
    
    public void Damage(int damage) {
        if (healthCurrent <= 0) return;
        healthCurrent -= damage;
        healthBarImage[healthCurrent].enabled = false;
        if (healthCurrent <= 0) {
            PlayerController.Instance.dead = true;
            OptionsMenu.Instance.DeadMenu();
        }
    }   
    public void Heal(int heal) {
        if (healthCurrent >= healthBarImage.Length || PlayerController.Instance.IsDead()) return;
        healthCurrent += heal;
        healthBarImage[healthCurrent - 1].enabled = true;
    }
}


