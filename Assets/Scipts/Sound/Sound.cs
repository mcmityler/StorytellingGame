using UnityEngine.Audio;
using UnityEngine;

/*
Created By: Tyler McMillan
Description: This class holds the information about each sound
*/

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip[] irlSounds;

    public AudioClip pronunciationSound;

    [Range(0f,1f)]
    public float volume = 1;
    [Range(0.1f,3f)]
    public float pitch = 1;

    public bool loop;

    //[HideInInspector]
    //public AudioSource source;
}
