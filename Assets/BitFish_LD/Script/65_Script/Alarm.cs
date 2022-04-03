using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    //��ʼ��alarm
    public static void AlarmInit(float[] a)
    {
        for (int i = 0; i < a.Length; i++)
            a[i] = 0;
    }
    //����alarm
    public static void AlarmSet(float[] a)
    {
        for (int i = 0; i < a.Length; i++)
        {
            if (a[i] < 0) a[i] = 0;
            if (a[i] > 0)
            {
                a[i] -= Time.deltaTime;
                //ȷ��alarm����һ֡Ϊ������������Ϊ������ʹ��
                if (a[i] <= 0) a[i] = -1;
            }
        }
    }
}
