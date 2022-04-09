using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon_Bar_Script : MonoBehaviour
{
    public Item_Get_Data weapon_Data;
    public Image weapon_UI;
    public float bar_xsc;
    void Awake()
    {
        bar_xsc = 1;
    }
    void Update()
    {
        if (gameObject.GetComponent<RectTransform>().localScale.x != bar_xsc)
        {
            scale_Set();
        }
    }

    public void weapon_Get(Item_Get_Data Item_Data)
    {
        UI_Manager.Static.Weapon_Count++;
        weapon_Data = Item_Data;
        weapon_UI.enabled = true;
        weapon_UI.sprite = Item_Data.Item_Sprite;
    }
    public void scale_Set()
    {
        float k =(bar_xsc-gameObject.GetComponent<RectTransform>().localScale.x)*0.04f;
        float v_xsc = gameObject.GetComponent<RectTransform>().localScale.x+k;
        gameObject.GetComponent<RectTransform>().localScale = new Vector2(v_xsc, v_xsc);
    }
}
