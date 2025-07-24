using UnityEngine;
using UnityEngine.InputSystem;

public class DoorInteractor : MonoBehaviour
{
    [SerializeField] private Transform rayPoint;
    [SerializeField] private float interactDistance = 2f;

    [SerializeField] private KeypadUIManager keypadUI;
    [SerializeField] private RiddleUIManager riddleUI;

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(rayPoint.position, transform.right, interactDistance);

        if (hit.collider != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            Door door = hit.collider.GetComponent<Door>();
            if (door != null)
            {
                switch (door.doorType)
                {
                    case DoorType.Keypad:
                        Keypad keypad = hit.collider.GetComponent<Keypad>();
                        if (keypad != null)
                            keypad.ShowKeypadUI();
                        break;
                    case DoorType.Riddle:
                        riddleUI.ShowRiddle(door);
                        break;
                    case DoorType.Keycard:
                        // You’d call your KeycardManager here if you have one
                        break;
                }
            }
        }
    }
}
