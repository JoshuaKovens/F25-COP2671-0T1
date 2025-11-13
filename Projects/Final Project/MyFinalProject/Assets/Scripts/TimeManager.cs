using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
    [Header("Time Settings")]
    public float secondsPerGameDay = 900f; // 15 min per full day
    public float timeMultiplier = 1f;      // Adjust speed in Inspector

    public int currentHour = 6;
    public int currentMinute = 0;

    public event Action<int, int> OnTimeChanged;

    private float timer = 0f;
    private float secondsPerMinute;

    void Start()
    {
        secondsPerMinute = secondsPerGameDay / (24f * 60f);
    }

    void Update()
    {
        timer += Time.deltaTime * timeMultiplier;

        if (timer >= secondsPerMinute)
        {
            timer -= secondsPerMinute;
            AdvanceTime();
        }
    }

    void AdvanceTime()
    {
        currentMinute++;

        if (currentMinute >= 60)
        {
            currentMinute = 0;
            currentHour++;

            if (currentHour >= 24)
                currentHour = 0;
        }

        OnTimeChanged?.Invoke(currentHour, currentMinute);
    }

    public string GetTimeString()
    {
        return string.Format("{0:00}:{1:00}", currentHour, currentMinute);
    }
}
