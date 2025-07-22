using UnityEngine;
using System.Collections;

[AddComponentMenu("Playground/Movement/Move With Arrows")]
[RequireComponent(typeof(Rigidbody2D))]
public class Move : Physics2DObject
{
    [Header("Input keys")]
    public Enums.KeyGroups typeOfControl = Enums.KeyGroups.ArrowKeys;

    [Header("Movement")]
    [Tooltip("Speed of movement")]
    public float speed = 5f;
    public Enums.MovementType movementType = Enums.MovementType.AllDirections;

    [Header("Orientation")]
    public bool orientToDirection = false;
    public Enums.Directions lookAxis = Enums.Directions.Up;

    private Vector3 movement, cachedDirection;
    private float moveHorizontal;
    private float moveVertical;

    // Cache last direction for idle animations
    private float lastMoveX = 0f;
    private float lastMoveY = -1f; // Default facing down

    // Animations
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        Debug.Log(animator);
    }

    // Update gets called every frame
    void Update()
    {
        // Input
        if (typeOfControl == Enums.KeyGroups.ArrowKeys)
        {
            moveHorizontal = Input.GetAxis("Horizontal");
            moveVertical = Input.GetAxis("Vertical");
        }
        else if (typeOfControl == Enums.KeyGroups.WASD)
        {
            moveHorizontal = Input.GetAxis("Horizontal2");
            moveVertical = Input.GetAxis("Vertical2");
        }

        // Deadzone to prevent flickering
        if (Mathf.Abs(moveHorizontal) < 0.01f) moveHorizontal = 0f;
        if (Mathf.Abs(moveVertical) < 0.01f) moveVertical = 0f;

        // Constrain movement
        switch (movementType)
        {
            case Enums.MovementType.OnlyHorizontal:
                moveVertical = 0f;
                break;
            case Enums.MovementType.OnlyVertical:
                moveHorizontal = 0f;
                break;
        }

        movement = new Vector3(moveHorizontal, moveVertical);

        // Animator updates
        if (animator != null)
        {
            animator.SetFloat("MoveX", moveHorizontal);
            animator.SetFloat("MoveY", moveVertical);
            animator.SetFloat("Speed", movement.sqrMagnitude);
            animator.SetBool("IsMoving", movement.sqrMagnitude > 0.01f);

            // Cache last move direction if moving
            if (moveHorizontal != 0 || moveVertical != 0)
            {
                lastMoveX = moveHorizontal;
                lastMoveY = moveVertical;
            }

            animator.SetFloat("LastMoveX", lastMoveX);
            animator.SetFloat("LastMoveY", lastMoveY);
        }

        // Rotate the GameObject if needed
        if (orientToDirection)
        {
            if (movement.sqrMagnitude >= 0.01f)
            {
                cachedDirection = movement;
            }
            Utils.SetAxisTowards(lookAxis, transform, cachedDirection);
        }
    }

    // FixedUpdate is called every frame when the physics are calculated
    void FixedUpdate()
    {
        transform.position += movement * Time.deltaTime * speed * 10f;
    }

    // Called by mobile input UI buttons
    public void SetHorizontalInput(int input)
    {
        moveHorizontal = input;
    }

    public void SetMoveLeft()
    {
        Debug.Log("Left");
        moveHorizontal = -1;
    }

    public void SetMoveRight()
    {
        Debug.Log("Right");
        moveHorizontal = 1;
    }

    public void StopMoving()
    {
        moveHorizontal = 0;
    }
}
