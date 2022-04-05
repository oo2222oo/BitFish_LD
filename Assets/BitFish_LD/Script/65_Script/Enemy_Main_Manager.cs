using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Main_Manager : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public float maxhp;
    private float nowhp;
    public GameObject body;
    public float damage,knockback;
    private float[] alarm = new float[3];
    private bool isHurt;
    // Start is called before the first frame update
    void Start()
    {
        Alarm.AlarmInit(alarm);
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Alarm.AlarmSet(alarm);
        if (alarm[0] < 0)
        {
            anim.SetBool("hurting", false);
        }
    }

    //µÐÈËÊÜÉË
    public void GetHit(float dam, float force, Vector2 dir)
    {
        alarm[0] = 0.5f;
        anim.SetBool("hurting", true);
        nowhp -= dam;
        rb.AddForce(dir * force);
        if (nowhp <= 0)
        {
            GameObject bd = Instantiate(body, transform.position, Quaternion.identity);
            bd.transform.localScale = transform.localScale;
            Destroy(gameObject);
        }
    }
}
