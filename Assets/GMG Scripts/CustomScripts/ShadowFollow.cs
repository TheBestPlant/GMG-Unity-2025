using UnityEngine;

public class ShadowFollow : MonoBehaviour
{
    public Transform anchorPoint; // Center of the room
    public Transform spriteToMove; // The shadow sprite to move
    public Transform player; // Reference to the player
    public BoxCollider2D roomBounds; // Room boundary collider

    [Range(0f, 1f)]
    public float blockFraction = 0.25f; // How much to shift (fraction of a tile)

    private Vector3 initialLocalPosition;

    void Start()
    {
        if (spriteToMove == null) spriteToMove = transform;
        initialLocalPosition = spriteToMove.localPosition;
    }

    void Update()
    {
        if (!roomBounds.bounds.Contains(player.position))
        {
            // Player is not in the room — reset sprite position
            spriteToMove.localPosition = initialLocalPosition;
            return;
        }

        Vector2 direction = GetDirectionFromCenterToPlayer();

        Vector3 offset = new Vector3(direction.x, direction.y, 0f) * blockFraction;
        spriteToMove.localPosition = initialLocalPosition + offset;
    }

    Vector2 GetDirectionFromCenterToPlayer()
    {
        Vector2 toPlayer = player.position - anchorPoint.position;
        Vector2 direction = Vector2.zero;

        // Determine which axis is stronger (dominant movement direction)
        if (Mathf.Abs(toPlayer.x) > Mathf.Abs(toPlayer.y))
        {
            direction.x = toPlayer.x > 0 ? 1 : -1;
        }
        else
        {
            direction.y = toPlayer.y > 0 ? 1 : -1;
        }

        return direction;
    }
}
