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
    // Start is called before the first frame update
    void Start()
    {
        time = Spawn_Spawn_time;
    }

    // Update is called once per frame
    void Update()
    {
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
        while (v_ok && k<10)
        {
            k++;
            v_ok = false;
            float v_size = Camera.main.orthographicSize / 5;
            v_spawn = new Vector2(Random.Range(-15f, 15f)*v_size, Random.Range(-2f, 10f)*v_size);
            if(v_spawn.x<10 * v_size && v_spawn.x > -10 * v_size) { v_ok = true; }
            if (Physics2D.OverlapCircle(v_spawn, 0.1f,ground)) { v_ok = true; }
      
        }
    }
}
