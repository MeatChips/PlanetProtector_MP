using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private Canvas canvasUI;
    private Canvas canvasMenu;

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
    }

    public void ClosePauseMenu()
    {
        canvasMenu.enabled = false;
        canvasUI.enabled = true;
        GameManager.Instance.GamePaused = false;
    }

    public void LoadScene(string sceneName)
    {
        GameManager.Instance.GamePaused = false;
        SceneManager.LoadScene(sceneName);
    }
}
