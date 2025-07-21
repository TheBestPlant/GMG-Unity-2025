using UnityEngine;
using TMPro;

public class KeypadUIManager : MonoBehaviour
{
    public GameObject keypadPanel;        // The whole keypad UI panel
    public TMP_Text codeDisplay;
    public GameObject keypadDisplay;

    private string currentInput = "";
    private string correctCode = "";
    private string targetDoorID = "";

    private bool isOpen = false;

    void Start()
    {
        // Hide the code display at the start of the game
        if (codeDisplay != null)
        {
            codeDisplay.gameObject.SetActive(false);
            keypadPanel.gameObject.SetActive(false);
            keypadDisplay.gameObject.SetActive(false);
        }
    }

    public void Open(string code, string doorID)
    {
        currentInput = "";
        correctCode = code;
        targetDoorID = doorID;
        isOpen = true;

        keypadPanel.SetActive(true);

        if (codeDisplay != null)
        {
            codeDisplay.gameObject.SetActive(true); // Show display when keypad opens
            keypadPanel.gameObject.SetActive(true);
            keypadDisplay.gameObject.SetActive(true);
        }

        UpdateDisplay();
    }

    public void Close()
    {
        keypadPanel.SetActive(false);
        isOpen = false;
        currentInput = "";

        if (codeDisplay != null)
        {
            codeDisplay.gameObject.SetActive(false); // Hide display when keypad closes
            keypadPanel.gameObject.SetActive(false);
            keypadDisplay.gameObject.SetActive(false);
        }
    }

    public void PressKey(string digit)
    {
        if (!isOpen || currentInput.Length >= 10) return;

        currentInput += digit;
        UpdateDisplay();
    }

    public void ClearInput()
    {
        currentInput = "";
        UpdateDisplay();
    }

    public void Submit()
    {
        if (currentInput == correctCode)
        {
            Debug.Log("Correct code entered for door: " + targetDoorID);

            Door[] doors = FindObjectsOfType<Door>();
            foreach (Door door in doors)
            {
                if (door.doorID == targetDoorID)
                {
                    Destroy(door.gameObject);
                    break;
                }
            }

            Close();
        }
        else
        {
            Debug.Log("Incorrect code.");
            ClearInput();
        }
    }

    void UpdateDisplay()
    {
        codeDisplay.text = currentInput;
    }

    public void TestClick()
    {
        Debug.Log("Button clicked!");
    }

}
