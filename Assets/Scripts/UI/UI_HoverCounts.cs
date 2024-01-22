using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_HoverCounts : MonoBehaviour
{
    public GameObject boardToSlide;
    public Button button;
    public Sprite upArrow;
    public Sprite downArrow;

    public float slideSpeed;
    public float distanceToSlide;

    Vector3 startingPosition;

    bool menuOpened;

    private void Start()
    {
        startingPosition = boardToSlide.GetComponent<RectTransform>().transform.position;
    }

    public void ClickButton()
    {
        if(menuOpened) {
            menuOpened = false;
        }
        else
        {
            menuOpened = true;
        }
    }

    private void Update()
    {
        if(menuOpened)
        {
            if(button.image.sprite != downArrow)
            {
                button.image.sprite = downArrow;
            }           
            float step = Time.deltaTime * slideSpeed;
            boardToSlide.GetComponent<RectTransform>().transform.position = Vector3.MoveTowards(boardToSlide.GetComponent<RectTransform>().transform.position, new Vector3(startingPosition.x, startingPosition.y + distanceToSlide, startingPosition.y), step);
        }
        else
        {
            if (button.image.sprite != upArrow)
            {
                button.image.sprite = upArrow;
            }
            float step = Time.deltaTime * slideSpeed;
            boardToSlide.GetComponent<RectTransform>().transform.position = Vector3.MoveTowards(boardToSlide.GetComponent<RectTransform>().transform.position, new Vector3(startingPosition.x, startingPosition.y, startingPosition.y), step);
        }
    }
}
