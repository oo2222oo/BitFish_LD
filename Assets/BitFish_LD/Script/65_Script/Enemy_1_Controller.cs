using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1_Controller : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform turnCheck;
    public LayerMask ground;
    public bool hasGround;
    //    private Animator anim;

    public float speed;
    private float horizontalMove;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        horizontalMove = Mathf.Sign(Random.Range(-1, 1));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        hasGround = Physics2D.OverlapCircle(turnCheck.position, 0.1f, ground);
        if (!hasGround) horizontalMove = -horizontalMove;
        GroundMovement();
    }
    void GroundMovement()
    {
        rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y);
        if (horizontalMove != 0)
        {
            transform.localScale = new Vector3(horizontalMove, 1, 1);
        }

    }
}
