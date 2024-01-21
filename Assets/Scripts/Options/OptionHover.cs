using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionHover : MonoBehaviour
{
    [SerializeField]
    private float hoverGlowTimer;

    void HoverGlowTimer()
    {
        if (hoverGlowTimer > 0.0f)
        {
            hoverGlowTimer -= Time.deltaTime;

        }
        else
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        HoverGlowTimer();
    }
}
