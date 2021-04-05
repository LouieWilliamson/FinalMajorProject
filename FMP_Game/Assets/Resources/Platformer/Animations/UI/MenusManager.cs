using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenusManager : MonoBehaviour
{
    public enum UI { Controls, Settings };
    // Start is called before the first frame update
    public Animator Controls;
    public Animator Settings;

    public void BringIn(UI ui)
    {
        switch (ui)
        {
            case UI.Controls:
                Controls.gameObject.SetActive(true);
                Controls.SetTrigger("In");
                break;
            case UI.Settings:
                Settings.gameObject.SetActive(true);
                Settings.SetTrigger("In");
                break;
            default:
                break;
        }
    }
    public void BringOut(UI ui)
    {
        switch (ui)
        {
            case UI.Controls:
                Controls.SetTrigger("Out");
                break;
            case UI.Settings:
                Settings.SetTrigger("Out");
                break;
            default:
                break;
        }
    }
    //Used
    public void DisableObject(UI ui)
    {
        switch (ui)
        {
            case UI.Controls:
                Controls.gameObject.SetActive(false);
                break;
            case UI.Settings:
                Settings.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }
}
