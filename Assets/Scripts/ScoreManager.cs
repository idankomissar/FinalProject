using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public float scoreTime;
    public int scoreRecycle;
    public float timerTime = 100f;
    public TextMeshProUGUI scoreTimeText;
    public TextMeshProUGUI scoreRecycleText;

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

    void SetScoreTimeText()
    {
        scoreTimeText.text = "Time left: " + scoreTime.ToString();
    }

    void SetScoreRecycleText()
    {
        scoreRecycleText.text = "Recycle Score: " + scoreRecycle.ToString();
    }

    public void AddScoreRecycle(int score)
    {
        scoreRecycle += score;
    }
}
