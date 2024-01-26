using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FadeInScreen : MonoBehaviour
{
    public GameObject gameController;

    public GameObject boardObject;
    public GameObject textObject;
    public GameObject fireImage;

    public float timeBeforeFade;
    public float fadeSpeed;

    private float alpha = 1.0f;

    void Start()
    {
        gameController = GameObject.Find("Game Controller");
        gameController.GetComponent<GameController>().pauseMenuOpened = true;
    }

    void Update()
    {
        if(timeBeforeFade > 0)
        {
            timeBeforeFade -= Time.deltaTime;
        }
        else
        {
            alpha -= Time.deltaTime * fadeSpeed;
            if(alpha < 0) { alpha = 0; }
            boardObject.GetComponent<Image>().color = new Color(boardObject.GetComponent<Image>().color.r, boardObject.GetComponent<Image>().color.g, boardObject.GetComponent<Image>().color.b, alpha);
            fireImage.GetComponent<Image>().color = new Color(fireImage.GetComponent<Image>().color.r, fireImage.GetComponent<Image>().color.g, fireImage.GetComponent<Image>().color.b, alpha);
            textObject.GetComponent<TMP_Text>().color = new Color(textObject.GetComponent<TMP_Text>().color.r, textObject.GetComponent<TMP_Text>().color.g, textObject.GetComponent<TMP_Text>().color.b, alpha);
            if (alpha <= 0) 
            {
                gameController.GetComponent<GameController>().pauseMenuOpened = false;
                Destroy(gameObject); 
            }
        }
    }
}
