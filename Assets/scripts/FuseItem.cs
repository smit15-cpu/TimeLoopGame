using UnityEngine;

public class FuseItem : MonoBehaviour, IResettable
{
    private Vector3 initialPosition;

    void Start() => initialPosition = transform.position;

    public void Collect() => gameObject.SetActive(false);

    public void ResetObject()
    {
        transform.position = initialPosition;
        gameObject.SetActive(true);
    }
}