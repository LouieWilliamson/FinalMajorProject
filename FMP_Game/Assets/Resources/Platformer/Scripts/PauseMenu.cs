using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public HUDManager hud;
    public void Resume()
    {
        hud.PauseGame();
    }
}
