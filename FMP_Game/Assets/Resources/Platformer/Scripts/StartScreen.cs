using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    // Start is called before the first frame update
    private int Level1;
    public GameObject controls;
    public GameObject settings;
    public void PlayGame()
    {
        SceneManager.LoadScene("Level-1");
    }
    public void OpenControls()
    {
        controls.SetActive(true);
    }
    public void CloseControls()
    {
        controls.SetActive(false);
    }
    public void OpenSettings()
    {
        settings.SetActive(true);
    }
    public void CloseSettings()
    {
        settings.SetActive(false);
    }
    public void QuitGame()
    {

    }
}
