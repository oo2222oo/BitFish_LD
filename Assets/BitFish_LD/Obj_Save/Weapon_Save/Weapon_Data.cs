using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapon/NewWeapon")]
public class Weapon_Data : Item_Get_Data
{
    public Sprite Weapon_Icon;
    public float Weapon_Damge;
    public float Weapon_Bullet_Max;
    public float Weapon_KnockBack;
}
