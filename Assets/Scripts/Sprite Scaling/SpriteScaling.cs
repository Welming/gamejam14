using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteScaling : MonoBehaviour
{
    GameObject gameController;

    private void Start()
    {
        gameController = GameObject.Find("Game Controller");
    }

    private void Update()
    {
        gameObject.transform.localScale = new Vector3(gameController.GetComponent<GameController>().spriteScaling, gameController.GetComponent<GameController>().spriteScaling, gameController.GetComponent<GameController>().spriteScaling);
    }
}
