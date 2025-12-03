using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    [Header("References")]
    public Transform player; // Assign the player Transform in Inspector
    private Rigidbody2D rb;
    private Vector2 startPosition;
    NavMeshAgent agent;
    


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        
    }

    void Update()
    {

        
        

    }

    void MoveToPlayer() {
        agent.SetDestination(player.position);
    }
    



}
