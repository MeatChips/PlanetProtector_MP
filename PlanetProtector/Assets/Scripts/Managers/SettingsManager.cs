using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private GameObject[] settingsMenus;

    private void Start()
    {
        ChangeSettingsMenu(0);
    }

    public void VideoSettingsSwitch()
    {
        ChangeSettingsMenu(0);
    }

    public void AudioSettingsSwitch()
    {
        ChangeSettingsMenu(1);
    }

    public void KeybindSettingsSwitch()
    {
        ChangeSettingsMenu(2);
    }

    // Change menu function
    private void ChangeSettingsMenu(int menuNumber)
    {
        // Turn off every menu
        settingsMenus[0].SetActive(false);
        settingsMenus[1].SetActive(false);
        settingsMenus[2].SetActive(false);

        // Turn on the selected menu only
        settingsMenus[menuNumber].SetActive(true);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
