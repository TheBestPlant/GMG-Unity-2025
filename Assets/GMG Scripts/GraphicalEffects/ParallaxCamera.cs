using UnityEngine;

[ExecuteInEditMode]
public class ParallaxCamera : MonoBehaviour
{
    public delegate void ParallaxCameraDelegate(Vector2 deltaMovement);
    public ParallaxCameraDelegate onCameraTranslate;

    private Vector2 oldPosition;

    void Start()
    {
        oldPosition = transform.position;
    }

    void Update()
    {
        Vector2 currentPosition = transform.position;
        Vector2 delta = oldPosition - currentPosition;

        if (delta != Vector2.zero && onCameraTranslate != null)
        {
            onCameraTranslate(delta);
        }

        oldPosition = currentPosition;
    }
}
