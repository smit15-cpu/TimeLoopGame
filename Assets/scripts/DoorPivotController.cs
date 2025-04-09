using UnityEngine;

public class DoorPivotController : MonoBehaviour
{
    public float openAngle = 90f; // Degrees to open
    public float openSpeed = 90f; // Speed of rotation

    private Quaternion closedRotation;
    private Quaternion openRotation;
    private bool isOpen = false;

    void Start()
    {
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(0, openAngle, 0) * closedRotation;
    }

    void Update()
    {
        if (isOpen)
        {
            // Rotate around the pivot edge
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                openRotation,
                openSpeed * Time.deltaTime
            );
        }
    }

    public void OpenDoor() => isOpen = true;
    public void CloseDoor() => isOpen = false;
}