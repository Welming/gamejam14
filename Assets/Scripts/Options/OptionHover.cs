using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionHover : MonoBehaviour
{
    [SerializeField]
    private float hoverGlowTimer;

    public float colorDimValue = 0.8f;

    public void HoverGlow(float glowTimer)
    {
        hoverGlowTimer = glowTimer;
    }

    void HoverGlowTimer()
    {
        if (hoverGlowTimer > 0.0f)
        {
            hoverGlowTimer -= Time.deltaTime;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f,1.0f,1.0f,1.0f);
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(colorDimValue, colorDimValue, colorDimValue, 1.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        HoverGlowTimer();
    }
}
