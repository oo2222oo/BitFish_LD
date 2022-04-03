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

    [LabelText("�����б�")]
    public List<Item_Data> Item_List = new List<Item_Data>();

    [LabelText("����1��҂����˔�")]
    public float Item1_Damge_Mul = 0.1f;
    [LabelText("����2������Ѫ��")]
    public float Item2_HP=20f;
    [LabelText("����3������ٳ˔�")]
    public float Item3_Firerate=0.05f;
    [LabelText("����4������ٳ˔�")]
    public float Item4_Movespeed = 0.05f;
    

    void Awake()
    {
        Static = gameObject.GetComponent<Item_Manager>();
    }
}
