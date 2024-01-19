using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject gameController;

    [InspectorName("Camera Distance"), Range(0.0f, 10.0f)]
    public float monsterMovementSpeed;

    public List<GameObject> directionsList;
    public float distanceBeforeTurning;
    public GameObject monsterModel;
    private Animator animator;

    // PUBLIC FOR TESTING
    public bool initiated;

    private int pathIndex;
    private int directionMemory;

    private int healthPoints;

    public void SetValues(float moveSpeed, List<GameObject> list)
    {
        monsterMovementSpeed = moveSpeed;
        directionsList = list;
        initiated = true;
    }

    private void MoveOnPath()
    {
        float step = monsterMovementSpeed * Time.deltaTime;
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
        animator = gameObject.transform.Find("Model").GetComponent<Animator>().GetComponent<Animator>();
        animator.SetBool("Walk", true);
    }

    // Update is called once per frame
    private void Update()
    {
        if (initiated) MoveOnPath();
    }
}
