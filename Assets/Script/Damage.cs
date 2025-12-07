using System;
using Unity.VisualScripting;
using UnityEngine;

public enum whoDamage {
    Player,
    Enemy
}

public class Damage : MonoBehaviour {

    public whoDamage whoDamage;
    
    public void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && whoDamage == whoDamage.Player) {
            Debug.Log("MAN GET DAMAGE");
            HealthBar.Instance.Damage(1);
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy") && whoDamage == whoDamage.Enemy) {
            Destroy(other.gameObject);
        }
    }
    
}
