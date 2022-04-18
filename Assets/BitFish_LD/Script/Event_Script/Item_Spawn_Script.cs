using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 道具刷新點
/// </summary>
public class Item_Spawn_Script : MonoBehaviour
{
    public Item_Get_Data Item_Data; //道具數據
    public float Destroy_time;  //消失時間
    public float Distance;  //距離
    public GameObject Show_UI;  //顯示位置的UI
    public GameObject Item_obj; //道具
    public bool canWeapon; //是不是武器


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
            if (Destroy_time <= 0 && Item_Data.Item_Type == Item_Get_Data.Eunm_Type.Item) { 
                Item_Data = null;
                gameObject.SetActive(false);
            }
        }
    }

    public void Item_Spawn_sc(Item_Get_Data v_item,float Des_time)
    {
        if (v_item == null) { v_item = Item_Manager.Static.Item_List[Random.Range(0, Item_Manager.Static.Item_List.Count-1)]; }
        bool v_ok=false;
        while (!v_ok)
        {
            v_ok = true;
            v_item = Item_Manager.Static.Item_List[Random.Range(0, Item_Manager.Static.Item_List.Count-1)];
            if (v_item.Item_Type == Item_Get_Data.Eunm_Type.Weapon && !canWeapon) v_ok = false;
            if (v_item.Item_Type == Item_Get_Data.Eunm_Type.Item && canWeapon) v_ok = false;
        }
        Item_Data = v_item;
        Destroy_time = Des_time;
        gameObject.SetActive(true);

    }

    public void itemGet_sc(Item_Get_Data Item_Data)
    {
        canWeapon = false;
        Game_Manager_Script.Hobby_bar += 10;
        if (Game_Manager_Script.Hobby_bar > Game_Manager_Script.Hobby_bar_Max) Game_Manager_Script.Hobby_bar = Game_Manager_Script.Hobby_bar_Max;
        if (Item_Data == null) { return; }
        if (Item_Data.Item_Type == Item_Get_Data.Eunm_Type.Item)
        {
            if (Item_Data.Item_ID == "Item1")
            {

                Game_Manager_Script.Player_Damage += Item_Manager.Static.Item1_Damge_Mul;
                Debug.Log("Damage+");
            }
            if (Item_Data.Item_ID == "Item2")
            {
                Game_Manager_Script.Player_HP_Max += Item_Manager.Static.Item2_HP;
                Game_Manager_Script.Player_HP += Item_Manager.Static.Item2_HP;
                Debug.Log("HP+");
            }
            /*
            for (int i = 0; i < UI_Manager.Static.Weapon_Bar.Count; i++)
            {
                if (UI_Manager.Static.Weapon_Bar[i].GetComponent<Weapon_Bar_Script>().weapon_Data == null)
                {
                    UI_Manager.Static.Weapon_Bar[i].weapon_Get(Item_Data);
  
                    break;
                }
            }
            */
        }

        if (Item_Data.Item_Type == Item_Get_Data.Eunm_Type.Weapon)
        {
            for (int i = 0; i < UI_Manager.Static.Weapon_Bar.Count; i++)
            {
                if(UI_Manager.Static.Weapon_Bar[i].GetComponent<Weapon_Bar_Script>().weapon_Data == Item_Data)
                {
                    Weapon_Data wd = (Weapon_Data)UI_Manager.Static.Weapon_Bar[i].GetComponent<Weapon_Bar_Script>().weapon_Data;
                    Debug.Log(wd.Item_ID);
                    wd.Weapon_manager.level += 0.2f;
                    break;
                }
                if (UI_Manager.Static.Weapon_Bar[i].GetComponent<Weapon_Bar_Script>().weapon_Data == null)
                {
                    Weapon_Data wd = (Weapon_Data)Item_Data;
                    wd.Weapon_manager.level = 1f;
                    UI_Manager.Static.Weapon_Bar[i].weapon_Get(Item_Data);
                    break;
                }
            }
        }
        //Debug.Log("撿到了");
        this.Item_Data = null;
        gameObject.SetActive(false);
    }

}
