using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;//переменная, которая хранит скорость движения персонажа.
    private Rigidbody2D rb;//переменная, которая отвечает за физическое поведение персонажа
    private Vector2 movement;//переменная, хранящая направление движения.
    public LayerMask whatIsGround; // Перетащи сюда слой Ground
    
    //Jumping
    private bool readyToJump = true;
    private float jumpCooldown = 0.25f;
    public float jumpForce = 7f;
    [HideInInspector]
    public int jumpsLeft = 1;
    private int jumpCounterResetTime = 10;
    private int resetJumpCounter;
    
    //Input
    float x;

    public bool grounded, jumping, dead;
    
    // BoxCast parameters - using for ground check
    Vector2 boxSize = new Vector2(0.8f, 0.1f); // x, y
    float distance = 1f;
    Vector2 direction = Vector2.down; 
    
    public static PlayerController Instance;

    private void Awake() {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();// при запуске игры получвсем физику у персонажа
    }
    

    public void Jump() {
        if ((grounded || jumpsLeft > 0) && readyToJump) {
            readyToJump = false;
            jumpsLeft--;
            resetJumpCounter = 0;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); //если условия верны то происходит прыжок
        }
    }

    public void SetInput(Vector2 dir, bool jumping)
    {
        x = dir.x;
        this.jumping = jumping;
    }

    public void Movement(float x) {
        if (dead) return;
        GroundCheck();
		this.x = x;
        if (readyToJump && jumping) Jump();
        
         rb.linearVelocity = new Vector2(movement.x * speed, rb.linearVelocity.y);//изменяет скорость персонажа по горизонтали
         
         if (!readyToJump) {
             resetJumpCounter++;
             if (resetJumpCounter >= jumpCounterResetTime)
             {
                 ResetJump();
             }
         }
    }
    
    private void ResetJump()
    {
        readyToJump = true;
    }
    
    void GroundCheck() {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, boxSize, 0f, Vector2.down, distance, whatIsGround);
        
        if (hit) {
            grounded = true;
        }
        else {
            grounded = false;
        }
    }
    
    void OnDrawGizmosSelected() // Runs when the GameObject is selected in the Scene view
    {
        
        Vector3 position = transform.position;
        
        // Optional: Draw the box at the end of the cast (blue wireframe)
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(position + (Vector3)(direction * distance), new Vector3(boxSize.x, boxSize.y, 0));
    }
    

}
