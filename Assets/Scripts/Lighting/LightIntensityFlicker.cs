using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightIntensityFlicker : MonoBehaviour
{
    [Range(0.0f, 2.0f)]
    public float minIntensity = 1.0f;
    [Range(0.0f, 1.0f)]
    public float deltaIntensity = 0.5f;
    [Range(0.0f, 1.0f)]
    public float flickerSpeed = 1.0f;

    void Update()
    {
        gameObject.GetComponent<Light2D>().intensity = Mathf.PingPong(Time.time * flickerSpeed, deltaIntensity) + minIntensity;
    }
}
