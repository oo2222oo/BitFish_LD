using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 基礎數據信息
/// </summary>

public static class Game_Manager_Script
{
    public static GameObject Player;    //玩家

    public static float Player_HP,Player_HP_Max;    //玩家血量  血量上限
    public static float Bullet_Count, Bullet_Count_Max; //子彈量   子彈量上限
    public static float Hobby_bar, Hobby_bar_Max;   //興趣條   興趣條上限
    public static float Player_Damage;  //玩家伤害乘数，默认为1，最终伤害乘上该数值
    public static float Player_Firerate;  //玩家射速乘数，默认为1，最终射速乘上该数值
    public static float Player_Movespeed;  //玩家移动速度乘数，默认为1，最终移动速度乘上该数值
    public static int Weapon_loc;    //裝備當前位置

}
