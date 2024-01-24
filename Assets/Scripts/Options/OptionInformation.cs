using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OptionInformation : MonoBehaviour
{
    public GameObject gameController;
    public TMP_Text textComponent;
    Vector3 startPosition;

    public int buttonIndex;
    public bool flipAnimation;

    void Start()
    {
        gameController = GameObject.Find("Game Controller");

        if (gameObject.GetComponent<RectTransform>() != null)
        {
            startPosition = gameObject.GetComponent<RectTransform>().localPosition;
        }
        if (gameObject.GetComponent<TextMeshPro>() != null)
        {
            gameObject.GetComponent<TextMeshPro>().font = gameController.GetComponent<GameController>().textFont;
        }

    }

    void Update()
    {
        if (gameObject.GetComponent<RectTransform>() != null)
        {
            if(flipAnimation)
            {
                gameObject.GetComponent<RectTransform>().localPosition = new Vector3((Mathf.SmoothStep(-gameController.GetComponent<GameController>().textEffectDistance + 0.004f, gameController.GetComponent<GameController>().textEffectDistance, Mathf.PingPong(Time.time / gameController.GetComponent<GameController>().textEffectSpeed * -1, 1)) + startPosition.x), startPosition.y, 0.0f);
            }
            else
            {
                gameObject.GetComponent<RectTransform>().localPosition = new Vector3((Mathf.SmoothStep(-gameController.GetComponent<GameController>().textEffectDistance + 0.004f, gameController.GetComponent<GameController>().textEffectDistance, Mathf.PingPong(Time.time / gameController.GetComponent<GameController>().textEffectSpeed, 1)) + startPosition.x), startPosition.y, 0.0f);
            }
        }
    }
}