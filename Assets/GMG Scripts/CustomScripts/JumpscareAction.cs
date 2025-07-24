using UnityEngine;

[AddComponentMenu("Playground/Actions/Jumpscare")]
public class JumpscareAction : Action
{
    [Header("UI Setup")]
    public GameObject jumpscareImagePrefab; // UI image prefab
    public Canvas uiCanvas;                 // Reference to Canvas

    [Header("Sound")]
    public AudioClip jumpscareSound;        // Sound to play
    public float volume = 1f;               // Volume of sound

    [Header("Timing")]
    public float displayTime = 1f;          // How long image stays

    public override bool ExecuteAction(GameObject dataObject)
    {
        // Show image
        if (jumpscareImagePrefab != null && uiCanvas != null)
        {
            GameObject imageInstance = GameObject.Instantiate(jumpscareImagePrefab, uiCanvas.transform);
            GameObject.Destroy(imageInstance, displayTime);
        }
        else
        {
            Debug.LogWarning("JumpscareAction: Missing prefab or canvas.");
        }

        // Play sound
        if (jumpscareSound != null)
        {
            AudioSource.PlayClipAtPoint(jumpscareSound, Camera.main.transform.position, volume);
        }
        else
        {
            Debug.LogWarning("JumpscareAction: Missing audio clip.");
        }

        return true;
    }
}