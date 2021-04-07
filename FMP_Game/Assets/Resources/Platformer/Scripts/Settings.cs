using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    public AudioMixer Mixer;

    public AudioMixerSnapshot paused; //https://youtu.be/7wWNAiWc8ws
    public AudioMixerSnapshot unpaused;
    public void ToggleFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
        print("Fullscreen: " + Screen.fullScreen);
    }
    public void MasterVolumeChange(float volume)
    {
        Mixer.SetFloat("MasterVol", volume);
    }
    public void MusicVolumeChange(float volume)
    {
        Mixer.SetFloat("MusicVol", volume);
    }
    public void SFXVolumeChange(float volume)
    {
        Mixer.SetFloat("SFXVol", volume);
    }
}
