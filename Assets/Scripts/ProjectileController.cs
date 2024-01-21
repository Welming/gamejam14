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

    public float distanceBeforeImpact = 0.05f;

    private void TrackEnemy()
    {
        projectileSpeed += projectileSpeed * Time.deltaTime;
        float step = projectileSpeed * Time.deltaTime;
        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, targetObject.transform.position, step);
        if(Vector2.Distance(gameObject.transform.position, targetObject.transform.position) <= distanceBeforeImpact)
        {
            targetObject.GetComponent<EnemyController>().enemyHealthPoints -= projectileDamage;
            Destroy(gameObject);
        }
    }

    void Update()
    {
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
