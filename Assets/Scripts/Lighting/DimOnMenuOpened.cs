using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimOnMenuOpened : MonoBehaviour
{
    public GameObject gameController;

    void Start()
    {
        gameController = GameObject.Find("Game Controller");
    }

    void Update()
    {
        
    }
}
