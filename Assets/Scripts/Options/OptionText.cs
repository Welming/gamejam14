using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OptionText : MonoBehaviour
{
    public GameObject gameController;

    void Start()
    {
        gameController = GameObject.Find("Game Controller");
        gameObject.GetComponent<TextMeshPro>().font = gameController.GetComponent<GameController>().font;
    }
}