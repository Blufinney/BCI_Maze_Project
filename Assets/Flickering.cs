using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightFlashController : MonoBehaviour
{
    public float frequency = 15.0f; // Flashing frequency in Hertz
    public Color lightColor = Color.white; // Default light color

    private Light pointLight;
    private float nextActionTime = 0.0f;
    private bool isLightOn = true;

    void Start()
    {
        pointLight = GetComponent<Light>();
        pointLight.color = lightColor;
    }

    void Update()
    {
        if (Time.time >= nextActionTime)
        {
            // Calculate the next update time
            nextActionTime = Time.time + (1 / frequency);
            isLightOn = !isLightOn; // Toggle light state

            // Update light intensity to simulate flashing
            pointLight.intensity = isLightOn ? 4.0f : 0.0f;
        }
    }

    // Call this method to change the light's color dynamically
    public void ChangeLightColor(Color newColor)
    {
        lightColor = newColor;
        pointLight.color = lightColor;
    }
}

