using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1_Controller : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public Transform turnCheck, turnCheck2;
    public LayerMask ground;
    public bool hasGround, hasWall;
    //    private Animator anim;

    public float speed;
    private float horizontalMove;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        horizontalMove = Mathf.Sign(Random.Range(-1, 1));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        hasGround = Physics2D.OverlapCircle(turnCheck.position, 0.1f, ground);
        hasWall = Physics2D.OverlapCircle(turnCheck2.position, 0.1f, ground);
        if (!hasGround || hasWall) horizontalMove = -horizontalMove;
        if (anim.GetBool("hurting") == false)
        {
            GroundMovement();
        }
        else
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player)
            {
                horizontalMove = Mathf.Sign(player.transform.position.x - transform.position.x);
                transform.localScale = new Vector3(horizontalMove, 1, 1);
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
