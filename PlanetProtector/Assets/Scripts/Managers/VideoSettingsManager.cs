using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VideoSettingsManager : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullScreenToggle;

    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;

    private double currentRefreshRate;
    private int currentResolutionIndex = 0;

    bool isFullscreen;

    void Start()
    {
        fullScreenToggle.isOn = Screen.fullScreen;

        resolutions = Screen.resolutions;
        filteredResolutions = new List<Resolution>();

        resolutionDropdown.ClearOptions();
        currentRefreshRate = Screen.currentResolution.refreshRateRatio.value;

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].refreshRateRatio.value == currentRefreshRate)
            {
                filteredResolutions.Add(resolutions[i]);
            }
        }

        List<string> options = new List<string>();
        for (int i = 0; i < filteredResolutions.Count; i++)
        {
            string resolutionOption = filteredResolutions[i].width + "x" + filteredResolutions[i].height + " - " + filteredResolutions[i].refreshRateRatio.value.ToString("F0") + "Hz";
            options.Add(resolutionOption);

            if (filteredResolutions[i].width == Screen.width && filteredResolutions[i].height == Screen.height)
            {
                currentResolutionIndex = Mathf.Max(options.Count);
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = Mathf.Max(options.Count);
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        SetFullScreen();
        Resolution resolution = filteredResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, isFullscreen);
    }

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
