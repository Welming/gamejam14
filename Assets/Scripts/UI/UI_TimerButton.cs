using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_TimerButton : MonoBehaviour
{
    public GameObject timerObject;
    public GameObject buttonObject;
    public GameObject buttonTextObject;

    public GameObject gameController;

    void Start()
    {
        gameController = GameObject.Find("Game Controller");
    }

    public void ActivateWave()
    {
        gameController.GetComponent<GameController>().waveActive = true;
        gameController.GetComponent<GameController>().waveTimer = gameController.GetComponent<GameController>().waveLength;
    }
    
    void Update()
    {
        if (gameController.GetComponent<GameController>().pauseMenuOpened) 
        {
            buttonObject.GetComponent<Button>().interactable = false;
            return; 
        }

        if (gameController.GetComponent<GameController>().waveTimer <= 0)
        {
            buttonObject.GetComponent<Button>().interactable = true;
            buttonObject.GetComponent<Image>().color = new Color( 0.3333f, 0.7176f, 0.3333f, 1.0f );
            if(timerObject.activeSelf)
            {
                timerObject.SetActive(false);
            }

            if (!buttonTextObject.activeSelf)
            {
                buttonTextObject.SetActive(true);
            }
        }

        if(gameController.GetComponent<GameController>().waveTimer > 0)
        {
            buttonObject.GetComponent<Button>().interactable = false;
            buttonObject.GetComponent<Image>().color = new Color( 1.0f, 1.0f, 1.0f, 1.0f);
            if (!timerObject.activeSelf)
            {
                timerObject.SetActive(true);
            }

            if(gameController.GetComponent<GameController>().waveTimer < 10)
            {
                timerObject.GetComponent<UI_VariableToText>().initialText = "00:0";
            }
            else
            {
                timerObject.GetComponent<UI_VariableToText>().initialText = "00:";
            }

            if (buttonTextObject.activeSelf)
            {
                buttonTextObject.SetActive(false);
            }
        }
    }
}