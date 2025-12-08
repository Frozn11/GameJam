using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    
    //Movement
    public float speed = 5f;//переменная, которая хранит скорость движения персонажа.
    public LayerMask whatIsGround; // Перетащи сюда слой Ground
    private Vector2 lastMoveSpeed;
    float fallSpeed;
    
    
    //Jumping
    private bool readyToJump = true;
    private float jumpCooldown = 0.32f;
    public float jumpForce = 7f;
    public float coyoteTime = 0.3f;
    public int jumpsLeft = 1;
    public int maxJumps = 1;
    private int jumpCounterResetTime = 10;
    private int resetJumpCounter;
    
    //Input
    float x, y;

    public bool grounded, jumping, dead, lookingRight, invincible;
    
    // BoxCast parameters - using for ground check
    Vector2 boxSize = new Vector2(1.01f, 0.1f); // x, y
    float distance = 0.99f;
    Vector2 direction = Vector2.down; 
    
    //other
    private Rigidbody2D rb;//переменная, которая отвечает за физическое поведение персонажа
    
    
    public static PlayerController Instance;

    private void Awake() {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();// при запуске игры получвсем физику у персонажа
    }

    void Update() {
        if (!dead) {
            fallSpeed = rb.linearVelocity.y;
            lastMoveSpeed = VectorExtensions.XZVector(rb.linearVelocity);
        }
    }

    public void Jump() {
        if ((grounded || jumpsLeft >= 0) && readyToJump) {
            readyToJump = false;
            jumpsLeft--;
            resetJumpCounter = 0;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); //если условия верны то происходит прыжок
        }
    }

    public void SetInput(Vector2 dir, bool jumping) {
        x = dir.x;
        y = dir.y;
        this.jumping = jumping;
    }

    public void Movement(float x) {
        if (dead) {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            return;
        }
        GroundCheck();
		this.x = x;
        if (readyToJump && jumping) Jump();
        
         rb.linearVelocity = new Vector2(x * speed, rb.linearVelocity.y);//изменяет скорость персонажа по горизонтали

         if (x == 1) {
             Flip(1, true);
         }
         else if (x == -1) {
             Flip(-1, false);
         }
         
         if (!readyToJump) {
             resetJumpCounter++;
             if (resetJumpCounter >= jumpCounterResetTime)
             {
                 ResetJump();
             }
         }
    }
    
    void Flip(float scaleFlip, bool dir)
    {
        lookingRight = dir;
        Vector3 scale = transform.localScale;
        scale.x = scaleFlip;
        transform.localScale = scale;
    }
    
    private void ResetJump() {
        readyToJump = true;
    }
    
    void GroundCheck() {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, boxSize, 0f, Vector2.down, distance, whatIsGround);
        
        if (hit) {
            grounded = true;
            jumpsLeft = maxJumps;
        }
        else {
            Invoke("NotOnGround", coyoteTime);
        }
    }

    void NotOnGround() {
        grounded = false;
    }
    
    public bool IsDead() {
        return dead;
    }
    

    void OnDrawGizmosSelected(){
        Vector2 position = transform.position;
        
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(position + (Vector2)(direction * distance), new Vector2(boxSize.x, boxSize.y));
    }
    

}
