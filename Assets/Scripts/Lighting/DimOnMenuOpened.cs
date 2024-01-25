using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DimOnMenuOpened : MonoBehaviour
{
    public GameObject gameController;
    public Color initialColor;

    void Start()
    {
        initialColor = gameObject.GetComponent<Light2D>().color;
        gameController = GameObject.Find("Game Controller");
    }

    void Update()
    {
        if(gameController.GetComponent<GameController>().turretMenuOpened && gameObject.GetComponent<Light2D>().color.a >= gameController.GetComponent<GameController>().menuLightDimIntensity)
        {
            gameObject.GetComponent<Light2D>().color *= (1 - Time.deltaTime);
        }
        if (!gameController.GetComponent<GameController>().turretMenuOpened && gameObject.GetComponent<Light2D>().color.r <= 1)
        {
            if(gameObject.GetComponent<Light2D>().color.a * (1 + Time.deltaTime) > 1) 
            {
                gameObject.GetComponent<Light2D>().color = initialColor;
                return;
            }
            gameObject.GetComponent<Light2D>().color *= (1 + (Time.deltaTime * 2));
        }
    }
}
