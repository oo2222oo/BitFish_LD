using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hobby_Bar_Manager : MonoBehaviour
{
    private Image bar;
    // Start is called before the first frame update
    void Start()
    {
        bar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        bar.fillAmount = Game_Manager_Script.Hobby_bar / Game_Manager_Script.Hobby_bar_Max;
    }
}
