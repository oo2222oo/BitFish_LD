using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Main_Manager : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public float maxhp;
    private float nowhp;
    public GameObject body,deathEffect;
    public float damage,knockback;
    public string deathSound;
    private float[] alarm = new float[3];
    private bool isHurt;
    // Start is called before the first frame update
    void Start()
    {
        Alarm.AlarmInit(alarm);
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        nowhp = maxhp;
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
    void SoundPlay(string snd)
    {
        Sound_Manager.Sound.Play(snd);
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
            var rbd = bd.GetComponent<Rigidbody2D>();
            rbd.AddForce(dir * force);
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            SoundPlay(deathSound);
            Game_Manager_Script.Player_HP += maxhp / 40;
            if (Game_Manager_Script.Player_HP > Game_Manager_Script.Player_HP_Max) Game_Manager_Script.Player_HP = Game_Manager_Script.Player_HP_Max;
            Game_Manager_Script.Hobby_bar += maxhp / 40;
            Destroy(gameObject);
        }
    }
}
