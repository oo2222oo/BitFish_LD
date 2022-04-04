using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Range_Manager : MonoBehaviour
{
    public List<Collider2D> attackRange;
    public int attackTime;
    // Start is called before the first frame update
    void Awake()
    {
        attackTime = attackRange.Count;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
