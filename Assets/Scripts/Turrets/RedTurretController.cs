using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RedTurretController : MonoBehaviour
{
    public GameObject turretModel;
    public GameObject projectileType;
    public GameObject rangeCircle;
    public GameObject gameController;
    public GameObject menuOptions;
    public List<GameObject> menuOptionsList;
    public GameObject upgradesMenu;
    public GameObject maxLevelMenu;
    public GameObject acceptButton;

    [Range(0.0f, 10.0f)]
    public float turretAttackSpeed;
    public List<float> attackSpeedLevels;

    [Range(0.0f, 100.0f)]
    public float turretProjectileSpeed;

    [Range(0.01f, 5.0f)]
    public float turretProjectileScale = 1.0f;

    [Range(0, 100)]
    public int turretDamage;
    public List<int> damageLevels;

    [Range(0.0f, 10.0f)]
    public float turretRange;
    public List<float> rangeLevels;

    [Range(0.0f, 10.0f)]
    public float projectileSpawnYOffset;

    public int turretLevel;

    public bool currentFocus;

    private float attackTimer;
    public List<GameObject> enemyList;

    void Start()
    {
        InvokeRepeating("FindTarget", 0.0f, 0.1f);
        gameController = GameObject.Find("Game Controller");
        turretAttackSpeed = attackSpeedLevels[0];
        turretDamage = damageLevels[0];
        turretRange = rangeLevels[0];
    }

    public void UpgradeTurret(int index)
    {

        gameController.GetComponent<GameController>().emberCount -= gameController.GetComponent<GameController>().turretUpgradeCosts[turretLevel];
        turretLevel++;
        turretAttackSpeed = attackSpeedLevels[index];
        turretDamage = damageLevels[index];
        turretRange = rangeLevels[index];

        if(turretLevel == gameController.GetComponent<GameController>().turretUpgradeCosts.Count)
        {
            upgradesMenu.SetActive(false);
            maxLevelMenu.SetActive(true);
        }
        
    }

    public void ActivateButton(int index)
    {
        switch (index)
        {
            case -1:
                gameController.GetComponent<GameController>().turretMenuOpened = false;
                break;
            case 0:
                if (gameController.GetComponent<GameController>().emberCount >= gameController.GetComponent<GameController>().turretUpgradeCosts[turretLevel])
                {
                    UpgradeTurret(turretLevel);
                }
                break;
        }
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
            newProjectile.GetComponent<RedProjectile>().startPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + projectileSpawnYOffset, gameObject.transform.position.z);
            newProjectile.GetComponent<RedProjectile>().projectileSpeed = turretProjectileSpeed;
            newProjectile.GetComponent<RedProjectile>().projectileDamage = turretDamage;
            newProjectile.GetComponent<RedProjectile>().transform.localScale *= turretProjectileScale;
            for (int e = 0; e < enemyList.Count; e++)
            {
                if (enemyList[e] == null)
                {
                    enemyList.Remove(enemyList[e]);
                }
            }
            if (enemyList[0] != null)
            {
                newProjectile.GetComponent<RedProjectile>().targetObject = enemyList[0];
                newProjectile.GetComponent<RedProjectile>().initiated = true;
            }
        }
    }

    private void FindTarget()
    {
        GameObject[] tempArray = GameObject.FindGameObjectsWithTag("Enemy");
        if (tempArray.Length == 0) { return; }

        for (int e = 0; e < tempArray.Length; e++)
        {
            if (Vector2.Distance(gameObject.transform.position, tempArray[e].transform.position) <= turretRange)
            {
                if (!enemyList.Contains(tempArray[e]))
                {
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

    private void ShowRange()
    {
        if(acceptButton.GetComponent<OptionHover>().hoverGlowTimer > 0 && turretLevel != gameController.GetComponent<GameController>().turretUpgradeCosts.Count)
        {
            turretRange = rangeLevels[turretLevel+1];
            
        }
        else
        {
            turretRange = rangeLevels[turretLevel];
        }

        if (currentFocus && !rangeCircle.activeSelf)
        {
            rangeCircle.SetActive(true);
        }
        else if (!currentFocus && rangeCircle.activeSelf)
        {
            rangeCircle.SetActive(false);
        }
    }

    private void Update()
    {
        
        for (int i = 0; i < enemyList.Count; i++)
        {
            if (enemyList[i] == null)
            {
                enemyList.Remove(null);
            }
        }

        rangeCircle.transform.localScale = new Vector2((turretRange * 4), (turretRange * 4));

        gameController.GetComponent<GameController>().MenuOptionsCheck(ref currentFocus, menuOptions, menuOptionsList);

        ShowRange();

        if (enemyList.Count > 0) TurretShooting();
    }
}
