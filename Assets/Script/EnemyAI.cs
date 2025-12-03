using UnityEngine;
 
public class EnemyAI : MonoBehaviour
{
    [Header("Патрулирование")]
    public Transform pointA;          // Точка A патрулирования
    public Transform pointB;          // Точка B патрулирования
    public float patrolSpeed = 2f;    // Скорость патрулирования
    [Header("Обнаружение игрока")]
    public float detectionRadius = 5f; // Радиус обнаружения
    public float attackRadius = 2f;    // Радиус атаки
    public LayerMask playerLayer;      // Слой игрока
    [Header("Атака")]
    public float attackCooldown = 1f;  // Перезарядка атаки
    public int attackDamage = 10;      // Урон
    private Transform currentTarget;   // Текущая точка патрулирования
    private Transform player;          // Ссылка на игрока
    private float lastAttackTime;      // Время последней атаки
    private bool isPatrolling = true;  // Режим патрулирования
    void Start()
    {
        // Начинаем с точки A
        currentTarget = pointA;
        // Ищем игрока по тегу
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        // Проверяем, виден ли игрок
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRadius)
        {
            // Игрок в радиусе обнаружения!
            isPatrolling = false;
            if (distanceToPlayer <= attackRadius)
            {
                // Игрок в радиусе атаки - атакуем!
                AttackPlayer();
            }
            else
            {
                // Игрок близко, но не для атаки - преследуем
                ChasePlayer();
            }
        }
        else
        {
            // Игрок не виден - патрулируем
            isPatrolling = true;
            Patrol();
        }
    }
    void Patrol()
    {
        // Двигаемся к текущей точке
        transform.position = Vector2.MoveTowards(
            transform.position,
            currentTarget.position,
            patrolSpeed * Time.deltaTime
        );
        // Если достигли точки, меняем цель
        if (Vector2.Distance(transform.position, currentTarget.position) < 0.1f)
        {
            if (currentTarget == pointA)
                currentTarget = pointB;
            else
                currentTarget = pointA;
        }
    }
    void ChasePlayer()
    {
        // Двигаемся к игроку
        transform.position = Vector2.MoveTowards(
            transform.position,
            player.position,
            patrolSpeed * 1.5f * Time.deltaTime  // Быстрее при преследовании
        );
    }
    void AttackPlayer()
    {
        // Проверяем перезарядку атаки
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            Debug.Log("Враг атаковал игрока!");
            lastAttackTime = Time.time;
        }
    }
    // Отображение радиусов в редакторе (для настройки)
    void OnDrawGizmosSelected()
    {
        // Радиус обнаружения (желтый)
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        // Радиус атаки (красный)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}