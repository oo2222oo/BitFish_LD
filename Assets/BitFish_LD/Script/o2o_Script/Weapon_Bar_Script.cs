using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Bar_Script : MonoBehaviour
{
    public static Weapon_Bar_Script Static;
    public Weapon_Data weapon_Data;

    void Awake()
    {
        Static = gameObject.GetComponent<Weapon_Bar_Script>();
    }
}
