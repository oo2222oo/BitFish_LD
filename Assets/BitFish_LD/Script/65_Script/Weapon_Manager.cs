using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Manager : MonoBehaviour
{
    public List<Collider2D> attackRange;
    public int attackTime;
    public List<float> damage, knockBack, attackForce;
    public List<Vector2> knockDir;
    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}