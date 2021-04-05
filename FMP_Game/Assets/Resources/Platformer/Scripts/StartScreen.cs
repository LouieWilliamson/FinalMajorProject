using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    // Start is called before the first frame update
    private int Level1;
    public Animator controls;
    public Animator settings;

    public void PlayGame()
    {
        SceneManager.LoadScene("Level-1");
    }
    public void OpenControls()
    {
        //controls.gameObject.SetActive(true);
        controls.SetTrigger("In");
    }
    public void CloseControls()
    {
        //controls.gameObject.SetActive(false);
        controls.SetTrigger("Out");
    }
    public void OpenSettings()
    {
        //settings.gameObject.SetActive(true);
        settings.SetTrigger("In");
    }
    public void CloseSettings()
    {
        //settings.gameObject.SetActive(false);
        settings.SetTrigger("Out");
    }
    public void QuitGame()
    {

    }
}
