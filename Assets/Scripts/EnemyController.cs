using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject gameController;
    public GameObject deathAnimation;
    public GameObject rewardAnimation;

    [SerializeField]
    private float initialMovementSpeed;
    [Range(0.0f, 10.0f)]
    public float enemyMovementSpeed = 1;

    public GameObject enemyModel;
    Color initialColor;

    [Range(0, 100)]
    public int enemyHealthPoints = 10;
    [SerializeField]
    public int initialHealthPoints;
    public GameObject healthBar;

    public int embersReward;

    public List<GameObject> directionsList;
    public float distanceBeforeTurning;
    private Animator animator;

    // PUBLIC FOR TESTING
    public bool initiated;

    public float slowTimer;
    public float slowAmount;

    public float poisonTimeLength = 2.0f;
    private float poisonTimer;
    public int poisonTicks;
    public int poisonDamage;

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
            enemyMovementSpeed = initialMovementSpeed * (1 - slowAmount);
            enemyModel.GetComponent<SpriteRenderer>().color = initialColor / 2.0f;
            enemyModel.GetComponent<SpriteRenderer>().color = new Color(enemyModel.GetComponent<SpriteRenderer>().color.r, enemyModel.GetComponent<SpriteRenderer>().color.g, enemyModel.GetComponent<SpriteRenderer>().color.b, 1.0f);
        }
        else
        {
            enemyModel.GetComponent<SpriteRenderer>().color = initialColor;
            enemyMovementSpeed = initialMovementSpeed;
        }
    }

    private void PoisonEnemy()
    {
        if(poisonTicks > 0 && poisonTimer <= 0) 
        {
            poisonTicks--;
            enemyHealthPoints -= poisonDamage;
            poisonTimer = poisonTimeLength;
            enemyModel.GetComponent<SpriteRenderer>().color = initialColor / 2.0f;  
            enemyModel.GetComponent<SpriteRenderer>().color = new Color(enemyModel.GetComponent<SpriteRenderer>().color.r, enemyModel.GetComponent<SpriteRenderer>().color.g, enemyModel.GetComponent<SpriteRenderer>().color.b, 1.0f);
        }
        if (poisonTimer > 0)
        {
            poisonTimer -= Time.deltaTime;
        }
        if(poisonTicks == 0 && poisonTimer <= 0)
        {
            enemyModel.GetComponent<SpriteRenderer>().color = initialColor;
        }
    }

    private void Awake()
    {
        gameController = GameObject.Find("Game Controller");
        initialMovementSpeed = enemyMovementSpeed;
        initialColor = enemyModel.GetComponent<SpriteRenderer>().color;
        animator = gameObject.transform.Find("Model").GetComponent<Animator>().GetComponent<Animator>();
        animator.SetBool("Walk", true);
    }

    private void Update()
    {
        if (gameController.GetComponent<GameController>().pauseMenuOpened) { return; }

        SlowEnemy();
        PoisonEnemy();

        float healthBarScale = (float)((float)enemyHealthPoints / (float)initialHealthPoints);
        if (healthBarScale > 1) { healthBarScale = 1; }
        healthBar.transform.localScale = new Vector3(healthBarScale, 1.0f, 1.0f);
        if (initiated) MoveOnPath();
        if(enemyHealthPoints <= 0)
        {
            GameObject death = Instantiate(deathAnimation);
            death.transform.position = enemyModel.transform.position;
            GameObject reward = Instantiate(rewardAnimation);
            reward.GetComponent<RewardAnimation>().rewardAmount = (int)Mathf.Ceil((float)embersReward * gameController.GetComponent<GameController>().enemyScaling);           
            Destroy(gameObject);
        }
    }
}
