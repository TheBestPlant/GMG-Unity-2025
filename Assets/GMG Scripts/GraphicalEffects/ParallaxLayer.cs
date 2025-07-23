using UnityEngine;

[ExecuteInEditMode]
public class ParallaxLayer : MonoBehaviour
{
    [Header("Parallax Factors")]
    public float parallaxFactorX = 1f;
    public float parallaxFactorY = 1f;

    public void Move(Vector2 delta)
    {
        Vector3 newPos = transform.localPosition;
        newPos.x -= delta.x * parallaxFactorX;
        newPos.y -= delta.y * parallaxFactorY;
        transform.localPosition = newPos;
    }
}
