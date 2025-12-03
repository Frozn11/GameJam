using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;//переменная, которая хранит скорость движения персонажа.
    public float jumpForce = 7f;//переменная, задающая силу прыжка персонажа
    private Rigidbody2D rb;//переменная, которая отвечает за физическое поведение персонажа
    private Vector2 movement;//переменная, хранящая направление движения.
    public bool grounded;//переменная, указывающая, стоит ли персонаж на земле.
     
    public LayerMask whatIsGround; 
     
    private Vector3 normalVector = Vector3.up;
    public float maxSlopeAngle = 35f;
    public float groundingCancelDelay = 0.15f; 

     
    void Start()
    {
         rb = GetComponent<Rigidbody2D>();// при запуске игры получвсем физику у персонажа

    }
    void Update()
    {
         movement.x = Input.GetAxis("Horizontal");//определяет, что персонаж будет двигаться только влево или вправо
         if (Input.GetKeyDown(KeyCode.Space) && grounded)//проверка на нажатие клавишь и стоит ли персонаж на земле
         {
             Debug.Log("Jump");
             rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);//если условия верны то происходит прыжок
         }
         Vector2 boxSize = new Vector2(1, 0.1f); // Example: 0.8 wide, 0.1 tall
         float distance = 1.5f; // Same as ray distance
         RaycastHit2D hit = Physics2D.BoxCast(transform.position, boxSize, 0f, Vector2.down, distance, whatIsGround);
         if (hit) {
             grounded = true;
         }
         else {
             grounded = false;
         }

    }
     
    void FixedUpdate()
    {
         rb.linearVelocity = new Vector2(movement.x * speed, rb.linearVelocity.y);//изменяет скорость персонажа по горизонтали
    }
    
    void OnDrawGizmosSelected() // Runs when the GameObject is selected in the Scene view
    {
        // Match your BoxCast parameters
        Vector2 boxSize = new Vector2(1, 0.1f); // Adjust to your box size
        Vector3 position = transform.position;
        float distance = 1.5f; // Match your cast distance
        Vector2 direction = Vector2.down; // Match your direction
        
        // Optional: Draw the box at the end of the cast (blue wireframe)
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(position + (Vector3)(direction * distance), new Vector3(boxSize.x, boxSize.y, 0));
    }
    
    
}
