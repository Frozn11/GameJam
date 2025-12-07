using UnityEngine;

public class PlayerCreateProjectile : MonoBehaviour {
    public GameObject projectile;
    public Transform projectileSpawnPos;
    public float CoolDownTime = 0.5f;
    public bool shoot, coolDown;
    

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
        //moves projectileSpawnPos.position.x by .33 meters
        Vector2 Pos = new Vector2(projectileSpawnPos.position.x, projectileSpawnPos.position.y);
        Instantiate(projectile, Pos, projectileSpawnPos.rotation);
    }

    public void Flip(float flip) {
        Vector3 localScale = projectileSpawnPos.localScale;
        localScale.x = flip; // change's the X scale
        projectileSpawnPos.localScale = localScale;
    }

    void ResetCoolDown() {
        coolDown = false;
    }
}
