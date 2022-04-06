using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Manager : MonoBehaviour
{
    public static Sound_Manager Sound;
    private AudioSource src;
    // Start is called before the first frame update
    void Start()
    {
        Sound = this;
        src = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Play(string name)
    {
        AudioClip clip = Resources.Load<AudioClip>("Sound/" + name);
        src.PlayOneShot(clip);
    }
}
