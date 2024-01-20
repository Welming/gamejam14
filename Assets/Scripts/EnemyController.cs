using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject gameController;

    [Range(0.0f, 10.0f)]
    public float enemyMovementSpeed = 1;
    [Range(0, 100)]
    public int enemyHealthPoints = 10;


    public List<GameObject> directionsList;
    public float distanceBeforeTurning;
    private Animator animator;

    // PUBLIC FOR TESTING
    public bool initiated;

    private int pathIndex;
    private int directionMemory;

    private void MoveOnPath()
    {
        float step = enemyMovementSpeed * Time.deltaTime;
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

    private void Start()
    {
        gameController = GameObject.Find("Game Controller");
        animator = gameObject.transform.Find("Model").GetComponent<Animator>().GetComponent<Animator>();
        animator.SetBool("Walk", true);
    }

    // Update is called once per frame
    private void Update()
    {
        if (initiated) MoveOnPath();
    }
}
