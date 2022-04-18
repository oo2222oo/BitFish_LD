using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn_Manager : MonoBehaviour
{
    public List<GameObject> ItemSpawn = new List<GameObject>();
    public float Reset_Time_set = 15;
    public float Reset_Time;
    public float Weapon_Time_set = 10;
    public float Weapon_Time;
    public GameObject spawnEffect;
    // Start is called before the first frame update
    void Start()
    {
        Reset_Time = Reset_Time_set;
        Weapon_Time = Weapon_Time_set;
    }

    // Update is called once per frame
    void Update()
    {
        if (Reset_Time_set != -1)
        {
            Reset_Time -= Time.deltaTime;
            if (Reset_Time <= 0)
            {
                var index = Random.Range(0, ItemSpawn.Count - 1);
                Instantiate(spawnEffect, ItemSpawn[index].transform.position, Quaternion.identity);
                Reset_Time = Reset_Time_set;
                Item_Spawn_Script v_Script = ItemSpawn[index].GetComponent<Item_Spawn_Script>();
                if (Weapon_Time < 0)
                {
                    v_Script.canWeapon = true;
                    Weapon_Time = Weapon_Time_set;
                }
                else
                {
                    v_Script.canWeapon = false;
                }
                v_Script.Item_Spawn_sc(null, 30);
            }
        }

        if (Weapon_Time_set != -1)
        {
            Weapon_Time -= Time.deltaTime;
        }
    }
}
