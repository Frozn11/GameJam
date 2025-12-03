using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    
    PlayerController playerController;
    
    float x;
    
    private bool jumping;
    static public PlayerInput Instance { get; set; }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake() {
        Instance = this;
        playerController = (PlayerController)GetComponent("PlayerController");
    }
    void Update()
    {
        MyInput();
    }
    private void FixedUpdate() {
        playerController.Movement(x);
    }

    // Update is called once per frame

        private void MyInput() {
            if ((bool)playerController) {
                x = Input.GetAxisRaw("Horizontal");
                jumping = Input.GetButton("Jump");
                
                
                playerController.SetInput(new Vector2(x, 0), jumping);
            }
    
        }
}
