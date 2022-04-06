using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D coll;
    private Animator anim;
    private Cinemachine.CinemachineImpulseSource cis;

    public float speed, jumpForce;
    public int jumpTime;
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
    private int jumpCount;

    bool jumpPressed;
    float direction;
    private float[] alarm = new float[8];

    // Start is called before the first frame update
    void Awake()
    {
        Game_Manager_Script.Player = gameObject;
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        Alarm.AlarmInit(alarm);
        anim = GetComponent<Animator>();
        cis = GetComponent<Cinemachine.CinemachineImpulseSource>();
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
        if (Input.GetButtonDown("Jump"))
        {
            jumpPressed = true;
            alarm[4] = 0.2f;
        }
        if (alarm[4] < 0f)
        {
            jumpPressed = false;
        }
        if (alarm[0] < 0f)
        {
            canHurt = true;
        }
        if (alarm[5] < 0f)
        {
            anim.speed = 1;
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
        bool hit = false;
        for(int i = 0; i < collideNum; i++)
        {
            var hitdir = knockDir[attackRound - 1].normalized * new Vector2(Mathf.Sign(attackCollide[i].transform.position.x - transform.position.x), 1);
            if (attackCollide[i].tag == "Enemy")
            {
                hit = true;
                Enemy_Main_Manager ec = attackCollide[i].GetComponent<Enemy_Main_Manager>();
                ec.GetHit(damage[attackRound - 1], knockBack[attackRound - 1], hitdir);
            }
            if(attackCollide[i].tag == "Body")
            {
                var pos = attackCollide[i].transform.position;
                pos.x += Random.Range(-0.1f, 0.1f);
                pos.y += Random.Range(-0.1f, 0.1f);
                attackCollide[i].GetComponent<Rigidbody2D>().AddForceAtPosition(knockBack[attackRound - 1] * hitdir, pos);

            }
        }
        if (hit)
        {
            anim.speed = 0;
            alarm[5] = 0.1f;
            SoundPlay("enemyHit");
            cis.GenerateImpulse(new Vector2(1, 1));
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

    void SoundPlay(string snd)
    {
        Sound_Manager.Sound.Play(snd);
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

    void Jump()//Ã¯‘æ
    {
        
        if (isGround)
        {
            isJump = false;
            jumpCount = jumpTime;
        }
        else
        {
            if (!isJump) jumpCount = jumpTime - 1;
        }
        if (jumpPressed && isGround)
        {
            JumpDeal();
        }
        else if (jumpPressed && jumpCount > 0)
        {
            JumpDeal();
        }
            
    }
    void JumpDeal()
    {
            SoundPlay("jumpSound");
            isJump = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
            jumpPressed = false;
    }
    //ÕÊº“ ‹…À
    public void GetHit(float dam, float force, Vector2 dir, Vector2 hitpos)
    {
        SoundPlay("playerHit");
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

    public void Weapon_Change()    //«–ìQŒ‰∆˜ïr’{”√ﬂ@ÇÄ
    {
        Weapon_Data v_weapon_data = (Weapon_Data)UI_Manager.Static.Weapon_Bar[Game_Manager_Script.Weapon_loc].weapon_Data;
        weaponObj = v_weapon_data.Weapon_manager;

        WeaponInit();
    }
}
