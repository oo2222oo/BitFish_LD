using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Script : Event_interface
{
    bool onekSb;
    public Test_Script(bool onekSb)
    {
        this.onekSb = onekSb;
    }
    public void Event()
    {
        Debug.Log("1ksb? " + onekSb);
    }



}
