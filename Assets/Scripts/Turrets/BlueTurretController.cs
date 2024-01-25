using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class BlueTurretController : MonoBehaviour
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
    public GameObject costTextObject;
    public GameObject levelObject;

    public GameObject currentInformationObject;
    public GameObject upgradeInformationObject;
    public GameObject maxLevelInformationObject;

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
    public float turretAoeRange;
    public List<float> aoeRangeLevels;

    [Range(0.0f, 10.0f)]
    public float turretSlow;
    public List<float> slowLevels;

    [Range(0.0f, 10.0f)]
    public float turretSlowLength;
    public List<float> slowLengthLevels;

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
        costTextObject.GetComponent<UI_VariableToText>().initialText = "/" + gameController.GetComponent<GameController>().turretUpgradeCosts[turretLevel].ToString();
    }

    public void UpgradeTurret()
    {
        gameController.GetComponent<GameController>().emberCount -= gameController.GetComponent<GameController>().turretUpgradeCosts[turretLevel];
        turretLevel++;
        turretAttackSpeed = attackSpeedLevels[turretLevel];
        turretDamage = damageLevels[turretLevel];
        turretRange = rangeLevels[turretLevel];

        if (turretLevel == gameController.GetComponent<GameController>().turretUpgradeCosts.Count)
        {
            upgradesMenu.SetActive(false);
            maxLevelMenu.SetActive(true);
            return;
        }
        costTextObject.GetComponent<UI_VariableToText>().initialText = "/" + gameController.GetComponent<GameController>().turretUpgradeCosts[turretLevel].ToString();
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
                    UpgradeTurret();
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
            newProjectile.GetComponent<BlueProjectile>().startPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - projectileSpawnYOffset, gameObject.transform.position.z);
            newProjectile.GetComponent<BlueProjectile>().projectileSpeed = turretProjectileSpeed;
            newProjectile.GetComponent<BlueProjectile>().projectileDamage = turretDamage;
            newProjectile.GetComponent<BlueProjectile>().aoeRange = turretAoeRange;
            newProjectile.GetComponent<BlueProjectile>().projectileSlowness = turretSlow;
            newProjectile.GetComponent<BlueProjectile>().projectileSlowLength = turretSlowLength;
            newProjectile.GetComponent<BlueProjectile>().transform.localScale *= turretProjectileScale;
            for (int e = 0; e < enemyList.Count; e++)
            {
                if (enemyList[e] == null)
                {
                    enemyList.Remove(enemyList[e]);
                }
            }
            if (enemyList.Count > 0)
            {
                newProjectile.GetComponent<BlueProjectile>().targetObject = enemyList[0];
                newProjectile.GetComponent<BlueProjectile>().initiated = true;
                return;
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
        if(levelObject.activeSelf)
        {
            levelObject.GetComponent<TMP_Text>().text = "lvl " + (turretLevel + 1).ToString();
        }
        if(currentInformationObject.activeSelf)
        {
            currentInformationObject.transform.Find("Attack Speed").GetComponent<TMP_Text>().text = "-Atk Spd = " + attackSpeedLevels[turretLevel].ToString("#0.0");
            currentInformationObject.transform.Find("Attack Damage").GetComponent<TMP_Text>().text = "-Atk Dmg = " + damageLevels[turretLevel].ToString();
            currentInformationObject.transform.Find("Attack Range").GetComponent<TMP_Text>().text = "-Atk Range = " + rangeLevels[turretLevel].ToString("#0.0");
            currentInformationObject.transform.Find("AOE Range").GetComponent<TMP_Text>().text = "-Slow Range = " + aoeRangeLevels[turretLevel].ToString("#0.0");
            currentInformationObject.transform.Find("Slow Time").GetComponent<TMP_Text>().text = "-Slow Length = " + slowLengthLevels[turretLevel].ToString("#0") + "s";
            currentInformationObject.transform.Find("Slow Amount").GetComponent<TMP_Text>().text = "-Slowness = " + (slowLevels[turretLevel] * 100).ToString() + "%";
        }
        if (upgradeInformationObject.activeSelf)
        {
            int index = turretLevel + 1;
            if(index >= damageLevels.Count) { index = damageLevels.Count - 1; }
            upgradeInformationObject.transform.Find("Attack Speed").GetComponent<TMP_Text>().text = "-Atk Spd = " + attackSpeedLevels[index].ToString("#0.0");
            upgradeInformationObject.transform.Find("Attack Damage").GetComponent<TMP_Text>().text = "-Atk Dmg = " + damageLevels[index].ToString();
            upgradeInformationObject.transform.Find("Attack Range").GetComponent<TMP_Text>().text = "-Atk Range = " + rangeLevels[index].ToString("#0.0");
            upgradeInformationObject.transform.Find("AOE Range").GetComponent<TMP_Text>().text = "-Slow Range = " + aoeRangeLevels[index].ToString("#0.0");
            upgradeInformationObject.transform.Find("Slow Time").GetComponent<TMP_Text>().text = "-Slow Length = " + slowLengthLevels[index].ToString("#0") + "s";
            upgradeInformationObject.transform.Find("Slow Amount").GetComponent<TMP_Text>().text = "-Slowness = " + (slowLevels[index] * 100).ToString() + "%";
        }
        if (maxLevelInformationObject.activeSelf)
        {
            maxLevelInformationObject.transform.Find("Attack Speed").GetComponent<TMP_Text>().text = "-Atk Spd = " + attackSpeedLevels[turretLevel].ToString("#0.0");
            maxLevelInformationObject.transform.Find("Attack Damage").GetComponent<TMP_Text>().text = "-Atk Dmg = " + damageLevels[turretLevel].ToString();
            maxLevelInformationObject.transform.Find("Attack Range").GetComponent<TMP_Text>().text = "-Atk Range = " + rangeLevels[turretLevel].ToString("#0.0");
            maxLevelInformationObject.transform.Find("AOE Range").GetComponent<TMP_Text>().text = "-Slow Range = " + aoeRangeLevels[turretLevel].ToString("#0.0");
            maxLevelInformationObject.transform.Find("Slow Time").GetComponent<TMP_Text>().text = "-Slow Length = " + slowLengthLevels[turretLevel].ToString("#0") + "s";
            maxLevelInformationObject.transform.Find("Slow Amount").GetComponent<TMP_Text>().text = "-Slowness = " + (slowLevels[turretLevel] * 100).ToString() + "%";
        }

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
