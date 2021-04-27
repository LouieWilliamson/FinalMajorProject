using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator controls;
    public Animator settings;
    public Animator quitCheck;

    private AudioSource MusicPlayer;
    public AudioClip menumusic;
    private AudioManager sound;
    private void Start()
    {
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("StartScreen"))
        {
            sound = GameObject.FindGameObjectWithTag("Manager").GetComponent<AudioManager>();
            MusicPlayer = GameObject.Find("MusicPlayer").GetComponent<AudioSource>();

            if (MusicPlayer.clip != menumusic)
            {
                MusicPlayer.clip = menumusic;
                MusicPlayer.Play();
            }

            sound.SetMusicSnapshot(false);
        }
    }
    public void PlayUIClick()
    {
        sound.PlaySFX(AudioManager.SFX.UIClick);
    }
    public void PlayUIHover()
    {
        sound.PlaySFX(AudioManager.SFX.UIHover);
    }
    public void PlayGame()
    {
        GameObject.Find("Fader").GetComponent<Fader>().FadeOut();
    }
    public void OpenControls()
    {
        controls.SetTrigger("In");
    }
    public void CloseControls()
    {
        controls.SetTrigger("Out");
    }
    public void OpenSettings()
    {
        settings.SetTrigger("In");
    }
    public void CloseSettings()
    {
        settings.SetTrigger("Out");
    }
    public void QuitCheck()
    {
        quitCheck.SetTrigger("In");
    }
    public void CloseQuitCheck()
    {
        quitCheck.SetTrigger("Out");
    }
    public void QuitGame()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("StartScreen"))
        {

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
        }
        else
        {
            if (Time.timeScale != 1) Time.timeScale = 1;

            SceneManager.LoadScene("StartScreen");
        }
    }
}
