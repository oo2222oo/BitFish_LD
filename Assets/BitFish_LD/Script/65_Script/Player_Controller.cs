using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D coll;
//    private Animator anim;

    public float speed, jumpForce;
    private float horizontalMove;
    public Transform groundCheck;
    public LayerMask ground;

    public bool isGround, isJump, isDashing, isHurt, canHurt;

    bool jumpPressed;
    int jumpCount;
    public float[] alarm = new float[5];

    // Start is called before the first frame update
    void Awake()
    {
  //      Game_Manager_Script.Player = gameObject;
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        Alarm.AlarmInit(alarm);
        //anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Alarm.AlarmSet(alarm);
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            jumpPressed = true;
        }
        if (alarm[0] < 0)
        {
            canHurt = true;
        }
    }

    private void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);
        if (!isHurt)
        {
            GroundMovement();
            Jump();
        }
        else
        {
            if (Mathf.Abs(rb.velocity.x)+ Mathf.Abs(rb.velocity.y) < 2f && alarm[1]<=0)
            {
                isHurt = false;
            }
        }
    }

    void GroundMovement()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");//只返回-1，0，1
        rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y);

        if (horizontalMove != 0)
        {
            transform.localScale = new Vector3(horizontalMove, 1, 1);
        }

    }

    void Jump()//跳跃
    {
        if (isGround)
        {
            jumpCount = 2;//可跳跃数量
            isJump = false;
        }
        if (jumpPressed && isGround)
        {
            isJump = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
            jumpPressed = false;
        }
        else if (jumpPressed && jumpCount > 0 && isJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
            jumpPressed = false;
        }
    }
    //玩家受伤
    public void GetHit(float dam, float force, Vector2 dir, Vector2 hitpos)
    {
        rb.AddForceAtPosition(dir * force, hitpos);
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy" && !isHurt && canHurt)
        {
            isHurt = true;
            canHurt = false;
            alarm[0] = 1f;
            alarm[1] = 0.3f;
            Vector2 vec = transform.position - other.collider.transform.position;
            vec.Normalize();
            GetHit(5f, 400f, vec, transform.position);
        }
    }
}
