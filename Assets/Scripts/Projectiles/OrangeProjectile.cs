using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeProjectile : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float spriteYOffset;

    public bool initiated;

    public int projectileDamage;
    public float aoeRange;
    public GameObject aoeObject;
    public Vector3 startPosition;

    private void Start()
    {
        gameObject.transform.position = startPosition;
    }

    private void AOEDamage()
    {
        GameObject[] tempArray = GameObject.FindGameObjectsWithTag("Enemy");
        if (tempArray.Length == 0) { return; }

        for (int e = 0; e < tempArray.Length; e++)
        {
            if (Vector2.Distance(gameObject.transform.position, tempArray[e].transform.position) <= aoeRange)
            {
                tempArray[e].GetComponent<EnemyController>().enemyHealthPoints -= projectileDamage;
            }          
        }
    }

    void Update()
    {
        if (initiated)
        {
            AOEDamage();
            GameObject newAOE = Instantiate(aoeObject);
            newAOE.transform.position = gameObject.transform.position;
            newAOE.transform.localScale *= aoeRange * 2;
            Destroy(gameObject);
        }
    }
}
