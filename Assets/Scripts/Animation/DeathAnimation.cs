using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnimation : MonoBehaviour
{
    public GameObject emitter;
    public float destroyTimer;
    public float disableTimer;

    void Update()
    {
        if(disableTimer >= 0)
        {
            disableTimer -= Time.deltaTime;
        }
        if(disableTimer <= 0)
        {
            var emission = emitter.GetComponent<ParticleSystem>().emission;
            if (emission.enabled)
            {
                emission.enabled = false;
            }
            destroyTimer -= Time.deltaTime;
            if(destroyTimer <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
