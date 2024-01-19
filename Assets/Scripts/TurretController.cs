using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    private bool activatedTurret;
    [SerializeField]
    private float hoverGlowTimer;

    public GameObject detectionSquare;

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
            detectionSquare.GetComponent<SpriteRenderer>().enabled = true;

            for (int e = 0; e < particleEffects.Count; e++)
            {
                var emission = particleSystems[e].emission;
                if(!emission.enabled) emission.enabled = true;
            }
        }
        else
        {
            detectionSquare.GetComponent<SpriteRenderer>().enabled = false;
            for (int e = 0; e < particleEffects.Count; e++)
            {
                var emission = particleSystems[e].emission;
                if (emission.enabled) emission.enabled = false;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!activatedTurret) HoverGlowTimer();
    }
}
