using UnityEngine;
using System.Collections;

[AddComponentMenu("Playground/Actions/Dialogue Balloon")]
public class DialogueBalloonAction : Action
{
    [Header("Contents")]
    public string textToDisplay = "Hey!";
    public Color backgroundColor = new Color32(113, 132, 146, 255);
    public Color textColor = Color.white;

    [Header("Options")]
    public Transform targetObject;
    public DisappearMode disappearMode = DisappearMode.ButtonPress;
    public float timeToDisappear = 2f;
    public KeyCode keyToPress = KeyCode.Return;

    [Header("Continue dialogue")]
    public DialogueBalloonAction followingText;

    private BalloonScript b;
    private bool balloonIsActive = false;

    public override bool ExecuteAction(GameObject other)
    {
        if (balloonIsActive)
        {
            Debug.Log("Balloon already active. Ignoring additional trigger.");
            return false;
        }

        DialogueSystem d = GameObject.FindObjectOfType<DialogueSystem>();
        if (d == null)
        {
            Debug.LogWarning("DialogueSystem is missing from the scene! Please add it.");
            return false;
        }

        if (d.balloonPrefab == null)
        {
            Debug.LogError("DialogueSystem has no balloonPrefab assigned!");
            return false;
        }

        b = d.CreateBalloon(
            textToDisplay,
            (disappearMode == DisappearMode.ButtonPress),
            keyToPress,
            timeToDisappear,
            backgroundColor,
            textColor,
            targetObject
        );

        if (b == null)
        {
            Debug.LogError("Failed to create balloon. Check CreateBalloon method.");
            return false;
        }

        b.BalloonDestroyed += OnBalloonDestroyed;
        balloonIsActive = true;
        Debug.Log("Balloon successfully created.");

        StartCoroutine(WaitForBallonDestroyed());
        return true;
    }

    private IEnumerator WaitForBallonDestroyed()
    {
        yield return new WaitUntil(() => !balloonIsActive);
    }

    private void OnBalloonDestroyed()
    {
        Debug.Log("Balloon destroyed.");
        if (b != null)
        {
            b.BalloonDestroyed -= OnBalloonDestroyed;
        }

        b = null;
        balloonIsActive = false;

        if (followingText != null)
        {
            Debug.Log("Triggering next dialogue...");
            followingText.ExecuteAction(this.gameObject);
        }
    }

    public enum DisappearMode
    {
        Time = 0,
        ButtonPress = 1,
    }
}
