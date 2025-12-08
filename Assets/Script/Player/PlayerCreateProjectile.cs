using System.Collections.Generic;
using UnityEngine;

public class PlayerCreateProjectile : MonoBehaviour {
    public GameObject projectile;
    public Transform projectileSpawnPos;
    public float CoolDownTime = 0.5f;
    public bool shoot, coolDown;
    
    public List<GameObject> projectiles = new List<GameObject>();

    public static PlayerCreateProjectile Instance;

    void Awake() {
        Instance = this;
    }
    
    public void SetInput(bool shoot) {
        this.shoot = shoot;
    }

    void Update() {
        if (shoot && !coolDown) {
            coolDown = true;
            CreateProjectile();
            Invoke("ResetCoolDown", CoolDownTime);
        }
    }
    
    public void CreateProjectile() {
        if(projectiles.Count >= 2) return;
        //moves projectileSpawnPos.position.x by .33 meters
        Vector2 Pos = new Vector2(projectileSpawnPos.position.x, projectileSpawnPos.position.y);
        GameObject projectileObj = Instantiate(projectile, Pos, projectileSpawnPos.rotation);
        
        projectiles.Add(projectileObj);
    }

    void ResetCoolDown() {
        coolDown = false;
    }
}

