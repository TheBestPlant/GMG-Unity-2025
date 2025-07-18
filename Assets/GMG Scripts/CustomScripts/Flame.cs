using UnityEngine;

public class Flame : MonoBehaviour
{
    void Start()
    {
        // Destroy the projectile after 1 second
        Destroy(gameObject, 1f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
    }
}
