using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEBlueProjectile : MonoBehaviour
{
    private Color color;
    private float alpha;
    public float fadeSpeed;
    public GameObject gameController;

    void Start()
    {
        gameController = GameObject.Find("Game Controller");
        color = gameObject.GetComponent<SpriteRenderer>().color;
        alpha = color.a;
    }

    void Update()
    {
        if (gameController.GetComponent<GameController>().pauseMenuOpened) { return; }

        if (alpha > 0)
        {
            alpha -= Time.deltaTime * fadeSpeed;
            if(alpha < 0)
            {
                alpha = 0;
            }
        }
        if(alpha == 0)
        {
            Destroy(gameObject);
        }
        gameObject.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, alpha);
    }
}
