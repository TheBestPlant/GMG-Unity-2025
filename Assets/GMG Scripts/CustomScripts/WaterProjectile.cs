using UnityEngine;

public class WaterProjectile : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 moveDirection;

    void Start()
    {
        // Destroy the projectile after 2 seconds
        Destroy(gameObject, 2f);
    }

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;
        FlipSprite(direction);
    }

    void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    private void FlipSprite(Vector2 direction)
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null) return;

        // Reset flips first
        sr.flipX = false;
        sr.flipY = false;

        // Flip logic
        if (direction.x < 0)
        {
            sr.flipX = true; // Flip horizontally when facing left
        }
        else if (direction.y > 0)
        {
            sr.flipY = false; // Facing up — no flip
        }
        else if (direction.y < 0)
        {
            // Facing down — explicitly no flip
            sr.flipX = false;
            sr.flipY = false;
        }
    }
}
