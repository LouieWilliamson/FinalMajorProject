using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    public AudioMixer Mixer;
    public void ToggleFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
    public void MasterVolumeChange(float volume)
    {
        Mixer.SetFloat("MasterVol", Mathf.Log10(volume) * 20);
    }
    public void MusicVolumeChange(float volume)
    {
        Mixer.SetFloat("MusicVol", Mathf.Log10(volume) * 20);
    }
    public void SFXVolumeChange(float volume)
    {
        Mixer.SetFloat("SFXVol", Mathf.Log10(volume) * 20);
    }
}
