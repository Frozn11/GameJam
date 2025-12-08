using System;
using UnityEngine;

[Flags] 
public enum whoDamage {
    None = 1,
    Player = 2,
    Enemy = 4,
    Boss = 8
}

public class Damage : MonoBehaviour {

    [SerializeField]
    public whoDamage whoDamage;
    public bool DestroyOnCollision;
    
    
    public void OnTriggerEnter2D(Collider2D other) {
        // проверяет ли если это игрок
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && (whoDamage & whoDamage.Player) != 0) {
            HealthBar.Instance.Damage(1);
            if (DestroyOnCollision) {
                Destroy(gameObject);
            }
        }   
        // проверяет ли если это босс
        if (other.gameObject.layer == LayerMask.NameToLayer("Boss") && (whoDamage & whoDamage.Boss) != 0) {
            other.GetComponent<BossHealth>().Damage(1);
            if (DestroyOnCollision) {
                gameObject.GetComponent<PlayerProjectile>().RemoveFromList(gameObject);
                
            }
        }
        // проверяет ли если это враг
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy") && (whoDamage & whoDamage.Enemy) != 0) {
            Destroy(other.gameObject);
        }    
        
    }
    
}
