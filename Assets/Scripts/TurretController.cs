using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> enemyList;
    private CircleCollider2D targetArea;

    public GameObject turretModel;


    // Start is called before the first frame update
    void Awake()
    {
        targetArea = GetComponent<CircleCollider2D>();
        
    }

    private void OnTriggerEnter2D(Collider2D newGuy)
    {
        Debug.Log("Hello");
        enemyList.Add(newGuy.gameObject.transform.parent.gameObject);
    }

    private void OnTriggerExit2D(Collider2D newGuy)
    {
        Debug.Log("Goodbye");
        enemyList.Remove(newGuy.gameObject.transform.parent.gameObject);
    }

    private void Update()
    {
        if(enemyList.Count > 0)
        {
            turretModel.GetComponent<Animator>().SetTrigger("IsAttacking");
        }
    }

}
