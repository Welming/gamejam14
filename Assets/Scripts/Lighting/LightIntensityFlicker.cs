using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightIntensityFlicker : MonoBehaviour
{
    private new Light2D light;
    [Range(0.0f, 2.0f)]
    public float minIntensity = 1.0f;
    [Range(0.0f, 1.0f)]
    public float deltaIntensity = 0.5f;
    [Range(0.0f, 1.0f)]
    public float flickerSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        light.intensity = Mathf.PingPong(Time.time * flickerSpeed, deltaIntensity) + minIntensity;
    }
}
