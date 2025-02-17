﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public enum SFX 
    { 
        Shoot, PickupGun, HitEnemy, HitWall, Jump, 
        HitPlayer, PlayerDeath, 
        EnemyDeath, CollectItem, Pause, Unpause, SwordAttack1, //add more enemy attacks
        SwordAttack2, UIHover, UIClick, PickupUsed,
        ShootLaser, LaserLoop, ShootGrenade, GrenadeExplosion, DialogueSound, TeleportIn, TeleportOut
    };

    public AudioMixerSnapshot pausedSnap;
    public AudioMixerSnapshot unpausedSnap;

    //Sound Effect Enum and Dictionary setup
    public List<AudioClip> SFXList = new List<AudioClip>();
    private Dictionary<SFX, AudioClip> SFXDictionary = new Dictionary<SFX, AudioClip>();
    public GameObject SFXPrefab;

    public enum Music { MainMenu, Level1 };
    //Music enum and Dictionary setup
    private AudioSource MusicPlayer;
    public List<AudioClip> MusicList = new List<AudioClip>();
    private Dictionary<Music, AudioClip> MusicDictionary = new Dictionary<Music, AudioClip>();

    //This function plays a sfx given an enum value from the SFX enum
    public void PlaySFX(SFX s)
    {
        //spawn sfx prefab and get audio source component
        AudioSource SoundEffect = Instantiate(SFXPrefab).GetComponent<AudioSource>();
        //play sfx
        SoundEffect.PlayOneShot(SFXDictionary[s]);
        //destroy prefab instance when sound has played
        Destroy(SoundEffect.gameObject, SFXDictionary[s].length);
    }
    //This function sets the music to the track given through a Music enum value
    public void SetMusicTrack(Music m)
    {
        MusicPlayer.clip = MusicDictionary[m];
        MusicPlayer.Play();
    }
    public void SetMusicSnapshot(bool isPaused)
    {
        if (isPaused)
        {
            pausedSnap.TransitionTo(.01f);
        }
        else
        {
            unpausedSnap.TransitionTo(.01f);
        }
    }
    public void SetMusicPitch(float newPitch)
    {
        MusicPlayer.pitch = newPitch;
    }
    // Start is called before the first frame update
    void Start()
    {
        MusicPlayer = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();

        //SFXDictionary.Add(SFX.PlayerDamage, SFXList[0]);
        for (int i = 0; i < SFXList.Capacity; i++)
        {
            SFXDictionary.Add((SFX)i, SFXList[i]);
        }
        MusicDictionary.Add(Music.MainMenu, MusicList[0]);
        MusicDictionary.Add(Music.Level1, MusicList[1]);
    }
}
