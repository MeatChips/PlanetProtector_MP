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
