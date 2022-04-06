using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn_Manager : MonoBehaviour
{
    public List<GameObject> ItemSpawn=new List<GameObject>();
    public float Reset_Time_set = 15;
    public float Reset_Time;
    public GameObject spawnEffect;
    // Start is called before the first frame update
    void Start()
    {
        Reset_Time = Reset_Time_set;
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
                v_Script.Item_Spawn_sc(null, 30);
            }
        }
    }
}
