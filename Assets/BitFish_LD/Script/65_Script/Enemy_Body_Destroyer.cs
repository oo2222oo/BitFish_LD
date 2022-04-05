using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Body_Destroyer : MonoBehaviour
{
    public float timer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 1f)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(gameObject.GetComponent<SpriteRenderer>().color.r, gameObject.GetComponent<SpriteRenderer>().color.g, gameObject.GetComponent<SpriteRenderer>().color.b, timer);
        }
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
