using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float spriteYOffset;

    public bool initiated;
    public GameObject targetObject;

    public float projectileSpeed;
    public int projectileDamage;
    public Vector3 startPosition;

    private float lerpDistanceValue;

    public float distanceBeforeImpact = 0.05f;

    private void TrackEnemy()
    {
        gameObject.transform.position = Vector3.Lerp(startPosition, targetObject.transform.position, lerpDistanceValue);
        if(Vector2.Distance(gameObject.transform.position, targetObject.transform.position) <= distanceBeforeImpact || lerpDistanceValue >= 1)
        {
            targetObject.GetComponent<EnemyController>().enemyHealthPoints -= projectileDamage;
            Destroy(gameObject);
        }
    }

    void Update()
    {
        lerpDistanceValue += projectileSpeed * Time.deltaTime;

        if (initiated && targetObject != null)
        {
            TrackEnemy();
        }
        else if (initiated)
        {
            Destroy(gameObject);
        }
    }
}
