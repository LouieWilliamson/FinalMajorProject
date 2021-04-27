using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fader : MonoBehaviour
{
    Animator anim;
    private int currentScene;
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        anim = GetComponent<Animator>();
    }
    public void FadeOut()
    {
        anim.SetTrigger("FadeOut");
    }
    public void FadeIn()
    {
        anim.SetTrigger("FadeIn");
    }
    public void FadeComplete()
    {
        //if start screen, change scene to level1
        if (currentScene == 0)
        {
            SceneManager.LoadScene("Level-1");
        }
        else
        {
            GameObject.Find("LoadingScreen").SetActive(false);
            FadeIn();
            //disable loading
        }
    }
}
