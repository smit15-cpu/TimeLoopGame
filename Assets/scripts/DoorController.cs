using UnityEngine;
using TMPro;
using System.Collections;

public class DoorController : MonoBehaviour, IResettable
{
    [Header("Settings")]
    public string requiredKeyID = "Key1"; // Set this per door

    [Header("References")]
    public GameObject doorObject;
    public KeyCollector keyCollector;
    public TMP_Text lockedText;

    private bool isLocked = true;

    void OnMouseDown()
    {
        if (Vector3.Distance(transform.position, keyCollector.transform.position) < 3f)
        {
            if (!isLocked)
            {
                doorObject.SetActive(false);
            }
            else if (keyCollector.HasKey(requiredKeyID))
            {
                doorObject.SetActive(false);
                isLocked = false;
            }
            else
            {
                StartCoroutine(ShowLockedText());
            }
        }
    }

    IEnumerator ShowLockedText()
    {
        lockedText.text = $"Requires {requiredKeyID}!";
        lockedText.alpha = 1;
        yield return new WaitForSeconds(2f);

        float elapsedTime = 0;
        while (elapsedTime < 1f)
        {
            lockedText.alpha = Mathf.Lerp(1, 0, elapsedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        lockedText.alpha = 0;
    }

    public void ResetObject()
    {
        isLocked = true;
        doorObject.SetActive(true); // Force door to reappear
    }
}