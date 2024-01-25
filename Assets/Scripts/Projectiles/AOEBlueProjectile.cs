using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEBlueProjectile : MonoBehaviour
{
    private Color color;
    private float alpha;
    public float fadeSpeed;

    // Start is called before the first frame update
    void Start()
    {
        color = gameObject.GetComponent<SpriteRenderer>().color;
        alpha = color.a;
    }

    // Update is called once per frame
    void Update()
    {

        if(alpha > 0)
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
