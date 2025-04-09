using UnityEngine;

public class KeyObject : MonoBehaviour, IResettable
{
    [Header("Settings")]
    public string keyID = "Key1"; // Unique per key
    private Vector3 initialPosition;

    void Start() => initialPosition = transform.position;

    public void Collect() => gameObject.SetActive(false);

    public void ResetObject()
    {
        transform.position = initialPosition;
        gameObject.SetActive(true);
    }
}