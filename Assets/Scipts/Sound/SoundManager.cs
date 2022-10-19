using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using TMPro;
/*
Created By: Tyler McMillan
Description: This script deals with audio and playing sounds / bg music
*/
public class SoundManager : MonoBehaviour
{
    [SerializeField] private Sound[] _gameSounds; //list of sounds for game and ui
    [SerializeField] private Sound[] _animalSounds; //list of sounds for animals and pronunciation
    
    
    public static SoundManager instance; //instance of audio manager to make sure there is only one in game
    private bool _soundMuted = false; //Should you hear sounds (for sound toggle)


    [SerializeField] private AudioSource _musicSource; //Audio Source that plays the background music
    private bool _bgMusicMuted = true; //Boolean that tells if it is muted or not
    [SerializeField] private Toggle _musicToggle; //toggle for music from settings

    [SerializeField] private AudioSource _aSource; //audio source for pronunciation and animal noises
    [SerializeField] private AudioSource _gameSource; //audio source for game and ui sounds



    private float _volumeFloat = 1f; //integer that holds the value of the volume slider which changes animal and button click sounds
    private float _musicFloat = 0.2f;//integer that holds the value of the msuic slider which changes just the background music volume
    private float _masterFloat = 1f; //integer that holds the value of the master volume slider, which changes both the music and animal sound values
    [SerializeField] TMP_Text _masterValueText, _volumeValueText, _musicValueText;
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

        ToggleMusic(_musicToggle); //stat background music 
        /*//add sounds to game
        foreach (Sound s in sounds)
        {
            //s.source = gameObject.AddComponent<AudioSource>();
           // s.source.clip = s.clip;
           // s.source.volume = s.volume;
           // s.source.pitch = s.pitch;
           // s.source.loop = s.loop;
        }*/

        //set original values of slider values beside them on settings screen
        _masterValueText.text = (100 * _masterFloat).ToString("F0");
        _volumeValueText.text = (100 * _volumeFloat).ToString("F0");
        _musicValueText.text = (100 * _musicFloat).ToString("F0");

    }
    //FindObjectOfType<AudioManager>().Play("AUDIOCLIPNAME");
    public bool PlayAnimal(string name) //called from other scripts to play audio 
    {
        // ** DONT NEED TO STOP ALL OTHER AUDIO BECAUSE ITS ONE AUDIO SOURCE, IT WILL STOP ITSELF WHEN A NEW SOUND IS PLAYED
        if (_soundMuted == false) //check if the sounds are muted (through sound toggle)
        {
            Sound s = Array.Find(_animalSounds, sound => sound.name == name);
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
                int m_randInt = Random.Range(0, s.irlSounds.Length);
                _aSource.clip = s.irlSounds[m_randInt];
            }
            else //if only one irl sound play that one
            {
                _aSource.clip = s.irlSounds[0];
            }
            _aSource.volume = s.volume * _volumeFloat * _masterFloat; //do sound calculation to see how loud the normal sounds will be
            _aSource.Play();
        }
        return true;
    }
    public bool PlayPronunciation(string name) //called from other scripts to play audio 
    {
        if (_soundMuted == false) //check if the sounds are muted (through sound toggle)
        {
            Sound s = Array.Find(_animalSounds, sound => sound.name == name);
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
            _aSource.volume = 1f * _volumeFloat * _masterFloat;//do sound calculation to see how loud the pronunciation sounds will be
            _aSource.clip = s.pronunciationSound;
            _aSource.Play();
        }
        return true;
    }
     public bool PlaySound(string name) //called from other scripts to play audio 
    {
        // ** DONT NEED TO STOP ALL OTHER AUDIO BECAUSE ITS ONE AUDIO SOURCE, IT WILL STOP ITSELF WHEN A NEW SOUND IS PLAYED
        if (_soundMuted == false) //check if the sounds are muted (through sound toggle)
        {
            Sound s = Array.Find(_gameSounds, sound => sound.name == name);
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
                int m_randInt = Random.Range(0, s.irlSounds.Length);
                _gameSource.clip = s.irlSounds[m_randInt];
            }
            else //if only one irl sound play that one
            {
                _gameSource.clip = s.irlSounds[0];
            }
            _gameSource.volume = s.volume * _volumeFloat * _masterFloat; //do sound calculation to see how loud the normal sounds will be
            _gameSource.Play();
        }
        return true;
    }
    public void StopAllAudio()
    {
        _aSource.Stop();
        _gameSource.Stop();

    }
    public void ToggleSound()
    {
        _soundMuted = !_soundMuted;
        StopAllAudio();
    }
    public void ToggleMusic(Toggle m_toggle)
    {
        _bgMusicMuted = m_toggle.isOn;
        if (_bgMusicMuted == false) //check if you should play bg music or stop it
        {
            _musicSource.Play();
        }
        else if (_bgMusicMuted == true)
        {
            _musicSource.Stop();
        }
    }
    public string SoundsExists(string _soundName)
    {
        Sound s = Array.Find(_animalSounds, sound => sound.name == _soundName);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + _soundName + " was not found! No reference made yet!");
            return "noreference";
        }
        else if (s.irlSounds.Length == 0 && s.pronunciationSound == null)
        {
            Debug.LogWarning("Sound does not have any sounds linked!");
            return "nosounds";
        }
        else if (s.irlSounds.Length == 0)
        {
            Debug.LogWarning("Sound does not have a irl sound!");
            return "noirl";
        }
        else if (s.pronunciationSound == null)
        {
            Debug.LogWarning("Sound does not have a pronounce sound!");
            return "nopron";
        }
        return "hassounds";
    }


    public void MasterControl(Slider m_masterSlider) //called from setting script to change master volume level
    {
        _masterFloat = m_masterSlider.value;
        _masterValueText.text = (100 * _masterFloat).ToString("F0"); //update slider value
        _musicSource.volume = 1 * _musicFloat * _masterFloat;//do sound calculation to see how loud the music will be
    }
    public void VolumeControl(Slider m_volumeSlider) //called from setting script to change volume level (animal sounds and other sounds excluding music)
    {
        _volumeFloat = m_volumeSlider.value;
        _volumeValueText.text = (100 * _volumeFloat).ToString("F0");//update slider value

    }
    public void MusicControl(Slider m_musicSlider) //called from setting script to change music volume level
    {
        _musicFloat = m_musicSlider.value;
        _musicValueText.text = (100 * _musicFloat).ToString("F0");//update slider value 

        _musicSource.volume = 1 * _musicFloat * _masterFloat;//do sound calculation to see how loud the music will be
    }
}
