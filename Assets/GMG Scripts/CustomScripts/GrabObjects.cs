using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class GrabObjects : MonoBehaviour
{
    [SerializeField]
    private Transform grabPoint;

    [SerializeField]
    private Transform rayPoint;

    [SerializeField]
    private float rayDistance = 2f;

    private GameObject grabbedObject;
    private int layerIndex;

    private void Start()
    {
        layerIndex = LayerMask.NameToLayer("Objects");
    }

    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(rayPoint.position, transform.right, rayDistance);

        if (hitInfo.collider != null &&
            hitInfo.collider.gameObject.layer == layerIndex &&
            grabbedObject == null)
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                grabbedObject = hitInfo.collider.gameObject;
                grabbedObject.GetComponent<Rigidbody2D>().isKinematic = true;
                grabbedObject.transform.position = grabPoint.position;
                grabbedObject.transform.SetParent(transform);
            }
        }

        else if (Keyboard.current.spaceKey.wasPressedThisFrame && grabbedObject != null)
        {
            grabbedObject.GetComponent<Rigidbody2D>().isKinematic = false;
            grabbedObject.transform.SetParent(null);

            Vector3 dropOffset = new Vector3(0, -0.25f, 0);
            Vector3 targetPosition = grabPoint.position + dropOffset;

            StartCoroutine(DropObjectSmoothly(grabbedObject, targetPosition));

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
