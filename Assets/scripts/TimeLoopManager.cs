using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class TimeLoopManager : MonoBehaviour
{
    [Header("Core Settings")]
    public float loopTime = 60f;
    public TMP_Text timerText;

    [Header("References")]
    public Transform player;
    public Vector3 startPosition;
    public List<GameObject> resettableObjects = new List<GameObject>();
    public FPSMovement playerMovement;

    private float currentTime;
    private bool isResetting;

    void Start() => InitializeGame();

    void InitializeGame()
    {
        currentTime = loopTime;
        ResetPlayer();
        UpdateTimer();
    }

    void Update()
    {
        if (!isResetting)
        {
            currentTime -= Time.deltaTime;
            UpdateTimer();

            if (currentTime <= 0)
                StartCoroutine(ResetLoop());
        }
    }

    IEnumerator ResetLoop()
    {
        isResetting = true;

        // 1. Reset objects first
        foreach (GameObject obj in resettableObjects)
        {
            IResettable resettable = obj.GetComponent<IResettable>();
            resettable?.ResetObject();
        }

        // 2. Reset player
        ResetPlayer();

        // 3. Reset timer
        currentTime = loopTime;
        isResetting = false;

        yield return null;
    }

    void ResetPlayer()
    {
        player.position = startPosition;
        playerMovement.ResetMovement();
    }

    void UpdateTimer()
    {
        timerText.text = $"Time Left: {Mathf.Max(0, Mathf.FloorToInt(currentTime))}";
    }
}