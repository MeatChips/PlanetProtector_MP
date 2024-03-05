using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private float timePassed = 0f;
    public float timeUntilUpdate = 10f;

    private void Update()
    {
        if (!GameManager.Instance.GamePaused)
        {
            timePassed += Time.deltaTime;
            if (timePassed > timeUntilUpdate)
            {
                GameManager.Instance.playerScore += 10;
                timePassed = 0f;
            }
        }
    }
}
