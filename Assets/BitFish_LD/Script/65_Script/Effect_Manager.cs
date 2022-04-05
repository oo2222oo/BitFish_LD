using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_Manager : MonoBehaviour
{
    public int effecttype;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetInteger("efftype", effecttype);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EffectEnd()
    {
        Destroy(gameObject);
    }
}
