using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager Static;
    public GameObject HP_UI;
    public GameObject Bullet_UI;
    public GameObject Hobby_UI;

    public List<Weapon_Bar_Script> Weapon_Bar;

    void Start()
    {
        Static = gameObject.GetComponent<UI_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
