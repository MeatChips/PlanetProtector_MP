using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class InGameMenu : MonoBehaviour
{
    private Canvas canvasUI;
    private Canvas canvasMenu;
    private Canvas canvasEnd;

    // Start is called before the first frame update
    void Awake()
    {
        canvasUI = GameObject.Find("CanvasUI").GetComponent<Canvas>();
        canvasEnd = GameObject.Find("CanvasEnd").GetComponent<Canvas>();
        canvasMenu = GameObject.Find("CanvasMenu").GetComponent<Canvas>();
    }

    private void Start()
    {
        canvasMenu.enabled = false;
        canvasEnd.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!GameManager.Instance.gamePaused) OpenPauseMenu();
            else ClosePauseMenu();
        }

        if (GameManager.Instance.gameEnded)
            OpenEndMenu();
    }

    public void OpenPauseMenu()
    {
        canvasMenu.enabled = true;
        canvasUI.enabled = false;
        GameManager.Instance.gamePaused = true;
        Cursor.visible = true;
    }

    public void ClosePauseMenu()
    {
        canvasMenu.enabled = false;
        canvasUI.enabled = true;
        GameManager.Instance.gamePaused = false;
        Cursor.visible = false;
    }

    public void OpenEndMenu()
    {
        canvasEnd.enabled = true;
        canvasUI.enabled = false;
        GameManager.Instance.gameEnded = true;
        Cursor.visible = true;
    }

    public void LoadScene(string sceneName)
    {
        GameManager.Instance.gamePaused = false;
        SceneManager.LoadScene(sceneName);
    }

    public void Retry(string sceneName)
    {
        GameManager.Instance.gamePaused = false;
        GameManager.Instance.gameEnded = false;
        SceneManager.LoadScene(sceneName);
    }
}
