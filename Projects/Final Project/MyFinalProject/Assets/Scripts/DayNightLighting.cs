using UnityEngine;
using UnityEngine.Rendering.Universal;


public class DayNightLighting : MonoBehaviour
{
    
    public TimeManager timeManager; // assign in inspector
    public UnityEngine.Rendering.Universal.Light2D sceneLight; // 2D light to control
    public AnimationCurve intensityCurve;
    public Gradient colorGradient;

    void Start()
    {
        if (timeManager == null)
            timeManager = GetComponent<TimeManager>();
        if (sceneLight == null)
            sceneLight = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
    }

    void Update()
    {
        UpdateLighting();
    }

    void UpdateLighting()
    {
        // Calculate normalized time of day (0-1)
        float timeOfDay = (timeManager.currentHour + timeManager.currentMinute / 60f) / 24f;

        // Update light intensity
        sceneLight.intensity = intensityCurve.Evaluate(timeOfDay);

        // Update light color
        sceneLight.color = colorGradient.Evaluate(timeOfDay);
    }
}
