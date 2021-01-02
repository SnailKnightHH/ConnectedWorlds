using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    public AudioMixer mainMixer;

    public void SetMasterVolume(float volume)
    {
        mainMixer.SetFloat("Volume", volume); 
    }

    public void SetSFXVolume(float volume)
    {
        mainMixer.SetFloat("SFX", volume);
    }

    public void SetBGMVolume(float volume)
    {
        mainMixer.SetFloat("BGM", volume);
    }

    public void SetFullScreen (bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}
