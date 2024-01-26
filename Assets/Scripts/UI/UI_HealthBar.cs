using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HealthBar : MonoBehaviour
{
    public GameObject gameController;

    float initialWidth;
    int initialHealth;

    void Start()
    {
        gameController = GameObject.Find("Game Controller");
        initialWidth = gameObject.GetComponent<RectTransform>().sizeDelta.x;
        initialHealth = gameController.GetComponent<GameController>().healthPoints;
    }

    void Update()
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(initialWidth * ((float)gameController.GetComponent<GameController>().healthPoints / (float)initialHealth), gameObject.GetComponent<RectTransform>().sizeDelta.y);
    }
}
