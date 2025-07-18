using UnityEngine;

public class WaterThrower : MonoBehaviour
{
    public GameObject waterPrefab;
    public Transform throwPoint;

    public int maxWater = 5;
    private int currentWater;

    private Vector2 facingDirection = Vector2.down;
    private float lastThrowTime;
    public float throwCooldown = 0.5f;

    [SerializeField] private Transform rayPoint;
    [SerializeField] private float rayDistance = 2f;

    private bool canRefill = false;

    void Start()
    {
        currentWater = maxWater;
    }

    void Update()
    {
        UpdateFacingDirection();

        // Cast ray to check refill station presence
        RaycastHit2D hitInfo = Physics2D.Raycast(rayPoint.position, facingDirection, rayDistance);
        if (hitInfo.collider != null && hitInfo.collider.CompareTag("RefillStation"))
        {
            canRefill = true;
            Debug.DrawRay(rayPoint.position, facingDirection * rayDistance, Color.green);
        }
        else
        {
            canRefill = false;
            Debug.DrawRay(rayPoint.position, facingDirection * rayDistance, Color.red);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time >= lastThrowTime + throwCooldown)
        {
            TryThrowWater();
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            if (canRefill)
            {
                RefillWater();
            }
            else
            {
                Debug.Log("Not near a refill station.");
            }
        }
    }

    void UpdateFacingDirection()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (x != 0) facingDirection = new Vector2(x, 0);
        else if (y != 0) facingDirection = new Vector2(0, y);
    }

    void TryThrowWater()
    {
        if (currentWater > 0)
        {
            GameObject water = Instantiate(waterPrefab, throwPoint.position, Quaternion.identity);
            water.GetComponent<WaterProjectile>().SetDirection(facingDirection);
            currentWater--;
            lastThrowTime = Time.time;
        }
        else
        {
            Debug.Log("Out of water! Refill at a station.");
        }
    }

    public void RefillWater()
    {
        currentWater = maxWater;
        Debug.Log("Water refilled!");
    }
}
