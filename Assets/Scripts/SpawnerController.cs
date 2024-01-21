using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public GameObject gameController;
    public List<GameObject> enemyPrefabList;
    public List<int> enemyQuantitiesList;
    public List<GameObject> directionsList;

    [Range(0.0f, 10.0f)]
    public float enemySpeedModifier = 1.0f;
    [InspectorName("Health Modifier"), Range(0.0f, 10.0f)]
    public float enemyHealthModifier = 1.0f;

    [InspectorName("Current Scaling"), Range(0.0f, 10.0f)]
    public float enemyScaling = 1.0f;

    private int enemyQuantityLeft;
    private int enemyIndex;
    private float spawnTimer;

    public bool activated;

    void Start()
    {
        gameController = GameObject.Find("Game Controller");
        enemyQuantityLeft = enemyQuantitiesList[enemyIndex];
    }

    void ChangeWave()
    {
        enemyIndex++;
        if(enemyIndex == enemyPrefabList.Count)
        {
            enemyIndex = 0;
        }

        enemyQuantityLeft = enemyQuantitiesList[enemyIndex];
    }

    void DispenseWaves()
    {
        spawnTimer += Time.deltaTime;
        if(spawnTimer >= ( gameController.GetComponent<GameController>().period * gameController.GetComponent<GameController>().periodModifier))
        {
            spawnTimer = 0;
            GameObject enemy = Instantiate(enemyPrefabList[enemyIndex], gameObject.transform);
            enemy.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            enemy.GetComponent<EnemyController>().enemyMovementSpeed = (enemySpeedModifier * enemyScaling * enemy.GetComponent<EnemyController>().enemyMovementSpeed);
            enemy.GetComponent<EnemyController>().enemyHealthPoints = (int)Mathf.Ceil(enemyHealthModifier * enemyScaling * enemy.GetComponent<EnemyController>().enemyHealthPoints);
            enemy.GetComponent<EnemyController>().directionsList = directionsList;
            enemy.GetComponent<EnemyController>().initiated = true;
            enemyQuantityLeft--;
            if (enemyQuantityLeft == 0)
            {
                ChangeWave();
            }
        }
    }

    void Update()
    {
        if(enemyScaling < gameController.GetComponent<GameController>().spawnersMaximumScaling)
        {
            enemyScaling += gameController.GetComponent<GameController>().spawnersScalingSpeed * Time.deltaTime;
        }
        if (activated) DispenseWaves();
    }
}
