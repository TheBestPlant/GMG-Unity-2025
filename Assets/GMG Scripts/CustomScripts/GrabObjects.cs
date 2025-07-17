using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class GrabObjects : MonoBehaviour
{
    [SerializeField] private Transform grabPoint;
    [SerializeField] private Transform rayPoint;
    [SerializeField] private float rayDistance = 2f;

    private GameObject grabbedObject;
    private int layerIndex;

    public NoteUIManager noteUIManager;

    private string acquiredKeyID = null;
    private bool justPickedUp = false;

    private void Start()
    {
        layerIndex = LayerMask.NameToLayer("Objects");
    }

    void Update()
    {
        justPickedUp = false; // Reset every frame

        RaycastHit2D hitInfo = Physics2D.Raycast(rayPoint.position, transform.right, rayDistance);

        if (hitInfo.collider != null)
        {
            GameObject target = hitInfo.collider.gameObject;

            // --- Pick up Keycard or Grabbable Object ---
            if (target.layer == layerIndex && grabbedObject == null)
            {
                if (Keyboard.current.spaceKey.wasPressedThisFrame)
                {
                    // Keycard logic
                    Keycard keycard = target.GetComponent<Keycard>();
                    if (keycard != null)
                    {
                        acquiredKeyID = keycard.doorID;
                        Destroy(target);
                        Debug.Log($"Picked up keycard for door ID: {acquiredKeyID}");
                        return;
                    }

                    // Normal grabbable object logic
                    grabbedObject = target;
                    grabbedObject.GetComponent<Rigidbody2D>().isKinematic = true;
                    grabbedObject.transform.position = grabPoint.position;
                    grabbedObject.transform.SetParent(transform);

                    // Show note if it's a note
                    Note note = grabbedObject.GetComponent<Note>();
                    if (note != null)
                    {
                        noteUIManager.ShowNote(note.noteMessage);
                    }

                    justPickedUp = true;
                }
            }

            // --- Door Interaction ---
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                Door door = target.GetComponent<Door>();
                if (door != null)
                {
                    if (door.doorID == acquiredKeyID)
                    {
                        Destroy(door.gameObject);
                        Debug.Log($"Unlocked and destroyed door: {door.doorID}");
                    }
                    else
                    {
                        Debug.Log("You don't have the correct keycard for this door.");
                    }
                }
            }
        }

        // --- Drop Logic ---
        if (Keyboard.current.spaceKey.wasPressedThisFrame && grabbedObject != null && !justPickedUp)
        {
            grabbedObject.GetComponent<Rigidbody2D>().isKinematic = false;
            grabbedObject.transform.SetParent(null);

            Vector3 dropOffset = new Vector3(0, -0.25f, 0);
            Vector3 targetPosition = grabPoint.position + dropOffset;

            StartCoroutine(DropObjectSmoothly(grabbedObject, targetPosition));

            // Hide note if it was a note
            if (grabbedObject.CompareTag("Note"))
            {
                noteUIManager.HideNote();
            }

            grabbedObject = null;
        }

        Debug.DrawRay(rayPoint.position, transform.right * rayDistance, Color.red);
    }

    private IEnumerator DropObjectSmoothly(GameObject obj, Vector3 dropToPosition)
    {
        float elapsed = 0f;
        float duration = 0.2f;
        Vector3 start = obj.transform.position;

        while (elapsed < duration)
        {
            if (obj == null) yield break;

            obj.transform.position = Vector3.Lerp(start, dropToPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        obj.transform.position = dropToPosition;
    }
}
