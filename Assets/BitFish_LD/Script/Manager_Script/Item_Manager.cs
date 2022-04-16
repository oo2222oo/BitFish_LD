using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

/// <summary>
/// 初始化道具強化數值
/// </summary>
public class Item_Manager : MonoBehaviour
{ 
    public static Item_Manager Static;

    [LabelText("開局武器")]
    public Weapon_Data Start_Weapon;

    [LabelText("道具列表")]
    public List<Item_Get_Data> Item_List = new List<Item_Get_Data>();

    [LabelText("道具1玩家傷害乘數")]
    public float Item1_Damge_Mul = 0.1f;
    [LabelText("道具2提升的血量")]
    public float Item2_HP=20f;
    
    
    void Awake()
    {
        Static = gameObject.GetComponent<Item_Manager>();

        
    }

    void Start()
    {
        UI_Manager.Static.Weapon_Bar[0].weapon_Get(Start_Weapon);
        Game_Manager_Script.Player.GetComponent<Player_Controller>().Weapon_Change(0);
    }
}
