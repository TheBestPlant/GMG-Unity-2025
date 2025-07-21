using UnityEngine;

public class MonsterPatrolChase : MonoBehaviour
{
    [Header("Patrol Settings")]
    public Transform pointA;
    public Transform pointB;
    public float patrolSpeed = 2f;

    [Header("Chase Settings")]
    public Transform player;
    public float chaseSpeed = 4f;
    public float detectionRange = 5f;
    public float fieldOfViewAngle = 120f; // Angle in degrees to detect player in front

    public AudioSource audioSource;
    public AudioClip monsterChaseSound;
    public AudioClip returnToPatrolSound;
    public AudioClip monsterIdleSound;

    private Vector3 currentTarget;
    private Rigidbody2D rb;
    private bool chasing = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentTarget = pointB.position;
    }

    void Update()
    {
        if (player == null) return;

        // Check if player is detected
        if (CanSeePlayer())
        {
            chasing = true;
        }
        else
        {
            chasing = false;
        }

        if (chasing)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    private bool CanSeePlayer()
    {
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer > detectionRange)
            return false;

        // Check if player is roughly in front (field of view)
        Vector2 facingDirection = rb.linearVelocity.x >= 0 ? Vector2.right : Vector2.left;
        float angle = Vector2.Angle(facingDirection, directionToPlayer);

        if (angle < fieldOfViewAngle / 2f)
        {
            // Optional: you could add a raycast here to check line of sight (no walls blocking)
            return true;
        }

        return false;
    }

    private void Patrol()
    {
        float step = patrolSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, currentTarget, step);

        // Switch target if close enough
        if (Vector2.Distance(transform.position, currentTarget) < 0.1f)
        {
            currentTarget = (currentTarget == pointB.position) ? pointA.position : pointB.position;
        }

        // Flip sprite to face movement direction (preserve scale)
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (currentTarget.x > transform.position.x ? 1 : -1);
        transform.localScale = scale;
    }

    private void ChasePlayer()
    {
        float step = chaseSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, player.position, step);

        // Flip sprite to face player (preserve scale)
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (player.position.x > transform.position.x ? 1 : -1);
        transform.localScale = scale;
    }

}
