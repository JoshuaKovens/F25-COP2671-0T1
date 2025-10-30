using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
    
    public float secondsPerGameDay = 900f; // 15 minutes per full day

     public int currentHour = 6;
     public int currentMinute = 0;

    public event Action<int, int> OnTimeChanged;

    private float timer = 0f;
    private float secondsPerMinute;

    void Start()
    {
        // Calculate how many real seconds per in-game minute
        secondsPerMinute = secondsPerGameDay / (24f * 60f); // 24h * 60min
    }

    void Update()
    {
        timer += Time.deltaTime;

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

    //  get formatted time
    public string GetTimeString()
    {
        return string.Format("{0:00}:{1:00}", currentHour, currentMinute);
    }
}
