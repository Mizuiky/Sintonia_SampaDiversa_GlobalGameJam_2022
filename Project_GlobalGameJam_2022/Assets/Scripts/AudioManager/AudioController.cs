using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.StopPlaying("Intro");
        AudioManager.instance.Play("MusicaOriginaria");


        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
