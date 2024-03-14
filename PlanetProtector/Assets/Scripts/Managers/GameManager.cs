using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool gameStarted;
    public bool gameEnded;
    public bool gamePaused;

    private int score;
    private int highscore;
    public int Score { get { return score; } set { score = value; } }
    public int Highscore { get { return highscore; } set { highscore = value; } }

    private float masterVolume;
    private float musicVolume;
    private float sfxVolume;
    private float ambienceVolume;
    public float MasterVolume { get { return masterVolume; } set { masterVolume = value; } }
    public float MusicVolume { get { return musicVolume; } set { musicVolume = value; } }
    public float SfxVolume { get { return sfxVolume; } set { sfxVolume = value; } }
    public float AmbienceVolume { get { return ambienceVolume; } set { ambienceVolume = value; } }

    private float sensitivityX;
    private float sensitivityY;

    public float SensitivityX { get { return sensitivityX; } set { sensitivityX = value; } }
    public float SensitivityY { get { return sensitivityY; } set { sensitivityY = value; } }

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        gameEnded = false; gamePaused = false;

        masterVolume = PlayerPrefs.GetFloat("mastervolume");
        musicVolume = PlayerPrefs.GetFloat("musicvolume");
        sfxVolume = PlayerPrefs.GetFloat("sfxvolume");
        ambienceVolume = PlayerPrefs.GetFloat("ambiencevolume");
        sensitivityX = PlayerPrefs.GetInt("sensx");
        sensitivityY = PlayerPrefs.GetInt("sensy");
    }

    private void Update()
    {
        if (gameStarted)
        {
            highscore = PlayerPrefs.GetInt("highscore");
        }

        if (gameEnded)
        {
            if(highscore < score)
            {
                highscore = score;
                PlayerPrefs.SetInt("highscore", highscore);
                PlayerPrefs.Save();

            }
        }

        Debug.Log(SensitivityX + " = SENS X");
        Debug.Log(SensitivityY + " = SENS Y");
    }


    // Scene loading/checking
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "Main")
        {
            gameStarted = true;
            gameEnded = false;
            gamePaused = false;
        }

        if (scene.name == "MainMenu" || scene.name == "Settings")
        {
            gameStarted = false;
            gameEnded = false;
            gamePaused = false;
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
