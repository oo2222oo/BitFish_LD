using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapon/NewWeapon")]
public class Weapon_Data : ScriptableObject
{
    public string Weapon_ID;
    public string Weapon_Name;
    public Sprite Weapon_Icon;
    public float Weapon_Damge;
    public float Weapon_Bullet_Max;
    public float Weapon_KnockBack;
}
