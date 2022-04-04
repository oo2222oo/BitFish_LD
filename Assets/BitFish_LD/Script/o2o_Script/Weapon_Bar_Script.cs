using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon_Bar_Script : MonoBehaviour
{
    public Item_Get_Data weapon_Data;
    public Image weapon_UI;

    public void weapon_Get(Item_Get_Data Item_Data)
    {
        weapon_Data = Item_Data;
        weapon_UI.enabled = true;
        weapon_UI.sprite = Item_Data.Item_Sprite;
    }
}
