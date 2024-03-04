using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private TMP_Text scoreText;

    private bool gameStarted;
    private bool gameEnded;
    private bool gamePaused;
    public bool GameStarted { get { return gameStarted; } set { gameStarted = value; } }
    public bool GameEnded { get { return gameEnded; } set { gameEnded = value; } }
    public bool GamePaused { get { return gamePaused; } set { gamePaused = value; } }

    public int playerScore;

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

        if(scoreText == null)
            return;
    }

    private void Start()
    {
        GameEnded = false; GamePaused = false;
    }

    private void Update()
    {
        if (gameStarted)
            scoreText.text = "Score: " + playerScore.ToString();
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
            GameStarted = true;
            scoreText = GameObject.Find("ScoreText").GetComponent<TMP_Text>();
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
