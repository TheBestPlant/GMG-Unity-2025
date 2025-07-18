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

        // Optional: rotate projectile to face direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
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
}
