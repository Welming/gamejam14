using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleProjectile : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float spriteYOffset;

    public bool initiated;
    public GameObject targetObject;

    public float projectileLuckDamage;
    public float projectileSpeed;
    public int projectileDamage;
    public Vector3 startPosition;

    public float distanceBeforeImpact = 0.05f;

    private void Start()
    {
        gameObject.transform.position = startPosition;
    }

    private void TrackEnemy()
    {
        projectileSpeed += projectileSpeed * Time.deltaTime;
        float step = projectileSpeed * Time.deltaTime;
        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, targetObject.transform.position, step);
        if(Vector2.Distance(gameObject.transform.position, targetObject.transform.position) <= distanceBeforeImpact)
        {
            int damage = projectileDamage + (int)Mathf.Ceil(Random.Range(-1 * projectileLuckDamage, projectileLuckDamage));
            if(damage < 0) { damage = 0; }
            targetObject.GetComponent<EnemyController>().enemyHealthPoints -= damage;
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if(targetObject != null)
        {

        }
        else
        {
            Destroy(gameObject);
        }

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
