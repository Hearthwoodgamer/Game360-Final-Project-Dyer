using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

public class settings : MonoBehaviour
{
    public AudioMixer audiomixer;
    public void SetMusicVolume (float volume)
    {
        audiomixer.SetFloat("MusicVolume", Mathf.Log(volume)*20);
    }
    public void SetSFXVolume(float volume)
    {
       audiomixer.SetFloat("SFXVolume", volume);
    }

}
