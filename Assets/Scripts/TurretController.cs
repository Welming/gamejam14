using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public GameObject turretModel;
    public GameObject projectileType;
    public GameObject rangeCircle;
    public GameObject gameController;
    public GameObject menuOptions;
    public List<GameObject> menuOptionsList;

    [Range(0.0f, 10.0f)]
    public float turretAttackSpeed;
    [Range(0.0f, 100.0f)]
    public float turretProjectileSpeed;
    [Range(0.01f, 5.0f)]
    public float turretProjectileScale = 1.0f;
    [Range(0, 100)]
    public int turretDamage;
    [Range(0.0f, 10.0f)]
    public float turretRange;

    [Range(0.0f, 10.0f)]
    public float projectileSpawnYOffset;

    public bool currentFocus;

    private float attackTimer;
    public List<GameObject> enemyList;

    public bool aoeTurret = false;

    void Start()
    {
        InvokeRepeating("FindTarget", 0.0f, 0.1f);
        gameController = GameObject.Find("Game Controller");
    }

    void TurretShooting()
    {
        if (turretAttackSpeed <= 0) turretAttackSpeed = 0.01f;

        if (attackTimer <= (1 / turretAttackSpeed))
        {
            attackTimer += Time.deltaTime;
        }
        else
        {
            attackTimer = 0;
            turretModel.GetComponent<Animator>().SetTrigger("IsAttacking");
            GameObject newProjectile = Instantiate(projectileType, gameObject.transform);
            newProjectile.GetComponent<ProjectileController>().startPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + projectileSpawnYOffset, gameObject.transform.position.z);
            newProjectile.GetComponent<ProjectileController>().projectileSpeed = turretProjectileSpeed;
            newProjectile.GetComponent<ProjectileController>().projectileDamage = turretDamage;
            newProjectile.GetComponent<ProjectileController>().transform.localScale *= turretProjectileScale;
            for (int e = 0; e < enemyList.Count; e++)
            {
                if (enemyList[e] == null)
                {
                    enemyList.Remove(enemyList[e]);
                }
            }
            newProjectile.GetComponent<ProjectileController>().targetObject = enemyList[0];
            newProjectile.GetComponent<ProjectileController>().initiated = true;
        }
    }

    private void FindTarget()
    {
        GameObject[] tempArray = GameObject.FindGameObjectsWithTag("Enemy");
        if (tempArray.Length == 0) { return; }

        for(int e = 0;e < tempArray.Length;e++)
        {
            if (Vector2.Distance(gameObject.transform.position, tempArray[e].transform.position) <= turretRange)
            {
                if (!enemyList.Contains(tempArray[e])) {
                    enemyList.Add(tempArray[e]);
                }
            }
            else
            {
                if (enemyList.Contains(tempArray[e]))
                {
                    enemyList.Remove(tempArray[e]);
                }
            }
        }
    }

    private void Update()
    {
        rangeCircle.transform.localScale = new Vector2((turretRange * 4), (turretRange * 4));

        gameController.GetComponent<GameController>().MenuOptionsCheck(currentFocus, menuOptions, menuOptionsList);

        if (!aoeTurret && enemyList.Count > 0) TurretShooting();
    }
}
