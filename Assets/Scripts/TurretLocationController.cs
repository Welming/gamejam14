using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLocationController : MonoBehaviour
{
    private bool activatedTurret;
    [SerializeField]
    private float hoverGlowTimer;

    public GameObject menuOption;

    public List<GameObject> particleEffects;
    [SerializeField]
    private List<ParticleSystem> particleSystems;

    // Start is called before the first frame update
    void Start()
    {
        for (int e = 0;e < particleEffects.Count;e++)
        {
            particleSystems.Add(particleEffects[e].GetComponent<ParticleSystem>());
            var emission = particleSystems[e].emission;
            emission.enabled = false;
        }
    }



    public void HoverGlow(float glowTimer)
    {
        hoverGlowTimer = glowTimer;
    }

    void HoverGlowTimer()
    {
        if (hoverGlowTimer > 0.0f)
        {
            hoverGlowTimer -= Time.deltaTime;

            for (int e = 0; e < particleEffects.Count; e++)
            {
                var emission = particleSystems[e].emission;
                if(!emission.enabled) emission.enabled = true;
            }
        }
        else
        {
            for (int e = 0; e < particleEffects.Count; e++)
            {
                var emission = particleSystems[e].emission;
                if (emission.enabled) emission.enabled = false;
            }
        }
    }

    void FixedUpdate()
    {
        if(!activatedTurret) HoverGlowTimer();
    }
}
