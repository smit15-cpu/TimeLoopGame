using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class KeyCollector : MonoBehaviour, IResettable
{
    [Header("UI References")]
    public TMP_Text keyNotificationText;
    public TMP_Text fuseNotificationText;

    [Header("Inventory")]
    private HashSet<string> collectedKeys = new HashSet<string>();
    private bool hasFuse = false;

    void OnTriggerEnter(Collider other)
    {
        // Key Collection
        if (other.CompareTag("Key"))
        {
            KeyObject key = other.GetComponent<KeyObject>();
            if (key != null)
            {
                collectedKeys.Add(key.keyID);
                key.Collect();
                StartCoroutine(ShowNotification(keyNotificationText, $"{key.keyID} Collected!"));
            }
        }

        // Fuse Collection
        if (other.CompareTag("Fuse"))
        {
            FuseItem fuse = other.GetComponent<FuseItem>();
            if (fuse != null)
            {
                hasFuse = true;
                fuse.Collect();
                StartCoroutine(ShowNotification(fuseNotificationText, "Fuse Collected!"));
            }
        }
    }

    public bool HasKey(string keyID) => collectedKeys.Contains(keyID);
    public bool HasFuse() => hasFuse;

    public void ResetObject()
    {
        collectedKeys.Clear();
        hasFuse = false;
    }

    IEnumerator ShowNotification(TMP_Text textElement, string message)
    {
        textElement.text = message;
        textElement.alpha = 1;
        yield return new WaitForSeconds(2f);

        float elapsedTime = 0;
        while (elapsedTime < 1f)
        {
            textElement.alpha = Mathf.Lerp(1, 0, elapsedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        textElement.alpha = 0;
    }
}