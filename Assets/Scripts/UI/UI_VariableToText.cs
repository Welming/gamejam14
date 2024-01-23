using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_VariableToText : MonoBehaviour
{
    public GameObject gameController;
    public TMP_Text textComponent;

    public int importantIndex;
    private int importantVariable;

    public string initialText;
    public bool flipTexts = false;

    void Start()
    {
        gameController = GameObject.Find("Game Controller");
    }

    void Update()
    {
        switch (importantIndex)
        {
            // ENERGY
            case 0:
                importantVariable = gameController.GetComponent<GameController>().emberCount;
                break;
            // BLOODTHORNE
            case 1:
                importantVariable = gameController.GetComponent<GameController>().bloodthorneCount;
                break;
            // MANA BLOOM
            case 2:
                importantVariable = gameController.GetComponent<GameController>().manaBloomCount;
                break;
            // SPARKSEED
            case 3:
                importantVariable = gameController.GetComponent<GameController>().sparkseedCount;
                break;
        }

        if (!flipTexts) { textComponent.SetText(initialText + importantVariable); }
        else { textComponent.SetText(importantVariable + initialText); }
    }
}