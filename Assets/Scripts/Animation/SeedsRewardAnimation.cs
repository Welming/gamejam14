using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SeedsRewardAnimation : MonoBehaviour
{
    public GameObject gameController;
    public GameObject playerModel;

    public int rewardType;
    public float yOffset;
    public float xOffset;
    public float speed;
    public float acceleration = 1.2f;
    public float destroyDistance = 0.02f;

    void Start()
    {
        gameController = GameObject.Find("Game Controller");
        playerModel = GameObject.Find(("Player/Model"));
        gameObject.transform.position = new Vector3((playerModel.transform.position.x + Random.Range(-1 * xOffset, xOffset)), playerModel.transform.position.y + yOffset, playerModel.transform.position.y);
    }

    void Update()
    {
        speed *= acceleration;
        float step = speed * Time.deltaTime;
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, playerModel.transform.position, step);
        if(Vector2.Distance(gameObject.transform.position, playerModel.transform.position) <= destroyDistance)
        {
            switch(rewardType)
            {
                case 0:
                    gameController.GetComponent<GameController>().bloodthorneCount++;
                    break;
                case 1:
                    gameController.GetComponent<GameController>().manaBloomCount++;
                    break;
                case 2:
                    gameController.GetComponent<GameController>().sparkseedCount++;
                    break;
            }
            Destroy(gameObject);
        }
    }
}
