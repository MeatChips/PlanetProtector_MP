using UnityEngine;
using UnityEngine.Audio;
using System;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioSettingsManager : MonoBehaviour
{
    private TMP_Text masterVolumeText;
    private TMP_Text musicVolumeText;
    private TMP_Text ambienceVolumeText;
    private TMP_Text sfxVolumeText;

    private Slider masterSlider;
    private Slider musicSlider;
    private Slider ambienceSlider;
    private Slider sfxSlider;

    [SerializeField] private AudioMixerGroup masterMixerGroup;
    [SerializeField] private AudioMixerGroup musicMixerGroup;
    [SerializeField] private AudioMixerGroup ambienceMixerGroup;
    [SerializeField] private AudioMixerGroup sfxMixerGroup;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        // Checking if there is a volume, if not assign 0.75
        if (!PlayerPrefs.HasKey("mastervolume"))
        {
            PlayerPrefs.SetFloat("mastervolume", 0.75f);
        }

        if (!PlayerPrefs.HasKey("musicvolume"))
        {
            PlayerPrefs.SetFloat("musicvolume", 0.75f);
        }

        if (!PlayerPrefs.HasKey("sfxvolume"))
        {
            PlayerPrefs.SetFloat("sfxvolume", 0.75f);
        }

        if (!PlayerPrefs.HasKey("ambiencevolume"))
        {
            PlayerPrefs.SetFloat("ambiencevolume", 0.75f);
        }
    }

    private void Start()
    {
        LoadAudioSettings();
    }

    // Change master volume
    private void OnMasterValueChanged()
    {
        GameManager.Instance.MasterVolume = masterSlider.value;
        masterVolumeText.text = ((int)(masterSlider.value * 100)).ToString();
        PlayerPrefs.SetFloat("mastervolume", masterSlider.value);

        UpdateMixerVolume();
    }

    // Change music volume
    private void OnMusicValueChanged()
    {
        GameManager.Instance.MusicVolume = musicSlider.value;
        musicVolumeText.text = ((int)(musicSlider.value * 100)).ToString();
        PlayerPrefs.SetFloat("musicvolume", musicSlider.value);

        UpdateMixerVolume();
    }

    // Change sound effects volume
    private void OnSfxValueChanged()
    {
        GameManager.Instance.SfxVolume = sfxSlider.value;
        sfxVolumeText.text = ((int)(sfxSlider.value * 100)).ToString();
        PlayerPrefs.SetFloat("sfxvolume", sfxSlider.value);

        UpdateMixerVolume();
    }

    // Change ambience volume
    private void OnAmbienceValueChanged()
    {
        GameManager.Instance.AmbienceVolume = ambienceSlider.value;
        ambienceVolumeText.text = ((int)(ambienceSlider.value * 100)).ToString();
        PlayerPrefs.SetFloat("ambiencevolume", ambienceSlider.value);

        UpdateMixerVolume();
    }

    // Reset the audio
    public void ResetAudioSettings()
    {
        masterSlider.value = 0.75f;
        masterVolumeText.text = ((int)(masterSlider.value * 100)).ToString();
        PlayerPrefs.SetFloat("mastervolume", masterSlider.value);

        musicSlider.value = 0.75f;
        musicVolumeText.text = ((int)(musicSlider.value * 100)).ToString();
        PlayerPrefs.SetFloat("musicvolume", musicSlider.value);

        sfxSlider.value = 0.75f;
        sfxVolumeText.text = ((int)(sfxSlider.value * 100)).ToString();
        PlayerPrefs.SetFloat("sfxvolume", sfxSlider.value);

        ambienceSlider.value = 0.75f;
        ambienceVolumeText.text = ((int)(ambienceSlider.value * 100)).ToString();
        PlayerPrefs.SetFloat("ambiencevolume", ambienceSlider.value);

        PlayerPrefs.Save();
    }

    // Load the audio voumes
    private void LoadAudioSettings()
    {
        GameManager.Instance.MasterVolume = PlayerPrefs.GetFloat("mastervolume");
        GameManager.Instance.MusicVolume = PlayerPrefs.GetFloat("musicvolume");
        GameManager.Instance.SfxVolume = PlayerPrefs.GetFloat("sfxvolume");
        GameManager.Instance.AmbienceVolume = PlayerPrefs.GetFloat("ambiencevolume");
        PlayerPrefs.Save();

        UpdateMixerVolume();
    }

    // Update the mixer's volume
    public void UpdateMixerVolume()
    {
        masterMixerGroup.audioMixer.SetFloat("VolumeMaster", Mathf.Log10(GameManager.Instance.MasterVolume) * 20);
        sfxMixerGroup.audioMixer.SetFloat("VolumeSoundEffects", Mathf.Log10(GameManager.Instance.SfxVolume) * 20);
        musicMixerGroup.audioMixer.SetFloat("VolumeMusic", Mathf.Log10(GameManager.Instance.MusicVolume) * 20);
        ambienceMixerGroup.audioMixer.SetFloat("VolumeAmbience", Mathf.Log10(GameManager.Instance.AmbienceVolume) * 20);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        // Add all 
        if (scene.name == "Settings")
        {
            masterVolumeText = GameObject.Find("MasterAudioNumber").GetComponent<TMP_Text>();
            musicVolumeText = GameObject.Find("MusicAudioNumber").GetComponent<TMP_Text>();
            ambienceVolumeText = GameObject.Find("AmbienceAudioNumber").GetComponent<TMP_Text>();
            sfxVolumeText = GameObject.Find("SoundEffectsAudioNumber").GetComponent<TMP_Text>();

            masterSlider = GameObject.Find("MasterAudioSlider").GetComponent<Slider>();
            musicSlider = GameObject.Find("MusicAudioSlider").GetComponent<Slider>();
            ambienceSlider = GameObject.Find("AmbienceAudioSlider").GetComponent<Slider>();
            sfxSlider = GameObject.Find("SoundEffectsAudioSlider").GetComponent<Slider>();

            GameObject.Find("MasterAudioSlider").GetComponent<Slider>().onValueChanged.AddListener(delegate { OnMasterValueChanged(); });
            GameObject.Find("MusicAudioSlider").GetComponent<Slider>().onValueChanged.AddListener(delegate { OnMusicValueChanged(); });
            GameObject.Find("AmbienceAudioSlider").GetComponent<Slider>().onValueChanged.AddListener(delegate { OnAmbienceValueChanged(); });
            GameObject.Find("SoundEffectsAudioSlider").GetComponent<Slider>().onValueChanged.AddListener(delegate { OnSfxValueChanged(); });

            GameObject.Find("ResetAudioButton").GetComponent<Button>().onClick.AddListener(() => { ResetAudioSettings(); });

            masterSlider.value = PlayerPrefs.GetFloat("mastervolume");
            musicSlider.value = PlayerPrefs.GetFloat("musicvolume");
            sfxSlider.value = PlayerPrefs.GetFloat("sfxvolume");
            ambienceSlider.value = PlayerPrefs.GetFloat("ambiencevolume");
            masterVolumeText.text = ((int)(PlayerPrefs.GetFloat("mastervolume") * 100)).ToString();
            musicVolumeText.text = ((int)(PlayerPrefs.GetFloat("musicvolume") * 100)).ToString();
            sfxVolumeText.text = ((int)(PlayerPrefs.GetFloat("sfxvolume") * 100)).ToString();
            ambienceVolumeText.text = ((int)(PlayerPrefs.GetFloat("ambiencevolume") * 100)).ToString();
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
