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

    public float scoreTime;
    public int scoreRecycle;
    public float timerTime = 100f;
    public TextMeshPro scoreTimeText;
    public TextMeshPro scoreRecycleText;

    private void Start()
    {
        SetScoreTimeText();
        SetScoreRecycleText();
    }
    void Update()
    {
        scoreTime = timerTime - Time.time;
        SetScoreTimeText();
        SetScoreRecycleText();
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
}
