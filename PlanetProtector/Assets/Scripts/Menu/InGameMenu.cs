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
    private Canvas canvasTutorial;

    [SerializeField] private GameObject[] tutorialMenus;

    private bool imageOpened;

    // Start is called before the first frame update
    void Awake()
    {
        canvasUI = GameObject.Find("CanvasUI").GetComponent<Canvas>();
        canvasEnd = GameObject.Find("CanvasEnd").GetComponent<Canvas>();
        canvasMenu = GameObject.Find("CanvasMenu").GetComponent<Canvas>();
        canvasTutorial = GameObject.Find("CanvasTutorial").GetComponent<Canvas>();
    }

    private void Start()
    {
        canvasMenu.enabled = false;
        canvasEnd.enabled = false;
        canvasUI.enabled = false;

        ChangeSettingsMenu(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!GameManager.Instance.gamePaused) OpenPauseMenu();
            else ClosePauseMenu();
        }
    }

    public void OpenPauseMenu()
    {
        canvasMenu.enabled = true;
        canvasUI.enabled = false;
        GameManager.Instance.gamePaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ClosePauseMenu()
    {
        canvasMenu.enabled = false;
        GameManager.Instance.gamePaused = false;
        if (GameManager.Instance.gameStarted)
        {
            canvasUI.enabled = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void OpenEndMenu()
    {
        canvasEnd.enabled = true;
        canvasUI.enabled = false;
        GameManager.Instance.gameEnded = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
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

    public void NextPage(int pageNumber)
    {
        ChangeSettingsMenu(pageNumber);
    }

    // Change menu function
    private void ChangeSettingsMenu(int menuNumber)
    {
        // Turn off every menu
        tutorialMenus[0].SetActive(false);
        tutorialMenus[1].SetActive(false);
        tutorialMenus[2].SetActive(false);

        // Turn on the selected menu only
        tutorialMenus[menuNumber].SetActive(true);
    }

    public void StartGame()
    {
        GameManager.Instance.gameStarted = true;
        canvasTutorial.enabled = false;
        canvasEnd.enabled = false;
        canvasMenu.enabled = false;
        canvasUI.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
