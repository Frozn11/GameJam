using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Detection Settings")]
    [Tooltip("The range within which the enemy will start chasing the player.")]
    public float sightRange = 10f;
    
    [Tooltip("The distance at which the enemy will stop moving and stand ready to attack.")]
    public float stoppingDistance = 1.5f;

    [Header("Movement Settings")]
    public float moveSpeed = 4f;

    // A reference to the Player's Transform component
    private Transform playerTransform;

    // The Rigidbody2D component on the enemy
    private Rigidbody2D rb;

    void Start()
    {
        // 1. Найдите игрока GameObject по своему тегу (Убедитесь, что у вашего игрока есть тег "Player")
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player GameObject not found! Make sure your player is tagged 'Player'.");
        }

        // 2. Get the Rigidbody2D component for physics-based movement
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found on the Enemy!");
        }
    }

    void Update()
    {
        if (playerTransform == null)
        {
            // Если мы потеряли игрока или не нашли его, ничего не делать.
            return;
        }

        // Рассчитайте расстояние между противником и игроком.
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        // Проверьте, находится ли игрок в пределах видимости.
        if (distanceToPlayer < sightRange)
        {
            // Игрок замечен! Преследуйте его.
            ChasePlayer(distanceToPlayer);
        }
        else
        {
            // Игрок вне поля зрения, прекратите движение.
            rb.linearVelocity = Vector2.zero;
        }
    }

    void ChasePlayer(float currentDistance)
    {
        // 1. Check if we are outside the stopping distance
        if (currentDistance > stoppingDistance)
        {
            // Calculate the direction vector towards the player
            Vector2 direction = (playerTransform.position - transform.position).normalized;

            // Примените скорость к объекту Rigidbody2D для управления движением.
            // Использовать direction * moveSpeed двигаться в сторону игрока
            rb.linearVelocity = direction * moveSpeed;

            // Обработка переворачивания спрайтов (необязательно, но важно для 2D).
            FlipSprite(direction.x);
        }
        else
        {
            // 2. Мы достаточно близко, прекратите движение (перейдите в режим атаки, если это предусмотрено).
            rb.linearVelocity = Vector2.zero;
        }
    }
    
    // Простой способ перевернуть спрайт врага в зависимости от направления движения.
    void FlipSprite(float directionX)
    {
        // Переключайте режим работы только в том случае, если мы значительно продвинемся вперед.
        if (Mathf.Abs(directionX) > 0.1f)
        {
            // Проверьте, влево (< 0) или вправо (> 0).
            if (directionX < 0)
            {
                // Двигаясь влево, повернитесь лицом влево (масштаб X = -1)
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                // Двигаясь вправо, повернитесь лицом вправо (масштаб X = 1)
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
    
}