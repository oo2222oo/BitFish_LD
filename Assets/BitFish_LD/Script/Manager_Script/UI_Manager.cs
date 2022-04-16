using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager Static;
    public GameObject HP_UI;
    public GameObject Hobby_UI;

    public List<Weapon_Bar_Script> Weapon_Bar;
    public float maxHobby;
    public int Weapon_Count;
    private float timeCount;

    void Awake()
    {
        Static = gameObject.GetComponent<UI_Manager>();
        Game_Manager_Script.Hobby_bar_Max = maxHobby;
        Game_Manager_Script.Hobby_bar = maxHobby;
        timeCount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Game_Manager_Script.Hobby_bar -= Time.deltaTime * timeCount;
        timeCount += Time.deltaTime * 0.001f;
    }
}
