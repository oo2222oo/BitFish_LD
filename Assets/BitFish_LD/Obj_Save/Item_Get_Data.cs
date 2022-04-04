using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Get_Data : ScriptableObject
{
    public Eunm_Type Item_Type;
    public string Item_ID;
    public string Item_Name;
    public Sprite Item_Sprite;
    
    public enum Eunm_Type{
        Item,
        Weapon
    }
}
