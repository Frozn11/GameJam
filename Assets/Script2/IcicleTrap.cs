using System;
using UnityEngine;
using UnityEngine.UI;

public class IcicleTrap : MonoBehaviour {
    public LayerMask playerLayer;
    public SpriteRenderer icicleImage;
    public GameObject icicleFalling;
    
    float rayDistance = 10;
    public float coolDownTime;
    public bool playerDetected;
    public bool coolDown;
    
    // Update is called once per frame
    void Update() {
        playerDetected = Physics2D.Raycast(transform.position, Vector2.down, rayDistance, playerLayer);
        Debug.DrawRay(transform.position, Vector2.down * rayDistance, Color.red);

        
        if (playerDetected && !coolDown) {
            coolDown = true;
            Invoke("ResetCoolDown", coolDownTime);
            icicleImage.enabled = false;
            icicleFalling.SetActive(true);
        }
    }

    void ResetCoolDown() {
        coolDown = false;
        icicleImage.enabled = true;
        icicleFalling.SetActive(false);
        
    }
}
