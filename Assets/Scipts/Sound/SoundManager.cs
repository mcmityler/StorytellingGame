using System;
using UnityEngine;
 using System.Collections;
 using System.Collections.Generic;
 using Random = UnityEngine.Random;
/*
Created By: Tyler McMillan
Description: This script deals with audio and playing sounds / bg music
*/
public class SoundManager : MonoBehaviour
{
    public Sound[] sounds; //list of sounds in game
    public static SoundManager instance; //instance of audio manager to make sure there is only one in game
    private bool _soundMuted = false; //Should you hear sounds (for sound toggle)
    

    [SerializeField] AudioSource _aSource;
    //Initialization
    void Awake()
    {
        //check only one instance
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            //destroy any multiple instances
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject); //dont destroy if changing scenes

        /*//add sounds to game
        foreach (Sound s in sounds)
        {
            //s.source = gameObject.AddComponent<AudioSource>();
           // s.source.clip = s.clip;
           // s.source.volume = s.volume;
           // s.source.pitch = s.pitch;
           // s.source.loop = s.loop;
        }*/
    }
    //FindObjectOfType<AudioManager>().Play("AUDIOCLIPNAME");
    public bool PlayAnimal(string name) //called from other scripts to play audio 
    {
        if (_soundMuted == false) //check if the sounds are muted (through sound toggle)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " was not found!");
                return false;
            }
            else if (s.irlSounds.Length == 0)
            {
                Debug.LogWarning("Sound does not have a irl sound!");
                return false;
            }
            // StopAllAudio(); //stop all other sounds before playing this one..
            if (s.irlSounds.Length > 1) //if more then one irl sound randomize them
            {
                int m_randInt = Random.Range(0,s.irlSounds.Length);
                 _aSource.clip = s.irlSounds[m_randInt];
            }
            else //if only one irl sound play that one
            {
                _aSource.clip = s.irlSounds[0];
            }
            _aSource.volume = s.volume;
            _aSource.Play();
        }
        return true;
    }
    public bool PlayPronunciation(string name) //called from other scripts to play audio 
    {
        if (_soundMuted == false) //check if the sounds are muted (through sound toggle)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " doesnt exist on object!");
                return false;
            }
            else if (s.pronunciationSound == null)
            {
                Debug.LogWarning("Sound does not have a pronunciation sound equipped!");
                return false;
            }
            _aSource.volume = 1f;
             _aSource.clip = s.pronunciationSound;
            _aSource.Play();
        }
        return true;
    }
    public void StopAllAudio()
    {
        _aSource.Stop();
        /* foreach (Sound s in sounds)
         {
             if(s.name != "music"){

                 //s.source.Stop();
             }
         }*/
    }


    public void PlayWithPitch(string name, float pitchNum) //called from other scripts to play audio 
    {
        if (_soundMuted == false)//check if the sounds are muted (through sound toggle)
        {


            Sound s = Array.Find(sounds, sound => sound.name == name);

            //s.source.volume = s.volume;
            //s.pitch = pitchNum;
            //s.source.pitch = s.pitch;
            Debug.Log(s.pitch);
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " was not found!");
                return;
            }
            //s.source.Play();
        }
    }
    public void ToggleSound()
    {
        _soundMuted = !_soundMuted;
    }
}
