using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D coll;
    private Animator anim;

    public float speed, jumpForce;
    public List<float> damage, knockBack, attackForce;
    private float horizontalMove,dashMove;
    private int attackTime,attackRound;
    private List<Collider2D> attackRange;
    private List<Vector2> knockDir;
    public Transform groundCheck;
    public LayerMask ground;

    public bool isGround, isJump, isDashing, isHurt, canHurt, isAttack, canPersue;
    public Weapon_Manager weaponObj;
    public bool tempUpdate;

    bool jumpPressed;
    int jumpCount;
    float direction;
    private float[] alarm = new float[5];

    // Start is called before the first frame update
    void Awake()
    {
        Game_Manager_Script.Player = gameObject;
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        Alarm.AlarmInit(alarm);
        anim = GetComponent<Animator>();
        WeaponInit();
    }

    // Update is called once per frame
    void Update()
    {
        Alarm.AlarmSet(alarm);
        anim.SetFloat("yspeed", rb.velocity.y);
        if (tempUpdate)
        {
            tempUpdate = false;
            WeaponInit();           
        }
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            jumpPressed = true;
        }
        if (alarm[0] < 0)
        {
            canHurt = true;
        }
        if (Input.GetButtonDown("Attack") && !isHurt)
        {
            if (!isAttack)
            {
                isAttack = true;
                attackRound += 1;
                anim.SetInteger("attackround", attackRound);
                anim.SetTrigger("attacking");
            }
            else if(canPersue && attackRound<attackTime)
            {
                attackRound += 1;
                anim.SetInteger("attackround", attackRound);
                canPersue = false;
            }
        }
        if (horizontalMove != 0 && !isHurt && !isAttack)
        {
            alarm[2] = 0f;
            anim.SetBool("moving", true);
            transform.localScale = new Vector3(horizontalMove, 1, 1);
        }
        else
        {
            if (alarm[2] == 0f)
            {
                alarm[2] = 0.1f;
            }
            if (alarm[2] < 0)
            {
                anim.SetBool("moving", false);
            }
        }
    }

    private void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);
        if (!isHurt && !isAttack)
        {
            GroundMovement();
            Jump();
        }
        else
        {
            if (Mathf.Abs(rb.velocity.x)+ Mathf.Abs(rb.velocity.y) < 2f && alarm[1]<=0)
            {
                anim.SetBool("hurting", false);
                isHurt = false;
            }
        }
    }
    void WeaponInit()
    {
        Weapon_Manager weapon = weaponObj;
        attackRange = weapon.attackRange;
        attackTime = weapon.attackTime;
        damage = weapon.damage;
        knockBack = weapon.knockBack;
        attackForce = weapon.attackForce;
        knockDir = weapon.knockDir;
    }

    void AttackDeal()
    {
        canPersue = true;
        rb.velocity = new Vector2(0, 0);
        rb.AddForce(new Vector2(transform.localScale.x, 0) * attackForce[attackRound - 1]);
        var filter = new ContactFilter2D();
        filter.useTriggers = true;
        Collider2D[] attackCollide = new Collider2D[20];
        int collideNum = attackRange[attackRound - 1].OverlapCollider(filter, attackCollide);
        for(int i = 0; i < collideNum; i++)
        {
            if (attackCollide[i].tag == "Enemy")
            {
                Enemy_Main_Manager ec = attackCollide[i].GetComponent<Enemy_Main_Manager>();
                ec.GetHit(damage[attackRound - 1], knockBack[attackRound - 1], knockDir[attackRound - 1].normalized * new Vector2(Mathf.Sign(attackCollide[i].transform.position.x - transform.position.x), 1));
            }
        }
    }
    void AttackStopIfNotPersue()
    {
        if (canPersue)
        {
            AttackStop();
        }
    }
    void AttackStop()
    {
        isAttack = false;
        canPersue = false;
        attackRound = 0;
        anim.SetInteger("attackround", 0);
    }
    void GroundMovement()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        if (Mathf.Abs(horizontalMove) < 0.1f)
        {
            horizontalMove = 0f;
        }
        else
        {
            horizontalMove = Mathf.Sign(horizontalMove);
        }
        dashMove = Input.GetAxisRaw("Dash");
        if (dashMove == 1 && alarm[3]<=0)
        {
            alarm[3] = 1f;
        }
        if (alarm[3] > 0.9f)
        {
            rb.velocity = new Vector2(horizontalMove * speed * 3, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y);
        }

        if (horizontalMove != 0f)
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
            anim.SetTrigger("jumping");
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
            anim.SetBool("hurting", true);
            AttackStop();
            alarm[0] = 1f;
            alarm[1] = 0.3f;
            Vector2 vec = transform.position - other.collider.transform.position;
            vec.Normalize();
            Enemy_Main_Manager ec = other.collider.GetComponent<Enemy_Main_Manager>();
            GetHit(ec.damage, ec.knockback, vec, transform.position);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Item") {
            Item_Spawn_Script v_Scrpit = other.gameObject.transform.parent.GetComponent<Item_Spawn_Script>();
            v_Scrpit.itemGet_sc();
        }
    }

    public void Weapon_Change()    //切換武器時調用這個
    {
        Weapon_Data v_weapon_data = (Weapon_Data)UI_Manager.Static.Weapon_Bar[Game_Manager_Script.Weapon_loc].weapon_Data;
        weaponObj = v_weapon_data.Weapon_manager;

        WeaponInit();
    }
}
