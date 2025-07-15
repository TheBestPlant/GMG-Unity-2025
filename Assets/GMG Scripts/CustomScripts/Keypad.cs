using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Keypad : MonoBehaviour
{
    public TMP_Text codeDisplay;
    public string correctCode = "1234";
    private string currentInput = "";

    public void PressKey(string key)
    {
        if (currentInput.Length >= 10) return;
        currentInput += key;
        UpdateDisplay();
    }

    public void ClearInput()
    {
        currentInput = "";
        UpdateDisplay();
    }

    public void SubmitCode()
    {
        if (currentInput == correctCode)
        {
            Debug.Log("Correct Code! Unlocking...");
            // Add unlock logic here
        }
        else
        {
            Debug.Log("Wrong Code!");
        }

        ClearInput();
    }

    void UpdateDisplay()
    {
        codeDisplay.text = currentInput;
    }
}

