using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public GameObject turretModel;
    public GameObject projectileType;
    public GameObject rangeCircle;
    public GameObject gameController;
    public GameObject menuOptions;
    public GameObject confirmButton;
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

    public List<int> combinationList;
    public List<GameObject> combinationAddedItems;

    public GameObject bloodthornObject;
    public GameObject manaBloomObject;
    public GameObject sparkseedObject;

    public GameObject firstSlot;
    public GameObject secondSlot;
    public GameObject giveSlot;

    public GameObject redUpgradedTurret;
    public GameObject blueUpgradedTurret;
    public GameObject yellowUpgradedTurret;
    public GameObject orangeUpgradedTurret;
    public GameObject purpleUpgradedTurret;
    public GameObject greenUpgradedTurret;

    public GameObject defaultTurretInformation;

    public GameObject redUpgradedTurretInformation;
    public GameObject blueUpgradedTurretInformation;
    public GameObject yellowUpgradedTurretInformation;
    public GameObject orangeUpgradedTurretInformation;
    public GameObject purpleUpgradedTurretInformation;
    public GameObject greenUpgradedTurretInformation;

    public List<GameObject> listOfTurretInformations;

    void Start()
    {
        InvokeRepeating("FindTarget", 0.0f, 0.1f);
        gameController = GameObject.Find("Game Controller");

        defaultTurretInformation.transform.Find("Attack Speed").GetComponent<TMP_Text>().text = "-Atk Spd = " + turretAttackSpeed.ToString("#0.0");
        defaultTurretInformation.transform.Find("Attack Damage").GetComponent<TMP_Text>().text = "-Atk Dmg = " + turretDamage.ToString("#0.0");
        defaultTurretInformation.transform.Find("Attack Range").GetComponent<TMP_Text>().text = "-Atk Range = " + turretRange.ToString("#0.0");
    }

    private void ChangePlantsCount(int index, int amount)
    {
        switch (index)
        {
            case 0:
                gameController.GetComponent<GameController>().bloodthorneCount += amount;
                break;
            case 1:
                gameController.GetComponent<GameController>().manaBloomCount += amount;
                break;
            case 2:
                gameController.GetComponent<GameController>().sparkseedCount += amount;
                break;
        }
    }

    private void AdjustPlantsList(GameObject placedItem)
    {
        ChangePlantsCount(combinationList[(int)(combinationList.Count - 1)], -1);
        placedItem.GetComponent<OptionInformation>().buttonIndex = 3 + combinationAddedItems.Count;
        placedItem.transform.Find("Name").gameObject.SetActive(false);
        placedItem.transform.Find("Text").gameObject.SetActive(false);
        combinationAddedItems.Add(placedItem);
        placedItem.transform.position = giveSlot.transform.position;
    }

    private void AddPlantsToList(int index)
    {
        if(combinationList.Count < 2)
        {
            combinationList.Add(index);
            switch (index)
            {
                case 0:
                    GameObject placedBloodthorn = Instantiate(bloodthornObject, giveSlot.transform);
                    AdjustPlantsList(placedBloodthorn);
                    break;
                case 1:
                    GameObject placedManaBloom = Instantiate(manaBloomObject, giveSlot.transform);
                    AdjustPlantsList(placedManaBloom);
                    break;
                case 2:
                    GameObject placedSparkseed = Instantiate(sparkseedObject, giveSlot.transform);
                    AdjustPlantsList(placedSparkseed);
                    break;
            }
        }
        if(combinationList.Count == 2)
        {
            ShowInformationUgpradedTurret();
        }
    }

    private void ActivateUgpradedTurret()
    {
        int combinationResult = 0;
        for(int e = 0; e < combinationList.Count;e++)
        {
            if (combinationList[e] == 0) { combinationResult += 1; }
            if (combinationList[e] == 1) { combinationResult += 2; }
            if (combinationList[e] == 2) { combinationResult += 4; }
        }
        switch (combinationResult)
        {
            case 2:
                GameObject newTurret1 = Instantiate(redUpgradedTurret, gameObject.transform.parent);
                newTurret1.transform.position = gameObject.transform.position;
                break;
            case 3:
                GameObject newTurret2 = Instantiate(purpleUpgradedTurret, gameObject.transform.parent);
                newTurret2.transform.position = gameObject.transform.position;
                break;
            case 4:
                GameObject newTurret3 = Instantiate(blueUpgradedTurret, gameObject.transform.parent);
                newTurret3.transform.position = gameObject.transform.position;
                break;
            case 5:
                GameObject newTurret4 = Instantiate(orangeUpgradedTurret, gameObject.transform.parent);
                newTurret4.transform.position = gameObject.transform.position;
                break;
            case 6:
                GameObject newTurret5 = Instantiate(greenUpgradedTurret, gameObject.transform.parent);
                newTurret5.transform.position = gameObject.transform.position;
                break;
            case 8:
                GameObject newTurret6 = Instantiate(yellowUpgradedTurret, gameObject.transform.parent);
                newTurret6.transform.position = gameObject.transform.position;
                break;
        }
        gameController.GetComponent<GameController>().turretMenuOpened = false;
        Destroy(gameObject);
    }

    private void HideInformationUgpradedTurret()
    {
        for(int e = 0; e < listOfTurretInformations.Count;e++)
        {
            if (listOfTurretInformations[e].activeSelf)
            {
                listOfTurretInformations[e].SetActive(false);
            }
        }
    }

    private void ShowInformationUgpradedTurret()
    {
        int combinationResult = 0;
        for (int e = 0; e < combinationList.Count; e++)
        {
            if (combinationList[e] == 0) { combinationResult += 1; }
            if (combinationList[e] == 1) { combinationResult += 2; }
            if (combinationList[e] == 2) { combinationResult += 4; }
        }
        switch (combinationResult)
        {
            case 2:
                redUpgradedTurretInformation.SetActive(true);
                redUpgradedTurretInformation.transform.Find("Attack Speed").GetComponent<TMP_Text>().text = "-Atk Spd = " + redUpgradedTurret.GetComponent<RedTurretController>().turretAttackSpeed.ToString("#0.0");
                redUpgradedTurretInformation.transform.Find("Attack Damage").GetComponent<TMP_Text>().text = "-Atk Dmg = " + redUpgradedTurret.GetComponent<RedTurretController>().turretDamage.ToString();
                redUpgradedTurretInformation.transform.Find("Attack Range").GetComponent<TMP_Text>().text = "-Atk Range = " + redUpgradedTurret.GetComponent<RedTurretController>().turretRange.ToString("#0.0");
                redUpgradedTurretInformation.transform.Find("AOE Range").GetComponent<TMP_Text>().text = "-AOE Range = " + redUpgradedTurret.GetComponent<RedTurretController>().turretAoeRange.ToString("#0.0");
                break;
            case 3:
                purpleUpgradedTurretInformation.SetActive(true);
                purpleUpgradedTurretInformation.transform.Find("Attack Speed").GetComponent<TMP_Text>().text = "-Atk Spd = " + purpleUpgradedTurret.GetComponent<PurpleTurretController>().turretAttackSpeed.ToString("#0.0");
                purpleUpgradedTurretInformation.transform.Find("Attack Damage").GetComponent<TMP_Text>().text = "-Atk Dmg = " + purpleUpgradedTurret.GetComponent<PurpleTurretController>().turretDamage.ToString();
                purpleUpgradedTurretInformation.transform.Find("Attack Range").GetComponent<TMP_Text>().text = "-Atk Range = " + purpleUpgradedTurret.GetComponent<PurpleTurretController>().turretRange.ToString("#0.0");
                purpleUpgradedTurretInformation.transform.Find("Luck Damage").GetComponent<TMP_Text>().text = "-Luck Dmg = " + purpleUpgradedTurret.GetComponent<PurpleTurretController>().turretLuckDamage.ToString();
                break;
            case 4:
                blueUpgradedTurretInformation.SetActive(true);
                blueUpgradedTurretInformation.transform.Find("Attack Speed").GetComponent<TMP_Text>().text = "-Atk Spd = " + blueUpgradedTurret.GetComponent<BlueTurretController>().turretAttackSpeed.ToString("#0.0");
                blueUpgradedTurretInformation.transform.Find("Attack Damage").GetComponent<TMP_Text>().text = "-Atk Dmg = " + blueUpgradedTurret.GetComponent<BlueTurretController>().turretDamage.ToString();
                blueUpgradedTurretInformation.transform.Find("Attack Range").GetComponent<TMP_Text>().text = "-Atk Range = " + blueUpgradedTurret.GetComponent<BlueTurretController>().turretRange.ToString("#0.0");
                blueUpgradedTurretInformation.transform.Find("AOE Range").GetComponent<TMP_Text>().text = "-AOE Range = " + blueUpgradedTurret.GetComponent<BlueTurretController>().turretAoeRange.ToString("#0.0");
                blueUpgradedTurretInformation.transform.Find("Slow Time").GetComponent<TMP_Text>().text = "-Slow Length = " + blueUpgradedTurret.GetComponent<BlueTurretController>().turretSlowLength.ToString("#0") + "s";
                blueUpgradedTurretInformation.transform.Find("Slow Amount").GetComponent<TMP_Text>().text = "-Slowness = " + (blueUpgradedTurret.GetComponent<BlueTurretController>().turretSlow * 100).ToString() + "%";
                break;
            case 5:
                orangeUpgradedTurretInformation.SetActive(true);
                orangeUpgradedTurretInformation.transform.Find("Attack Speed").GetComponent<TMP_Text>().text = "-Atk Spd = " + orangeUpgradedTurret.GetComponent<OrangeTurretController>().turretAttackSpeed.ToString("#0.0");
                orangeUpgradedTurretInformation.transform.Find("Attack Damage").GetComponent<TMP_Text>().text = "-Atk Dmg = " + orangeUpgradedTurret.GetComponent<OrangeTurretController>().turretDamage.ToString();
                orangeUpgradedTurretInformation.transform.Find("AOE Range").GetComponent<TMP_Text>().text = "-AOE Range = " + orangeUpgradedTurret.GetComponent<OrangeTurretController>().turretAoeRange.ToString("#0.0");
                break;
            case 6:
                greenUpgradedTurretInformation.SetActive(true);
                greenUpgradedTurretInformation.transform.Find("Attack Speed").GetComponent<TMP_Text>().text = "-Atk Spd = " + greenUpgradedTurret.GetComponent<GreenTurretController>().turretAttackSpeed.ToString("#0.0");
                greenUpgradedTurretInformation.transform.Find("Attack Damage").GetComponent<TMP_Text>().text = "-Atk Dmg = " + greenUpgradedTurret.GetComponent<GreenTurretController>().turretDamage.ToString();
                greenUpgradedTurretInformation.transform.Find("Attack Range").GetComponent<TMP_Text>().text = "-Atk Range = " + greenUpgradedTurret.GetComponent<GreenTurretController>().turretRange.ToString("#0.0");
                greenUpgradedTurretInformation.transform.Find("Poison Ticks").GetComponent<TMP_Text>().text = "-Poison Ticks = " + greenUpgradedTurret.GetComponent<GreenTurretController>().turretTicks.ToString();
                greenUpgradedTurretInformation.transform.Find("Poison Damage").GetComponent<TMP_Text>().text = "-Poison Dmg = " + greenUpgradedTurret.GetComponent<GreenTurretController>().turretPoisonDamage.ToString();
                break;
            case 8:
                yellowUpgradedTurretInformation.SetActive(true);
                yellowUpgradedTurretInformation.transform.Find("Attack Speed").GetComponent<TMP_Text>().text = "-Atk Spd = " + yellowUpgradedTurret.GetComponent<YellowTurretController>().turretAttackSpeed.ToString("#0.0");
                yellowUpgradedTurretInformation.transform.Find("Attack Damage").GetComponent<TMP_Text>().text = "-Atk Dmg = " + yellowUpgradedTurret.GetComponent<YellowTurretController>().turretDamage.ToString();
                yellowUpgradedTurretInformation.transform.Find("Attack Range").GetComponent<TMP_Text>().text = "-Atk Range = " + yellowUpgradedTurret.GetComponent<YellowTurretController>().turretRange.ToString("#0.0");
                break;
        }
    }

    public void ActivateButton(int index)
    {
        if(combinationList.Count == 0) { giveSlot = firstSlot; }
        if(combinationList.Count == 1) { giveSlot = secondSlot; }
        switch (index)
        {
            case -1:
                gameController.GetComponent<GameController>().turretMenuOpened = false;
                break;
            case 0:
                if (gameController.GetComponent<GameController>().bloodthorneCount > 0)
                {
                    AddPlantsToList(0);
                }
                break;
            case 1:
                if (gameController.GetComponent<GameController>().manaBloomCount > 0)
                {
                    AddPlantsToList(1);
                }
                break;
            case 2:
                if (gameController.GetComponent<GameController>().sparkseedCount > 0)
                {
                    AddPlantsToList(2);
                }
                break;
            case 3:
                ChangePlantsCount(combinationList[0], 1);
                combinationList.Remove(combinationList[0]);
                Destroy(combinationAddedItems[0]);
                combinationAddedItems.Remove(combinationAddedItems[0]);
                if(combinationAddedItems.Count > 0) 
                { 
                    combinationAddedItems[0].transform.position = firstSlot.transform.position;
                    combinationAddedItems[0].GetComponent<OptionInformation>().buttonIndex = 3;
                }
                HideInformationUgpradedTurret();
                break;
            case 4:
                ChangePlantsCount(combinationList[1], 1);
                combinationList.Remove(combinationList[1]);
                Destroy(combinationAddedItems[1]);
                combinationAddedItems.Remove(combinationAddedItems[1]);
                HideInformationUgpradedTurret();
                break;
            case 5:
                ActivateUgpradedTurret();
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
            newProjectile.GetComponent<ProjectileController>().startPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - projectileSpawnYOffset, gameObject.transform.position.z);
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
            if(enemyList[0] != null)
            {
                newProjectile.GetComponent<ProjectileController>().targetObject = enemyList[0];
                newProjectile.GetComponent<ProjectileController>().initiated = true;
            }
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

    private void ShowRange()
    {
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
        if(!currentFocus && combinationAddedItems.Count > 0)
        {
            for(int i = 0; i < combinationAddedItems.Count;i++)
            {
                HideInformationUgpradedTurret();
                ChangePlantsCount(combinationList[0], 1);
                combinationList.Remove(combinationList[0]);
                Destroy(combinationAddedItems[0]);
                combinationAddedItems.Remove(combinationAddedItems[0]);
            }
        }
        for (int i = 0; i < enemyList.Count; i++)
        {
            if (enemyList[i] == null)
            {
                enemyList.Remove(null);
            }
        }

        if(combinationAddedItems.Count == 2)
        {
            if(!confirmButton.activeSelf)
            {
                confirmButton.SetActive(true);
            }
        }
        else
        {
            if (confirmButton.activeSelf)
            {
                confirmButton.SetActive(false);
            }
        }

        rangeCircle.transform.localScale = new Vector2((turretRange * 4), (turretRange * 4));

        gameController.GetComponent<GameController>().MenuOptionsCheck(ref currentFocus, menuOptions, menuOptionsList);

        ShowRange();

        if (enemyList.Count > 0) TurretShooting();
    }
}
