using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private bool gameStarted;
    private bool gameEnded;
    private bool gamePaused;
    public bool GameStarted { get { return gameStarted; } set { gameStarted = value; } }
    public bool GameEnded { get { return gameEnded; } set { gameEnded = value; } }
    public bool GamePaused { get { return gamePaused; } set { gamePaused = value; } }

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
        GameEnded = false; GamePaused = false;
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
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
