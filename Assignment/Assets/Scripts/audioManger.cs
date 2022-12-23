using UnityEngine.Audio;
using UnityEngine;
using System;

//audiomanager to use sounds, refrenced from https://www.youtube.com/watch?v=6OT43pvUyfY
public class audioManger : MonoBehaviour
{
    //store the sounds in array
    public Sound[] sounds;

    // Start is called before the first frame update
    void Awake()
    {
        //loop through each sound in the array, and give values to volume, pitch and loop
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    
    }

    //play function
    public void play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
        Debug.Log("Playing sound" + name);
    }

    //pause method
    public void pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Pause();
    }
}
