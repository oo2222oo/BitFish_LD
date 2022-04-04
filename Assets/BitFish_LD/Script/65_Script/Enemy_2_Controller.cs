using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2_Controller : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private float[] alarm = new float[3];
    private GameObject target;
    private float xscale;
    public float speed;
    public float attackcd;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        Alarm.AlarmInit(alarm);
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        alarm[0] = attackcd;
    }

    // Update is called once per frame
    void Update()
    {
        Alarm.AlarmSet(alarm);
        if (anim.GetBool("hurting") == false && target)
        {
            if (alarm[0] < 0)
            {
                alarm[0] = attackcd;
                anim.SetBool("attacking", true);
                rb.AddForce(new Vector2(target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y).normalized * speed);
            }
            if (rb.velocity.magnitude < 1f && alarm[0]<attackcd-0.1f)
            {
                anim.SetBool("attacking", false);
                xscale = Mathf.Sign(target.transform.position.x - transform.position.x);

            }
        }
        else
        {
            alarm[0] = attackcd;
        }
        if (xscale != 0)
        {
            transform.localScale = new Vector3(xscale, 1, 1);
        }
    }
}
