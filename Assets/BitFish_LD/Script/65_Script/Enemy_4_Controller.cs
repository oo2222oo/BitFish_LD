using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_4_Controller : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public float speed;
    private float horizontalMove;
    public bool isAttack;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Mathf.Sign(player.transform.position.x - transform.position.x);
    }
    private void FixedUpdate()
    {
        if (!isAttack)
        {
            GroundMovement();
            if (player)
            {
                if (Mathf.Abs(player.transform.position.x - transform.position.x) < 2.3f)
                {
                    isAttack = true;
                    anim.SetTrigger("attacking");
                }
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
    void AttackEnd()
    {
        isAttack = false;
    }
}
