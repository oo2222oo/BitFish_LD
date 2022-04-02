using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hobby_Change_Script : Event_interface
{
    float Hobby_add;    //增加或減少興趣度
    public Hobby_Change_Script(float Hobby_add)
    {
        this.Hobby_add = Hobby_add;
    }

    // Update is called once per frame
    public void Event()
    {
        Game_Manager_Script.Hobby_bar += Hobby_add;
    }

}
