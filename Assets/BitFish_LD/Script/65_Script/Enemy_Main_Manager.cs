using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Main_Manager : MonoBehaviour
{
    private Rigidbody2D rb;
    public float maxhp,nowhp;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //µ–»À ‹…À
    public void GetHit(float dam, float force, Vector2 dir, Vector2 hitpos)
    {
        nowhp -= dam;
        rb.AddForceAtPosition(dir * force, hitpos);
        if (nowhp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
