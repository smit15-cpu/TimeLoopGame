using UnityEngine;
using TMPro;

public class KeyboardSafePuzzle : MonoBehaviour, IResettable
{
    // Assign these in Inspector
    public GameObject safeDoor; // Drag SafeDoor here
    public TMP_Text codeDisplayText; // Create a 3D Text object near the safe

    private string enteredCode = "";
    private string correctCode = "3157";
    private bool isPlayerNear = false;

    void Update()
    {
        if (isPlayerNear)
        {
            // Detect number key presses
            if (Input.GetKeyDown(KeyCode.Alpha3)) AddDigit(3);
            if (Input.GetKeyDown(KeyCode.Alpha1)) AddDigit(1);
            if (Input.GetKeyDown(KeyCode.Alpha5)) AddDigit(5);
            if (Input.GetKeyDown(KeyCode.Alpha7)) AddDigit(7);
        }
    }

    void AddDigit(int digit)
    {
        enteredCode += digit.ToString();
        codeDisplayText.text = enteredCode;

        if (enteredCode.Length == 4)
        {
            if (enteredCode == correctCode)
            {
                safeDoor.transform.Rotate(0, 90, 0); // Open door
                codeDisplayText.text = "UNLOCKED!";
            }
            else
            {
                enteredCode = "";
                codeDisplayText.text = "WRONG!";
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) isPlayerNear = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) isPlayerNear = false;
    }

    public void ResetObject()
    {
        enteredCode = "";
        codeDisplayText.text = "----";
        safeDoor.transform.rotation = Quaternion.identity;
    }
}