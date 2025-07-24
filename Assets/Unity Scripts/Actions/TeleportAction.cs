using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[AddComponentMenu("Playground/Actions/Teleport")]
public class TeleportAction : Action
{
    public GameObject objectToMove;
    public Vector2 newPosition;
    public bool stopMovements = true;

    [Header("UI Feedback")]
    public GameObject teleportImagePrefab; // Prefab with Image component (Canvas -> Image)
    public Canvas uiCanvas; // Assign the main UI Canvas in the Inspector

    public override bool ExecuteAction(GameObject dataObject)
    {
        Rigidbody2D rb2D;

        if (objectToMove != null)
        {
            objectToMove.transform.position = newPosition;
            rb2D = objectToMove.GetComponent<Rigidbody2D>();
        }
        else
        {
            transform.position = newPosition;
            rb2D = transform.GetComponent<Rigidbody2D>();
        }

        if (stopMovements && rb2D != null)
        {
            rb2D.linearVelocity = Vector2.zero;
            rb2D.angularVelocity = 0f;
        }

        // Show the teleport UI image
        if (teleportImagePrefab != null && uiCanvas != null)
        {
            GameObject imageInstance = GameObject.Instantiate(teleportImagePrefab, uiCanvas.transform);
            GameObject.Destroy(imageInstance, 1f); // Destroy after 1 second
        }

        return true;
    }
}
