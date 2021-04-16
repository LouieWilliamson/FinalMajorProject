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
    private void Start()
    {
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("StartScreen"))
        {
            MusicPlayer = GameObject.Find("MusicPlayer").GetComponent<AudioSource>();
            MusicPlayer.clip = menumusic;
            MusicPlayer.Play();
        }
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Level-1");
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
