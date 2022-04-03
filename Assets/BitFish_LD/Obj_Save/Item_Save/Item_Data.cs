using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// µÀ¾ß
/// </summary>

[CreateAssetMenu(fileName = "NewItem", menuName = "Item/NewItem")]
public class Item_Data : ScriptableObject
{
    public string Item_ID;
    public string Item_Name;
    public Sprite Item_Sprite;
}
