using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> enemyList;

    public GameObject turretModel;

    [Range(0.0f, 10.0f)]
    public float turretAttackSpeed;
    public float turretDamage;

    private float attackTimer;

    public bool aoeTurret = false;

    void Awake()
    {
        
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
        }
    }

    private void OnTriggerStay2D(Collider2D newGuy)
    {
        if(!enemyList.Contains(newGuy.gameObject.transform.parent.gameObject))
        {
            Debug.Log("Hello");
            enemyList.Add(newGuy.gameObject.transform.parent.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D newGuy)
    {
        Debug.Log("Goodbye");
        enemyList.Remove(newGuy.gameObject.transform.parent.gameObject);
    }

    private void Update()
    {
        if (!aoeTurret && enemyList.Count > 0) TurretShooting();
    }

}
