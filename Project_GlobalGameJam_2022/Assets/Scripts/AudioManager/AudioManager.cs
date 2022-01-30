using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;

    public static float pitchValue=1f;

    private string songName;
    // Start is called before the first frame update
    void Awake()
    {

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;


        }
        
    }
    void Start()
    {
        

        Play("Intro");
    }

    public void Play(string name)
    {
        songName = name;
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Som: " + name + "não encontrado");
            return;
        }
        s.source.Play();
        //s.source.pitch = 1f;
    }

    public void StopPlaying(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        //s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        //s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

        s.source.Stop();
    }

    public void ChangePitch(string name)
    {
        Debug.Log("pitch");
        Sound s = Array.Find(sounds, item => item.name == name);
        if (s == null)
        {
            Debug.LogWarning("Som: " + name + "não encontrado");
            return;
        }
        s.source.pitch = pitchValue;
    }

    private void Update()
    {
        ChangePitch(songName);
    }


}
