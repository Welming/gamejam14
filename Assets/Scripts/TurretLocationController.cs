using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLocationController : MonoBehaviour
{
    [SerializeField]
    private float hoverGlowTimer;

    public bool currentFocus;
    public GameObject menuOptions;

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

    public void CreateTurret()
    {

    }

    public void ActivateButton(int index)
    {
        switch (index)
        {
            case 0:
                CreateTurret();
                break;
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
        if(currentFocus)
        {
            if (!menuOptions.activeSelf) 
            {
                menuOptions.SetActive(true);
            }
        }
        else
        {
            if (menuOptions.activeSelf)
            {
                menuOptions.SetActive(false);
            }
        }

        HoverGlowTimer();
    }
}
