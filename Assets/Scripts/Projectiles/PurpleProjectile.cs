using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleProjectile : MonoBehaviour
{
    public GameObject gameController;
    
    [Range(0.0f, 1.0f)]
    public float spriteYOffset;

    public bool initiated;
    public GameObject targetObject;

    public float projectileLuckDamage;
    public float projectileSpeed;
    public int projectileDamage;
    public Vector3 startPosition;
    private int randomDamage;

    public float distanceBeforeImpact = 0.05f;

    private void Start()
    {
        gameController = GameObject.Find("Game Controller");
        gameObject.transform.position = startPosition;
        randomDamage = projectileDamage + (int)Mathf.Ceil(Random.Range(-1 * projectileLuckDamage, projectileLuckDamage));
        if (randomDamage < 0) { randomDamage = 0; }
        int adjustDamageForScale = randomDamage;
        if(adjustDamageForScale == 0) { adjustDamageForScale++; }
        gameObject.transform.localScale *= 2 * (randomDamage / (projectileDamage + projectileLuckDamage));
    }

    private void TrackEnemy()
    {
        projectileSpeed += projectileSpeed * Time.deltaTime;
        float step = projectileSpeed * Time.deltaTime;
        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, targetObject.transform.position, step);
        if(Vector2.Distance(gameObject.transform.position, targetObject.transform.position) <= distanceBeforeImpact)
        {
            targetObject.GetComponent<EnemyController>().enemyHealthPoints -= randomDamage;
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (gameController.GetComponent<GameController>().pauseMenuOpened) { return; }
        
        if (targetObject != null)
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
