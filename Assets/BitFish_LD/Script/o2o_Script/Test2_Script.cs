using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2_Script : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        Event_Invoker.AddCommand(new Test_Script(true));
    }
}
