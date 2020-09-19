using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour {
    [SerializeField]public Sound[] sounds;


    private void Awake() {
      

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.Pitch;
        }
    }

    public void Play(string name)
    {
        Sound clip = Array.Find(sounds , sound => sound.name == name);
        if(clip != null)
            clip.source.Play();
        else Debug.Log("NO CLIP FOUND");
    }
}
