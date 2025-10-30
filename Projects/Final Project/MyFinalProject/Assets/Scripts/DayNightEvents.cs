using UnityEngine;

public class DayNightEvents : MonoBehaviour
{
    public TimeManager timeManager;

    public int sunriseHour = 6;
    public int sunsetHour = 18;

    void Start()
    {
        if (timeManager == null)
            timeManager = GetComponent<TimeManager>();

        timeManager.OnTimeChanged += HandleTimeChanged;
    }

    void HandleTimeChanged(int hour, int minute)
    {
        if (hour == sunriseHour && minute == 0)
        {
            Debug.Log("Sunrise!");
            // Add your sunrise logic here (e.g., change light, spawn NPCs)
        }
        else if (hour == sunsetHour && minute == 0)
        {
            Debug.Log("Sunset!");
            // Add your sunset logic here (e.g., change light, start night events)
        }
    }

    private void OnDestroy()
    {
        timeManager.OnTimeChanged -= HandleTimeChanged;
    }
}

