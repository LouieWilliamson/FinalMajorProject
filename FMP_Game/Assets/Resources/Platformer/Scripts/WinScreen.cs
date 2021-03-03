using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public Text percentText;
    private int enemyPercent;
    bool textSet;
    // Update is called once per frame
    private void Start()
    {
        textSet = false;
        enemyPercent = 0;
        SetText();
    }
    void Update()
    {
        if(enemyPercent > 0 && !textSet)
        {
            SetText();
            textSet = true;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            print("Next Level");
            //SceneManager.LoadScene("");
        } 
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("StartScreen");
        }
    }
    private void SetText()
    {
        percentText.text = "You killed " + enemyPercent + "% of Enemies";
    }
    public void SetPercent(int percent) { enemyPercent = percent; }
}
