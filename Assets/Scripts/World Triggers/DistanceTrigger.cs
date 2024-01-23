using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceTrigger : MonoBehaviour
{
    public GameObject triggeredObject;
    public GameObject trackedObject;
    public float detectionRange;
    public bool enable;

    void FixedUpdate()
    {
        if(Vector2.Distance(gameObject.transform.position, trackedObject.transform.position) <= detectionRange)
        {
            if(!triggeredObject.activeSelf && enable)
            {
                triggeredObject.SetActive(true);
            }

            if (triggeredObject.activeSelf && !enable)
            {
                triggeredObject.SetActive(false);
            }
        }
    }
}
