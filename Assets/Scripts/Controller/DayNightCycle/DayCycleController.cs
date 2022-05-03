using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class DayCycleController : MonoBehaviour
{
    [Range(0, 24)]
    public float timeOfDay;

    public bool staticSky = true;
    public float orbitSeed = 1.0f;
    public Light sun;
    public Light moon;
    public Volume skyVolume;
    public AnimationCurve starsCurve;

    private bool isNight;
    private PhysicallyBasedSky sky;

    // Start is called before the first frame update
    void Start()
    {
        skyVolume.profile.TryGet(out sky);
    }

    // Update is called once per frame
    void Update()
    {
        if (!staticSky)
        {
            timeOfDay += Time.deltaTime * orbitSeed;
            if (timeOfDay > 24)
                timeOfDay = 0;
            UpdateTime();
        }
    }

    private void OnValidate()
    {
        skyVolume.profile.TryGet(out sky);
        UpdateTime();   
    }

    private void UpdateTime()
    {
        float alpha = timeOfDay / 24.0f;
        float sunRotation = Mathf.Lerp(-90, 270, alpha);
        float moonRotation = sunRotation - 180;

        sun.transform.rotation = Quaternion.Euler(sunRotation, -150.0f, 0);
        moon.transform.rotation = Quaternion.Euler(moonRotation, -150.0f, 0);

        sky.spaceEmissionMultiplier.value = starsCurve.Evaluate(alpha) * 1000.0f;

        CheckNightDayTransition();
    }

    private void CheckNightDayTransition()
    {
        if (isNight)
        {
            if (moon.transform.rotation.eulerAngles.x > 180)
            {
                StartDay();
            }
        }
        else
        {
            if (sun.transform.rotation.eulerAngles.x > 180)
            {
                StartNight();
            }
        }
    }

    private void StartDay()
    {
        isNight = false;
        sun.shadows = LightShadows.Soft;
        moon.shadows = LightShadows.None;
    }
    private void StartNight()
    {
        isNight = true;
        sun.shadows = LightShadows.None;
        moon.shadows = LightShadows.Soft;
    }
}
