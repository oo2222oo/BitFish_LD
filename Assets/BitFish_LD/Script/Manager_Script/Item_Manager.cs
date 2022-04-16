using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

/// <summary>
/// ��ʼ�����ߏ�����ֵ
/// </summary>
public class Item_Manager : MonoBehaviour
{ 
    public static Item_Manager Static;

    [LabelText("�_������")]
    public Weapon_Data Start_Weapon;

    [LabelText("�����б�")]
    public List<Item_Get_Data> Item_List = new List<Item_Get_Data>();

    [LabelText("����1��҂����˔�")]
    public float Item1_Damge_Mul = 0.1f;
    [LabelText("����2������Ѫ��")]
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
