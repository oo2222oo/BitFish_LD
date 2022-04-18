using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D coll;
    private Animator anim;
    private Cinemachine.CinemachineImpulseSource cis;

    public float maxHealth;
    public float speed, jumpForce, level;
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
        Game_Manager_Script.Player_HP_Max = maxHealth;
        Game_Manager_Script.Player_HP = maxHealth;
        Game_Manager_Script.Player_Damage = 1;
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
        var weaponSwitch = -Mathf.FloorToInt(Input.GetAxisRaw("Switch"));
        if ((Input.GetButtonDown("Switch") || Input.GetAxisRaw("Mouse ScrollWheel") != 0) && !isHurt && !isAttack)
        {
            Weapon_Change(weaponSwitch);
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
            if (Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.y) < 2f && alarm[1] <= 0)
            {

                if (Game_Manager_Script.Player_HP <= 0)
                {
                    alarm[0] = 0;
                    alarm[1] = 0;
                    anim.SetBool("dying", true);
                    rb.mass *= 10;
                }
                else
                {
                    anim.SetBool("hurting", false);
                    isHurt = false;

                }
            }
        }
    }
    void WeaponInit()
    {
        if (weaponObj == null) { return; }
        Weapon_Manager weapon = weaponObj;
        attackRange = weapon.attackRange;
        attackTime = weapon.attackTime;
        damage = weapon.damage;
        level = weapon.level;
        knockBack = weapon.knockBack;
        attackForce = weapon.attackForce;
        knockDir = weapon.knockDir;
        anim.runtimeAnimatorController = weapon.animController;
    }

    void AttackDeal()
    {
        canPersue = true;
        rb.velocity = new Vector2(0, 0);
        rb.AddForce(new Vector2(transform.localScale.x, 0) * attackForce[attackRound - 1]);
        var filter = new ContactFilter2D();
        filter.useTriggers = true;
        Collider2D[] attackCollide = new Collider2D[100];
        int collideNum = attackRange[attackRound - 1].OverlapCollider(filter, attackCollide);
        bool hit = false;
        List<int> hitid = new List<int>();
        for (int i = 0; i < collideNum; i++)
        {
            if (attackCollide[i].tag == "Enemy" || attackCollide[i].tag == "Body")
            {
                bool hasHit = false;
                for (int j = 0; j < hitid.Count; j++)
                {
                    if (attackCollide[i].gameObject.GetInstanceID() == hitid[j])
                    {
                        hasHit = true;
                        break;
                    }
                }
                if (hasHit)
                {
                    break;
                }
                else
                {
                    hitid.Add(attackCollide[i].gameObject.GetInstanceID());
                }
            }
            var hitdir = knockDir[attackRound - 1].normalized * new Vector2(Mathf.Sign(attackCollide[i].transform.position.x - transform.position.x), 1);
            if (attackCollide[i].tag == "Enemy")
            {
                hit = true;
                Enemy_Main_Manager ec = attackCollide[i].GetComponent<Enemy_Main_Manager>();
                ec.GetHit(damage[attackRound - 1] * Game_Manager_Script.Player_Damage * level, knockBack[attackRound - 1], hitdir);
                Debug.Log(damage[attackRound - 1] * Game_Manager_Script.Player_Damage * level);
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
        if (Mathf.Abs(horizontalMove) < 0.3f)
        {
            horizontalMove = 0f;
        }
        else
        {
            horizontalMove = Mathf.Sign(horizontalMove);
        }
        /*
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
        {*/
        rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y);
        //}
        
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
        isHurt = true;
        canHurt = false;
        anim.SetBool("hurting", true);
        AttackStop();
        alarm[0] = 0.5f;
        alarm[1] = 0.3f;
        SoundPlay("playerHit");
        rb.AddForceAtPosition(dir * force, hitpos);
        Game_Manager_Script.Player_HP -= dam;
        Game_Manager_Script.Hobby_bar -= 2;
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy" && !isHurt && canHurt)
        {
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
            v_Scrpit.itemGet_sc(v_Scrpit.Item_Data);
        }
    }

    public void Weapon_Change(int dir)    //«–ìQŒ‰∆˜ïr’{”√ﬂ@ÇÄ
    {

        var nextWeapon = (Game_Manager_Script.Weapon_loc + dir + UI_Manager.Static.Weapon_Count) % UI_Manager.Static.Weapon_Count;
        if (UI_Manager.Static.Weapon_Bar[nextWeapon].weapon_Data != null)
        {
            UI_Manager.Static.Weapon_Bar[Game_Manager_Script.Weapon_loc].bar_xsc = 1;
            Game_Manager_Script.Weapon_loc = nextWeapon;
            Weapon_Bar_Script v_weapon_bar = UI_Manager.Static.Weapon_Bar[Game_Manager_Script.Weapon_loc];
            Weapon_Data v_weapon_data = (Weapon_Data)v_weapon_bar.weapon_Data;
            v_weapon_bar.bar_xsc = 1.2f;
            if (weaponObj != null) { Destroy(weaponObj.gameObject); }
            
            weaponObj = v_weapon_data.Weapon_manager;

            weaponObj=Instantiate(v_weapon_data.Weapon_manager.gameObject,transform).GetComponent<Weapon_Manager>();
            WeaponInit();
        }
    }
}
