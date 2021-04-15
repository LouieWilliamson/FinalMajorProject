using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{
    float respawnTimer;
    float seconds;
    public Text timerText;
    
    // Start is called before the first frame update
    void Start()
    {
        respawnTimer = 0;
        seconds = 5;
        timerText.text = "" + seconds;
        
    }

    // Update is called once per frame
    void Update()
    {
        respawnTimer += Time.deltaTime;

        //timescale is set to 0.5 so the 1 second is halfed to accomodate this.
        if (respawnTimer >= Time.timeScale)
        {
            seconds--;
            timerText.text = "" + seconds;
            respawnTimer = 0;
        }

        if(seconds <= 0)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Level-1");
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("StartScreen");
        }
    }
}
