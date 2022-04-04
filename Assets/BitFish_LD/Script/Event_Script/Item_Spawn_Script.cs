using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 道具刷新點
/// </summary>
public class Item_Spawn_Script : MonoBehaviour
{
    public Item_Data Item_Data; //道具數據
    public float Destroy_time;  //消失時間
    public float Distance;  //距離
    public GameObject Show_UI;  //顯示位置的UI
    public GameObject Item_obj; //道具
    

    public void Start()
    {
        gameObject.SetActive(false);

    }
    public void Update()
    {
        
        if (Item_Data!=null)
        {

            Vector2 targetDir = (Game_Manager_Script.Player.transform.position - transform.position).normalized; // 目标坐标与当前坐标差的向量
            Vector2 targetSetDis = targetDir*1f;
            float angle = -Mathf.Atan2(targetDir.x, targetDir.y) * Mathf.Rad2Deg+90;
            Show_UI.transform.position = new Vector2(transform.position.x + targetSetDis.x, transform.position.y + targetSetDis.y);
            Show_UI.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            float v_size = Camera.main.orthographicSize/5;
            if (Show_UI.transform.position.x < Camera.main.transform.position.x - 6.5f* v_size)
            {
                Show_UI.transform.position = new Vector2(Camera.main.transform.position.x - 6.5f * v_size, Camera.main.transform.position.y);
            }
            if (Show_UI.transform.position.x > Camera.main.transform.position.x + 6.5f * v_size)
            {
                Show_UI.transform.position = new Vector2(Camera.main.transform.position.x + 6.5f * v_size, Camera.main.transform.position.y);
            }
            if (Show_UI.transform.position.y < Camera.main.transform.position.y - 4f * v_size)
            {
                Show_UI.transform.position = new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.y - 4f * v_size);
            }
            if (Show_UI.transform.position.y > Camera.main.transform.position.y + 4f * v_size)
            {
                Show_UI.transform.position = new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.y + 4f * v_size);
            }
            Distance =Vector2.Distance(Game_Manager_Script.Player.transform.position,transform.position);
            //道具消失
            Destroy_time -= Time.deltaTime;
            if (Destroy_time <= 0) { 
                Item_Data = null;
                gameObject.SetActive(false);
            }
        }
    }

    public void Item_Spawn_sc(Item_Data v_item,float Des_time)
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
        Destroy_time = Des_time;
        gameObject.SetActive(true);

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
        Debug.Log(Game_Manager_Script.Player_Damage);
        Item_Data = null;
        gameObject.SetActive(false);
    }

}
