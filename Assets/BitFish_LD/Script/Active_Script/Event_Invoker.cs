using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Invoker : MonoBehaviour
{
    static Queue<Event_interface> commandBuffer;


    void Awake()
    {
        commandBuffer = new Queue<Event_interface>();
    }

    // Start is called before the first frame update
    public static void AddCommand(Event_interface command)
    {
        commandBuffer.Enqueue(command);
    }


    // Update is called once per frame
    void Update()
    {
    
       while (commandBuffer.Count > 0)
        {
            Event_interface c = commandBuffer.Dequeue();
            c.Event();

        }

    }
}
