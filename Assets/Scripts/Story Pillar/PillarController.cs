using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarController : MonoBehaviour
{
    public GameObject gameController;
    public GameObject menuOptions;
    public List<GameObject> menuOptionsList;
    public GameObject textObject;

    public bool currentFocus;

    public void ActivateButton(int index)
    {
        switch (index)
        {
            case -1:
                gameController.GetComponent<GameController>().turretMenuOpened = false;
                break;
        }
    }

    void Start()
    {
        gameController = GameObject.Find("Game Controller");
        gameController.GetComponent<GameController>().turretMenuOpened = true;
    }

    void FixedUpdate()
    {
        if (currentFocus) { textObject.SetActive(false); }
        if (!currentFocus) { textObject.SetActive(true); }

        if (gameController.GetComponent<GameController>().pauseMenuOpened) { return; }

        gameController.GetComponent<GameController>().MenuOptionsCheck(ref currentFocus, menuOptions, menuOptionsList);
    }
}