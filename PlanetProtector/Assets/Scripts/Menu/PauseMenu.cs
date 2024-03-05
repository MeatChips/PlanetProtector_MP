using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    private Canvas canvasUI;
    private Canvas canvasMenu;

    [SerializeField] private TMP_Text highscoreText;
    [SerializeField] private TMP_Text scoreText;

    // Start is called before the first frame update
    void Awake()
    {
        canvasUI = GameObject.Find("CanvasUI").GetComponent<Canvas>();
        canvasMenu = GetComponent<Canvas>();

        canvasMenu.enabled = false; 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!GameManager.Instance.GamePaused) OpenPauseMenu();
            else ClosePauseMenu();
        }
    }

    public void OpenPauseMenu()
    {
        canvasMenu.enabled = true;
        canvasUI.enabled = false;
        GameManager.Instance.GamePaused = true;
        Cursor.visible = true;

        scoreText.text = GameManager.Instance.playerScore.ToString();
    }

    public void ClosePauseMenu()
    {
        canvasMenu.enabled = false;
        canvasUI.enabled = true;
        GameManager.Instance.GamePaused = false;
        Cursor.visible = false;
    }

    public void LoadScene(string sceneName)
    {
        GameManager.Instance.GamePaused = false;
        SceneManager.LoadScene(sceneName);
    }
}
