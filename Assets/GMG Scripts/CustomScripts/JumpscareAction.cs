using UnityEngine;

[AddComponentMenu("Playground/Actions/Jumpscare")]
public class JumpscareAction : Action
{
    [Header("UI Setup")]
    public GameObject jumpscareImagePrefab; // UI image prefab
    public Canvas uiCanvas;                 // Reference to Canvas

    [Header("Sound")]
    public AudioSource audioSource;         // Assigned AudioSource in scene
    public AudioClip jumpscareSound;        // Sound to play
    public float volume = 1f;               // Volume of sound

    [Header("Timing")]
    public float displayTime = 1f;          // How long image stays

    public override bool ExecuteAction(GameObject dataObject)
    {
        // Show jumpscare image
        if (jumpscareImagePrefab != null && uiCanvas != null)
        {
            GameObject imageInstance = GameObject.Instantiate(jumpscareImagePrefab, uiCanvas.transform);
            GameObject.Destroy(imageInstance, displayTime);
        }
        else
        {
            Debug.LogWarning("JumpscareAction: Missing prefab or canvas.");
        }

        // Play sound using assigned AudioSource
        if (audioSource != null && jumpscareSound != null)
        {
            audioSource.clip = jumpscareSound;
            audioSource.volume = volume;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("JumpscareAction: Missing AudioSource or AudioClip.");
        }

        return true;
    }
}
