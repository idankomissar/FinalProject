using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    #region Singelton Decleration
    private static ScoreManager _instance;

    public static ScoreManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    public int scoreTime = 500;
    public int scoreRecycle;
    public int timerTime = 500;
    public TextMeshPro scoreTimeText;
    public TextMeshPro scoreRecycleText;
    public TextMeshPro gameOverText;
    public int numberOfJunks;
    
    private void Start()
    {
        SetScoreTimeText();
        SetScoreRecycleText();
    }
    void Update()
    {
        numberOfJunks = (GameObject.FindGameObjectsWithTag("Junk")).Length;
        if (scoreTime >= 0 && numberOfJunks > 0)
        {
            scoreTime = (int)(timerTime - Time.time);
            SetScoreTimeText();
            SetScoreRecycleText();
        }
        else
        {
            finishGame();
        }
    }

    public void SetScoreTimeText()
    {
        scoreTimeText.text = "Time left: " + scoreTime.ToString();
    }

    public void SetScoreRecycleText()
    {
        scoreRecycleText.text = "Score: " + scoreRecycle.ToString();
    }

    public void AddScoreRecycle(int score)
    {
        scoreRecycle += score;
    }

    public void finishGame()
    {
        Destroy(scoreRecycleText);
        Destroy(scoreTimeText);
        gameOverText.text = "Game Over! Your score is: " + scoreRecycle.ToString();
    }
}
