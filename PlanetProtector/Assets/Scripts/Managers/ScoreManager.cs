using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private float timePassed = 0f;
    public float timeUntilUpdate = 10f;

    [SerializeField] private TMP_Text scoreTextPauseScreen;
    [SerializeField] private TMP_Text scoreTextEndScreen;

    [SerializeField] private TMP_Text highscoreTextEndScreen;
    [SerializeField] private TMP_Text highscoreTextPauseScreen;

    [SerializeField] private TMP_Text scoreText;

    private void Start()
    {
        GameManager.Instance.Score = 0;
    }

    private void Update()
    {
        if (!GameManager.Instance.gamePaused)
        {
            timePassed += Time.deltaTime;
            if (timePassed > timeUntilUpdate)
            {
                GameManager.Instance.Score += 10;
                timePassed = 0f;
            }
        }

        scoreTextPauseScreen.text = GameManager.Instance.Score.ToString();
        scoreTextEndScreen.text = GameManager.Instance.Score.ToString();
        scoreText.text = "Score: " + GameManager.Instance.Score.ToString();

        highscoreTextEndScreen.text = PlayerPrefs.GetInt("highscore").ToString();
        highscoreTextPauseScreen.text = PlayerPrefs.GetInt("highscore").ToString();
    }
}
