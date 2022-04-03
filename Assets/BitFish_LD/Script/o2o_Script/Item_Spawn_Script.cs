using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 道具刷新點
/// </summary>
public class Item_Spawn_Script : MonoBehaviour
{
    public Item_Data Item_Data; //道具數據
    public bool is_spawn;   //是否刷新
    public float Destroy_time;  //消失時間


    public void Update()
    {
        
        if (is_spawn)
        {

            //道具消失
            Destroy_time -= Time.deltaTime;
            if (Destroy_time <= 0) { 
                is_spawn = !is_spawn;
                Item_Data = null;
            }
        }
    }

    public void Item_Spawn_sc(Item_Data v_item)
    {
        if (v_item == null) { v_item = Item_Manager.Static.Item_List[Random.Range(0, Item_Manager.Static.Item_List.Count)]; }
        bool v_ok=false;
        while (v_ok)
        {
            v_ok = true;
            if (v_item.Item_ID == "Item3")
            {
                if(Game_Manager_Script.Player_Firerate >= 0.5f) { v_ok = false; }
            }
            if (v_item.Item_ID == "Item4")
            {
                if (Game_Manager_Script.Player_Movespeed >= 0.5f) { v_ok = false; }
            }
            if (!v_ok)
            {
                v_item = Item_Manager.Static.Item_List[Random.Range(0, Item_Manager.Static.Item_List.Count)];
            }
        }
        Item_Data = v_item;

    }

    public void itemGet_sc()
    {
        if (Item_Data == null) { return; }
        if (Item_Data.Item_ID == "Item1")
        {
            Game_Manager_Script.Player_Damage += Item_Manager.Static.Item1_Damge_Mul;
        }
        if (Item_Data.Item_ID == "Item2")
        {
            Game_Manager_Script.Player_HP_Max += Item_Manager.Static.Item2_HP;
            Game_Manager_Script.Player_HP += Item_Manager.Static.Item2_HP;
        }
        if (Item_Data.Item_ID == "Item3")
        {
            Game_Manager_Script.Player_Firerate += Item_Manager.Static.Item3_Firerate;
            if (Game_Manager_Script.Player_Firerate > 0.5f) { Game_Manager_Script.Player_Firerate = 0.5f; }
        }
        if (Item_Data.Item_ID == "Item4")
        {
            Game_Manager_Script.Player_Movespeed += Item_Manager.Static.Item4_Movespeed;
            if (Game_Manager_Script.Player_Movespeed > 0.5f){ Game_Manager_Script.Player_Movespeed = 0.5f; }
        }
    }

}
