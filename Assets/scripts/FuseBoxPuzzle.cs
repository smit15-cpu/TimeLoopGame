using UnityEngine;
using TMPro;
using System.Collections;

public class FuseBoxPuzzle : MonoBehaviour, IResettable
{
    [Header("References")]
    public KeyCollector playerInventory;
    public GameObject poweredDoor; // Assign door_03 here
    public TMP_Text unlockText;

    private bool fuseUsed;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && IsPlayerNear() && !fuseUsed)
        {
            if (playerInventory.HasFuse())
            {
                fuseUsed = true;
                playerInventory.ResetObject(); // Clear fuse
                poweredDoor.SetActive(false);
                StartCoroutine(ShowUnlockText());
            }
        }
    }

    bool IsPlayerNear() =>
        Vector3.Distance(transform.position, playerInventory.transform.position) < 2f;

    IEnumerator ShowUnlockText()
    {
        unlockText.text = "NEW DOOR UNLOCKED!";
        unlockText.alpha = 1;
        yield return new WaitForSeconds(3f);

        float elapsedTime = 0;
        while (elapsedTime < 1f)
        {
            unlockText.alpha = Mathf.Lerp(1, 0, elapsedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        unlockText.alpha = 0;
    }

    public void ResetObject()
    {
        fuseUsed = false;
        poweredDoor.SetActive(true); // Reset door_03
    }
}