using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLocationController : MonoBehaviour
{
    public GameObject gameController;

    [SerializeField]
    private float hoverGlowTimer;

    public bool currentFocus;
    public GameObject menuOptions;
    public List<GameObject> menuOptionsList;

    public GameObject turretLocationModel;
    public List<GameObject> particleEffects;

    [SerializeField]
    private List<ParticleSystem> particleSystems;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("Game Controller");

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
        gameController.GetComponent<GameController>().MenuOptionsCheck(currentFocus, menuOptions, menuOptionsList);

        HoverGlowTimer();
    }
}
