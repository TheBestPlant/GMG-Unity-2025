using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    [Header("Balloon Prefab")]
    public GameObject balloonPrefab; // <-- This must exist

    public BalloonScript CreateBalloon(string dialogueString, bool usingButton, KeyCode button, float timeToDisappear, Color backgroundC, Color textC, Transform targetObj)
    {
        if (balloonPrefab == null)
        {
            Debug.LogError("DialogueSystem: balloonPrefab is not assigned!");
            return null;
        }

        GameObject balloonGO = Instantiate(balloonPrefab, transform); // assumes you're under a Canvas
        BalloonScript balloon = balloonGO.GetComponent<BalloonScript>();

        if (balloon == null)
        {
            Debug.LogError("DialogueSystem: balloonPrefab is missing a BalloonScript!");
            Destroy(balloonGO);
            return null;
        }

        balloon.Setup(dialogueString, usingButton, button, timeToDisappear, backgroundC, textC, targetObj);
        return balloon;
    }
}
