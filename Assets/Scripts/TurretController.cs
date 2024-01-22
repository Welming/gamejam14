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

    public List<int> combinationList;
    public List<GameObject> combinationAddedItems;

    public GameObject bloodthornObject;
    public GameObject manaBloomObject;
    public GameObject sparkseedObject;

    public GameObject firstSlot;
    public GameObject secondSlot;
    public GameObject giveSlot;

    public bool aoeTurret = false;

    void Start()
    {
        InvokeRepeating("FindTarget", 0.0f, 0.1f);
        gameController = GameObject.Find("Game Controller");
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



    private void AddPlantsToList(int index)
    {
        if(combinationList.Count < 2)
        {
            combinationList.Add(index);
            switch (index)
            {
                case 0:
                    ChangePlantsCount(combinationList[(int)(combinationList.Count - 1)], -1);
                    GameObject placedItem = Instantiate(bloodthornObject);
                    placedItem.GetComponent<OptionInformation>().buttonIndex = 3 + combinationList.Count;
                    combinationAddedItems.Add(Instantiate(placedItem, giveSlot.transform));
                    placedItem.transform.position = giveSlot.transform.position;

                    break;
                case 1:
                    combinationAddedItems.Add(Instantiate(manaBloomObject, giveSlot.transform));
                    break;
                case 2:
                    combinationAddedItems.Add(Instantiate(sparkseedObject, giveSlot.transform));
                    break;
            }
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
                ChangePlantsCount(combinationList[0], -1);
                combinationList.Remove(combinationList[0]);
                Destroy(combinationAddedItems[0]);
                combinationAddedItems.Remove(combinationAddedItems[0]);
                if(combinationAddedItems[0] != null)
                {
                    combinationAddedItems[0].transform.position = firstSlot.transform.position;
                }
                break;
            case 4:
                ChangePlantsCount(combinationList[1], -1);
                combinationList.Remove(combinationList[1]);
                Destroy(combinationAddedItems[1]);
                combinationAddedItems.Remove(combinationAddedItems[1]);
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
                switch (combinationList[0])
                {
                    case 0:
                        gameController.GetComponent<GameController>().bloodthorneCount++;
                        break;

                }
                ChangePlantsCount(combinationList[0], -1);
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

        rangeCircle.transform.localScale = new Vector2((turretRange * 4), (turretRange * 4));

        gameController.GetComponent<GameController>().MenuOptionsCheck(ref currentFocus, menuOptions, menuOptionsList);

        ShowRange();

        if (!aoeTurret && enemyList.Count > 0) TurretShooting();
    }
}
