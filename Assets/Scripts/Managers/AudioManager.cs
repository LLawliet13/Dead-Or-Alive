using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
    }
    public static AudioManager instance;
    //public Sound[] musicSound;
    public AudioSource musicSources;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(transform.gameObject);

        }
        else
        {
            instance = this;
            DontDestroyOnLoad(transform.gameObject);
        }
    }

  
    public void ToggleMusic()
    {
        musicSources.mute = !musicSources.mute;
    }
    public void MusicVolume(float volume)
    {
        musicSources.volume = volume;
    }
}
