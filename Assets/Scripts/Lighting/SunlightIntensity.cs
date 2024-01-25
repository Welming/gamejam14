using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SunlightIntensity : MonoBehaviour
{
    public GameObject gameController;
    float scalingMultiplier = 1.0f;
    public float startingIntensity;

    private void Awake()
    {
        gameController = GameObject.Find("Game Controller");
        startingIntensity = gameObject.GetComponent<Light2D>().intensity;
    }

    private void OnEnable()
    {
        gameObject.GetComponent<Light2D>().intensity = 0.001f;
        scalingMultiplier = 1.0f;
    }

    private void Update()
    {
        if (gameObject.GetComponent<Light2D>().intensity < startingIntensity)
        {
            scalingMultiplier += gameController.GetComponent<GameController>().lightScalingSpeed * Time.deltaTime;
            gameObject.GetComponent<Light2D>().intensity *= scalingMultiplier;
        }
    }
}