using UnityEngine;

public enum DoorType { Keycard, Keypad, Riddle }

public class Door : MonoBehaviour
{
    public string doorID;
    public DoorType doorType;

    // For riddles
    [TextArea] public string riddleQuestion;
    public string[] answerChoices = new string[4]; // A, B, C, D
    public int correctAnswerIndex = 0; // 0 = A, 1 = B, etc.
    public TeleportAction failTeleport; // assign in Inspector
}
