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
     
    public LayerMask whatIsGround; // Перетащи сюда слой Ground
     
    private Vector3 normalVector = Vector3.up;
    public float maxSlopeAngle = 35f;
    public float groundingCancelDelay = 0.15f; 

     
    void Start()//метод который запускается один раз, при старте
    {
         rb = GetComponent<Rigidbody2D>();// при запуске игры получвсем физику у персонажа

    }
    void Update()//метод который вызывается каждый кадр
    {
         movement.x = Input.GetAxis("Horizontal");//определяет, что персонаж будет двигаться только влево или вправо
         if (Input.GetKeyDown(KeyCode.Space) && grounded)//проверка на нажатие клавишь и стоит ли персонаж на земле
         {
             rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);//если условия верны то происходит прыжок
         }
         Debug.DrawRay(transform.position, -transform.up * 1.5f, Color.red, 1);
         RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.5f, whatIsGround);
         if (hit) {
             grounded = true;
         }
         else {
             grounded = false;
         }

    }
     
    void FixedUpdate()//метод вызывается с фиксированным временным шагом и используется для работы с физикой
    {
         rb.linearVelocity = new Vector2(movement.x * speed, rb.linearVelocity.y);//изменяет скорость персонажа по горизонтали
    }
}
