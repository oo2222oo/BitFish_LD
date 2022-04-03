using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    //初始化alarm
    public static void AlarmInit(float[] a)
    {
        for (int i = 0; i < a.Length; i++)
            a[i] = 0;
    }
    //单步alarm
    public static void AlarmSet(float[] a)
    {
        for (int i = 0; i < a.Length; i++)
        {
            if (a[i] < 0) a[i] = 0;
            if (a[i] > 0)
            {
                a[i] -= Time.deltaTime;
                //确保alarm会有一帧为负数，可以作为触发器使用
                if (a[i] <= 0) a[i] = -1;
            }
        }
    }
}
