using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Enemy_Spawn_Manager : MonoBehaviour
{
    [LabelText("怪物生成時間")]
    public float Spawn_Spawn_time = 1;
    [LabelText("怪物生成倒計時")]
    public float time;
    public LayerMask ground;
    public float Upgrade_Time;
    public List<GameObject> enemy;
    private int enemyType = 1;
    private float[] alarm = new float[1];
    // Start is called before the first frame update
    void Start()
    {
        time = Spawn_Spawn_time;
        Alarm.AlarmInit(alarm);
        alarm[0] = Upgrade_Time;
    }

    // Update is called once per frame
    void Update()
    {
        Alarm.AlarmSet(alarm);
        if (alarm[0] < 0)
        {
            alarm[0] = Upgrade_Time;
            if (enemyType < 4)
            {
                enemyType++;
                Spawn_Spawn_time -= 1f;
            }
        }
        time -= Time.deltaTime;
        if (time <= 0) {
            time=Spawn_Spawn_time;
            enemy_Spawn();
        }
    }

    public void enemy_Spawn()
    {
        Vector2 v_spawn=new Vector2();
        bool v_ok=true;
        int k = 0;
        while (v_ok && k<20)
        {
            k++;
            v_ok = false;
            float v_size = Camera.main.orthographicSize / 5;
            v_spawn = new Vector2(Random.Range(-15f, 15f)*v_size, Random.Range(-2f, 10f)*v_size);
            if(v_spawn.x<10 * v_size && v_spawn.x > -10 * v_size) { v_ok = true; }
            if (Physics2D.OverlapCircle(v_spawn, 0.1f,ground)) { v_ok = true; }
      
        }
        if (!v_ok)
        {
            int type = Random.Range(1, enemyType + 1);
            Instantiate(enemy[type - 1], v_spawn, Quaternion.Euler(new Vector3(0, 0, 0)));
        }
    }
}
