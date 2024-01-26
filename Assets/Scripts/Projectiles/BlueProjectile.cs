using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueProjectile : MonoBehaviour
{
    public GameObject gameController;

    [Range(0.0f, 1.0f)]
    public float spriteYOffset;

    public bool initiated;
    public GameObject targetObject;

    public float projectileSpeed;
    public int projectileDamage;
    public float projectileSlowness;
    public float projectileSlowLength;
    public float aoeRange;
    public GameObject aoeObject;
    public Vector3 startPosition;

    public float distanceBeforeImpact = 0.05f;

    private void Start()
    {
        gameController = GameObject.Find("Game Controller");
        gameObject.transform.position = startPosition;
    }

    private void TrackEnemy()
    {
        projectileSpeed += projectileSpeed * Time.deltaTime;
        float step = projectileSpeed * Time.deltaTime;
        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, targetObject.transform.position, step);
        if (Vector2.Distance(gameObject.transform.position, targetObject.transform.position) <= distanceBeforeImpact)
        {
            AOEEffect();
            targetObject.GetComponent<EnemyController>().enemyHealthPoints -= projectileDamage;
            GameObject newAOE = Instantiate(aoeObject);
            newAOE.transform.position = targetObject.transform.position;
            newAOE.transform.localScale *= aoeRange * 2;
            Destroy(gameObject);
        }
    }

    private void AOEEffect()
    {
        GameObject[] tempArray = GameObject.FindGameObjectsWithTag("Enemy");
        if (tempArray.Length == 0) { return; }

        for (int e = 0; e < tempArray.Length; e++)
        {
            if (Vector2.Distance(gameObject.transform.position, tempArray[e].transform.position) <= aoeRange)
            {
                if (tempArray[e].GetComponent<EnemyController>().slowTimer <= projectileSlowLength)
                {
                    tempArray[e].GetComponent<EnemyController>().slowTimer = projectileSlowLength;
                }
                
                if(tempArray[e].GetComponent<EnemyController>().slowAmount <= projectileSlowness)
                {
                    tempArray[e].GetComponent<EnemyController>().slowAmount = projectileSlowness;
                }
            }          
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
