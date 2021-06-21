using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSetting : MonoBehaviour
{
    public AudioSource AudioSource;

    private float musicVolume = 1f;

    private void Update() 
    {
        AudioSource.volume = musicVolume;
    }

    public void UpdateVolume(float volume)
    {
        musicVolume = volume;
    }
}
