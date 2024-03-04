using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoSettingsManager : MonoBehaviour
{
    bool isFullscreen;

    public void SetFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
        if (Screen.fullScreen)
        {
            isFullscreen = true;
        }
        else if (!Screen.fullScreen)
        {
            isFullscreen = false;
        }
    }
}
