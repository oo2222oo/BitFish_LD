using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_3_Controller : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public Transform turnCheck, turnCheck2;
    public LayerMask ground, enemy;
    public bool hasGround, hasWall;
    private float[] alarm = new float[3];
    //    private Animator anim;

    public float speed, moveTime, stopTime;
    private float horizontalMove;
    // Start is called before the first frame update
    void Start()
    {
        Alarm.AlarmInit(alarm);
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        horizontalMove = 0;
        alarm[0] = stopTime;
    }

    // Update is called once per frame
    void Update()
    {
        Alarm.AlarmSet(alarm);
        if (alarm[0] < 0){
            anim.SetBool("moving", true);
            horizontalMove = Mathf.Sign(Random.Range(-1, 1));
            alarm[0] = moveTime + stopTime;
        }
        if (alarm[0]< stopTime)
        {
            anim.SetBool("moving", false);
            horizontalMove = 0;
        }
    }

    void FixedUpdate()
    {
        hasGround = Physics2D.OverlapCircle(turnCheck.position, 0.1f, ground);
        hasWall = Physics2D.OverlapCircle(turnCheck2.position, 0.1f, ground) || Physics2D.OverlapCircle(turnCheck2.position, 0.1f, enemy);
        if (!hasGround || hasWall) horizontalMove = -horizontalMove;
        if (anim.GetBool("hurting") == false)
        {
            GroundMovement();
        }
        else
        {
            alarm[0] = stopTime;
            GameObject player = GameObject.FindWithTag("Player");
            if (player)
            {
                horizontalMove = 0;
                transform.localScale = new Vector3(Mathf.Sign(player.transform.position.x - transform.position.x), 1, 1);
            }
        }

    }
    void GroundMovement()
    {
        rb.AddForce(new Vector2(horizontalMove, 0) * speed);
        if (horizontalMove != 0)
        {
            transform.localScale = new Vector3(horizontalMove, 1, 1);
        }

    }
}
