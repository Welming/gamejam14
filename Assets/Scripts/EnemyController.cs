using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject gameController;

    [SerializeField]
    private float initialMovementSpeed;
    [Range(0.0f, 10.0f)]
    public float enemyMovementSpeed = 1;

    [Range(0, 100)]
    public int enemyHealthPoints = 10;
    [SerializeField]
    private int initialHealthPoints;
    public GameObject healthBar;


    public List<GameObject> directionsList;
    public float distanceBeforeTurning;
    private Animator animator;

    // PUBLIC FOR TESTING
    public bool initiated;

    float slowTimer;
    float slowAmount;

    private int pathIndex;
    private int directionMemory;

    private void MoveOnPath()
    {
        float step = enemyMovementSpeed * Time.deltaTime * gameController.GetComponent<GameController>().enemyScaling;
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, directionsList[pathIndex].transform.position, step);

        if (gameObject.transform.position.x > directionsList[pathIndex].transform.position.x) 
        {
            animator.SetFloat("X", -1);
            directionMemory = -1;
        }
        if (gameObject.transform.position.x < directionsList[pathIndex].transform.position.x)
        {
            animator.SetFloat("X", 1);
            directionMemory = 1;
        }
        if (gameObject.transform.position.x == directionsList[pathIndex].transform.position.x)
        {
            animator.SetFloat("X", directionMemory);
        }

        if (gameObject.transform.position.y > directionsList[pathIndex].transform.position.y)
        {
            animator.SetFloat("Y", -1);
        }
        if (gameObject.transform.position.y < directionsList[pathIndex].transform.position.y)
        {
            animator.SetFloat("Y", 1);
        }
        if (gameObject.transform.position.y == directionsList[pathIndex].transform.position.y)
        {
            animator.SetFloat("Y", -1);
        }

        if (Vector2.Distance(gameObject.transform.position, directionsList[pathIndex].transform.position) <= distanceBeforeTurning)
        {
            pathIndex++;
            if(pathIndex == directionsList.Count)
            {
                gameController.GetComponent<GameController>().TakeDamage();
                Destroy(gameObject);
            }
        }
    }

    private void SlowEnemy()
    {
        if(slowTimer > 0)
        {
            slowTimer -= Time.deltaTime;

        }
        else
        {

        }
    }


    private void Start()
    {
        gameController = GameObject.Find("Game Controller");
        initialHealthPoints = enemyHealthPoints;
        initialMovementSpeed = enemyMovementSpeed;
        animator = gameObject.transform.Find("Model").GetComponent<Animator>().GetComponent<Animator>();
        animator.SetBool("Walk", true);
    }

    private void Update()
    {
        SlowEnemy();

        float healthBarScale = (float)enemyHealthPoints / (float)initialHealthPoints;
        healthBar.transform.localScale = new Vector3(healthBarScale, 1.0f, 1.0f);
        if (initiated) MoveOnPath();
        if(enemyHealthPoints <= 0)
        {
            Destroy(gameObject);
        }
    }
}
