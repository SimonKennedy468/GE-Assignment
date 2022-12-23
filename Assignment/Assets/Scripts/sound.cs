//custom sound class, taken from https://www.youtube.com/watch?v=6OT43pvUyfY
//sounds from:
//https://pixabay.com/sound-effects/search/rocket/ - LaserRocket2
//https://www.youtube.com/watch?v=ztz7n6jXmlQ
//https://www.youtube.com/watch?v=Msx3v-_WDss

using UnityEngine.Audio;
using UnityEngine;


[System.Serializable]
public class Sound
{
    // Start is called before the first frame update
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(0f, 1f)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
