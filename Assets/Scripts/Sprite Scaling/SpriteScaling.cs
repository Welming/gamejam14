using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteScaling : MonoBehaviour
{
    GameObject gameController;
    float scalingMultiplier = 1.0f;

    private void Start()
    {
        gameController = GameObject.Find("Game Controller");
    }

    private void OnEnable()
    {
        gameObject.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        scalingMultiplier = 1.0f;
    }

        private void Update()
    {
        if(gameObject.transform.localScale.x < gameController.GetComponent<GameController>().spriteScaling)
        {
            scalingMultiplier += gameController.GetComponent<GameController>().spriteScalingSpeed * Time.deltaTime;
            gameObject.transform.localScale *= scalingMultiplier;
        }
    }
}