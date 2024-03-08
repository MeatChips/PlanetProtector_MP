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

    [SerializeField] private EnemySpawner enemySpawnerScript;

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

        if(GameManager.Instance.Score >= 500)
            enemySpawnerScript.maxEnemies = 30;
        else if(GameManager.Instance.Score >= 1000)
            enemySpawnerScript.maxEnemies = 40;
        else if (GameManager.Instance.Score >= 1500)
            enemySpawnerScript.maxEnemies = 50;
        else if (GameManager.Instance.Score >= 2000)
            enemySpawnerScript.maxEnemies = 60;
    }
}
